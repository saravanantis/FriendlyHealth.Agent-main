using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ppsha.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using VirtualPostmanService;
using vp_webservice_auth;
using vp_webservice_documenttasks;

namespace ppsha.Helper
{
    public class ServiceBusiness
    {
        private string baseURL;
        private string tokenString;
        private int userId;
        private ClaimApiClient claimApiClient;
        private UserApiClient userApiClient;
        private readonly ILogger _logger;
        private MySettings _mySettings { get; set; }

        public ServiceBusiness(ILogger logger)
        {
            _logger = logger;
            baseURL = GlobalStatic._MySettings.BaseURL;
            _mySettings = GlobalStatic._MySettings;
            claimApiClient = new ClaimApiClient(baseURL);
            userApiClient = new UserApiClient(baseURL, GlobalStatic._MySettings);
        }

        public async Task<bool> CreateClaim()
        {
            try
            {

                var signinResult = await userApiClient.UserLogin();
                if (signinResult.Item2)
                {
                    tokenString = signinResult.Item1;
                    userId = signinResult.Item3;
                    AuthClient auth = new AuthClient();
                    String sessionId = auth.login(_mySettings.VPUsernName, _mySettings.VPPassword);
                    TaskQueuesClient tasksClient = new TaskQueuesClient();
                    TaskQueueEntry entryInformation = tasksClient.getNextEntryInTaskQueue(sessionId, "INDEXING");

                    if (entryInformation != null)
                    {
                        vp_webservice_documents.DocumentsClient documentsClient = new vp_webservice_documents.DocumentsClient();
                        vp_webservice_documents.document document = documentsClient.getDocument(sessionId, entryInformation.documentId);
                        vp_webservice_documents.document[] listDocuments = null;

                        if (document.contentType.Equals("application/pdf"))
                        {
                            vp_webservice_documents.document[] listDocument = new vp_webservice_documents.document[1] { document };
                            listDocuments = listDocument;
                        }
                        else if (document.contentType.Equals("message/rfc822"))
                        {
                            listDocuments = documentsClient.getDerivedDocuments(sessionId, entryInformation.documentId);
                        }
                        else
                        {
                            //Unrecognized content type
                        }
                        var clientJson = new
                        {
                            EntryInformation = entryInformation,
                            Document = document,
                            DocumentsList = listDocuments
                        };
                        var clientJsonStr = JsonConvert.SerializeObject(clientJson);

                        var unProcessedClaimIds = await claimApiClient.GetUnprocessedClaimIds(tokenString);
                        var processedClaimIds = await claimApiClient.GetProcessedClaimIds(tokenString);
                        if (unProcessedClaimIds.Count > 0 && unProcessedClaimIds != null)
                        {
                            var outputRes = UnProcessedClaimPostToOcrApi(unProcessedClaimIds);
                        }
                        if (processedClaimIds.Count > 0 && processedClaimIds != null)
                        {
                            var outputRes = ProcessedClaimPostToVP(processedClaimIds, sessionId);
                        }
                        var upCount = unProcessedClaimIds?.FindAll(x => x.DocumentQueueEntryId == entryInformation.queueEntryId);
                        var pCount = processedClaimIds?.FindAll(x => x.DocumentQueueEntryId == entryInformation.queueEntryId);
                        if (upCount.Count == 0 && pCount.Count == 0)
                        {
                            ClaimRequestModel claimRequestModel = new ClaimRequestModel();
                            List<string> claimIds = new List<string>();
                            if (listDocuments.Length > 0 && listDocuments != null)
                            {
                                foreach (vp_webservice_documents.document d in listDocuments)
                                {
                                    ClaimEntity claimData = new ClaimEntity();
                                    var doc = documentsClient.getDocumentContentAsPdf(sessionId, d.id);
                                    claimData.ClaimPdfData = doc.data;
                                    string[] sizes = { "B", "KB", "MB", "GB", "TB" };

                                    double len = doc.data.Length;
                                    int order = 0;
                                    while (len >= 1024 && order < sizes.Length - 1)
                                    {
                                        order++;
                                        len = len / 1024;
                                    }
                                    string fileSize = String.Format("{0:0.##} {1}", len, sizes[order]);
                                    claimData.PdfSize = fileSize;
                                    claimData.PDFName = d.originalFileName;
                                    claimData.TypeOfClaimId = 1;
                                    claimData.UserId = userId;
                                    claimData.ClientJson = clientJsonStr;
                                    var claimResult = await claimApiClient.CreateClaimApiClient(claimData, tokenString);
                                    if (claimResult.ClaimId != null)
                                    {
                                        string claimId = string.Join(",", claimResult.ClaimId.ToString());
                                        claimIds.Add(claimId);
                                    }
                                }
                            }
                            if (claimIds.Count > 0 && claimIds != null)
                            {
                                claimRequestModel.ClaimIds = claimIds.ToArray();
                                claimRequestModel.Debug = false;
                                claimRequestModel.ProcessAll = true;
                                var processResult = await claimApiClient.ClaimOCRAsyncApiClientForMultiplePdf(claimRequestModel, tokenString);
                            }
                        }
                    }
                    else
                    {
                        _logger.LogError("Null Entry Information from VP.");
                    }
                    return true;
                }
                else
                {
                    _logger.LogError("");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error Occured in Textract Service", ex.Message.ToString(), ex.StackTrace.ToString());
                return false;
            }
        }
        private async Task<bool> UnProcessedClaimPostToOcrApi(List<ClaimEntity> unProcessedClaimIds)
        {
            var claimIds = unProcessedClaimIds.Select(item => item.ClaimId.ToString()).ToList();
            ClaimRequestModel claimRequestModel = new ClaimRequestModel();
            if (claimIds.Count > 0 && claimIds != null)
            {
                claimRequestModel.ClaimIds = claimIds.ToArray();
                claimRequestModel.Debug = false;
                claimRequestModel.ProcessAll = true;
                var processResult = await claimApiClient.ClaimOCRAsyncApiClientForMultiplePdf(claimRequestModel, tokenString);
            }
            return true;
        }
        private async Task<bool> ProcessedClaimPostToVP(List<ClaimEntity> processedClaimIds, string sessionId)
        {
            var groups = processedClaimIds.GroupBy(x => x.DocumentQueueEntryId);
            ClaimRequestModel claimRequestModel = new ClaimRequestModel();
            List<string> processedUpdateClaimIds = new List<string>();
            List<string> unProcessedClaimIds = new List<string>();
            foreach (var item in groups)
            {
                DataTable resultDt = new DataTable();
                foreach (var up in item)
                {
                    var structuredJson = await claimApiClient.GetStructuredJsonByClaimId(up.ClaimId.ToString(), tokenString);
                    if (structuredJson.Count > 0 && structuredJson != null)
                    {
                        DataTable resultStructuredDt = jsonStringToDatatable(structuredJson[0].StructuredJson);
                        resultDt.Merge(resultStructuredDt);
                        string updateClaimId = string.Join(",", up.ClaimId.ToString());
                        processedUpdateClaimIds.Add(updateClaimId);
                    }
                    else
                    {
                        string claimId = string.Join(",", up.ClaimId.ToString());
                        unProcessedClaimIds.Add(claimId);
                    }
                }
                var result = DataTableToCsv(resultDt, item.First().DocumentQueueEntryId.ToString());
                var vpc = new VirtualPostmanClient();
                TaskQueuesClient tasksClient = new TaskQueuesClient();
                var newIndex = new indexValue();
                newIndex.indexName = "ediData";
                string output = String.Join("\n", result.Select(x => x.ToString()).ToArray());
                newIndex.indexValue1 = output;
                var indexlist = new List<indexValue>();
                indexlist.Add(newIndex);
                vpc.updateIndexes(sessionId, item.First().DocumentId, indexlist.ToArray());
                tasksClient.setEntryProcessedSuccess(sessionId, item.First().DocumentQueueEntryId);
            }
            if (processedUpdateClaimIds.Count > 0 && processedUpdateClaimIds != null)
            {
                var updateClaimResult = await claimApiClient.UpdatePostedClaimIds(processedUpdateClaimIds.ToArray(), tokenString);
            }
            if (unProcessedClaimIds.Count > 0 && unProcessedClaimIds != null)
            {
                claimRequestModel.ClaimIds = unProcessedClaimIds.ToArray();
                claimRequestModel.Debug = false;
                claimRequestModel.ProcessAll = true;
                var processResult = await claimApiClient.ClaimOCRAsyncApiClientForMultiplePdf(claimRequestModel, tokenString);
            }
            return true;
        }
        public static DataTable jsonStringToDatatable(string jsonContent)
        {
            //used NewtonSoft json nuget package
            XmlNode xml = JsonConvert.DeserializeXmlNode("{records:{record:" + jsonContent + "}}");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml.InnerXml);
            XmlReader xmlReader = new XmlNodeReader(xml);
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlReader);
            var dataTable = dataSet.Tables[1];
            DataView dv = new DataView(dataTable);

            DataTable dt = dv.ToTable(false, "FieldValue");
            List<DataTable> splitTable = SplitTable(dt, 68);
            DataTable outTable = new DataTable();

            foreach (var item in splitTable)
            {
                DataTable transposedDt = GenerateTransposedTable(item);
                outTable.Merge(transposedDt);
            }
            outTable.Columns.RemoveAt(0);
            return outTable;
        }
        public static List<string> DataTableToCsv(DataTable outTable, string batchNumber)
        {
            DateTime date = DateTime.Now;
            outTable = AddAutoIncrementColumn(outTable);
            outTable.Columns.RemoveAt(1);
            outTable.Columns["Column1"].SetOrdinal(1);

            //Datatable to CSV
            var lines = new List<string>();
            string[] columnNames = { "1", "NHLS", "M", "0", batchNumber, date.ToString("yyyymmdd"), "RFMCF" };
            var header = string.Join(",", columnNames);
            lines.Add(header);
            var colSum = outTable.AsEnumerable()
                            .Where(x => x.Field<string>("Column_15").ToLower() != "nan" && x.Field<string>("Column_15").ToLower() != "")
                           .Sum(x => Convert.ToDecimal(x.Field<string>("Column_15")))
                            .ToString();
            string[] columnNamesFoo = { "Z", outTable.Rows.Count.ToString(), colSum };
            var footer = string.Join(",", columnNamesFoo);
            var valueLines = outTable.AsEnumerable()
                               .Select(row => string.Join(",", row.ItemArray));
            lines.AddRange(valueLines);
            lines.Add(footer);
            return lines;
        }
        private static List<DataTable> SplitTable(DataTable originalTable, int batchSize)
        {
            List<DataTable> tables = new List<DataTable>();
            int i = 0;
            int j = 1;
            DataTable newDt = originalTable.Clone();
            newDt.TableName = "Table_" + j;
            newDt.Clear();
            foreach (DataRow row in originalTable.Rows)
            {
                DataRow newRow = newDt.NewRow();
                newRow.ItemArray = row.ItemArray;
                newDt.Rows.Add(newRow);
                i++;
                if (i == batchSize)
                {
                    tables.Add(newDt);
                    j++;
                    newDt = originalTable.Clone();
                    newDt.TableName = "Table_" + j;
                    newDt.Clear();
                    i = 0;
                }
            }
            return tables;
        }
        private static DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();
            // Add columns by prefixing Column_ by looping rows
            for (int i = 0; i <= inputTable.Rows.Count; i++)
            {
                outputTable.Columns.Add("Column_" + i.ToString());
            }
            // Add rows by looping columns        
            for (int rCount = 0; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();
                // First column is inputTable's Header row's second column
                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }
            return outputTable;
        }
        public static DataTable AddAutoIncrementColumn(DataTable dt)
        {
            DataColumn column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.AutoIncrementStep = 1;
            dt.Columns.Add(column);
            int index = 0;
            foreach (DataRow row in dt.Rows)
            {
                row.SetField(column, ++index);
            }
            return dt;
        }
    }
}
