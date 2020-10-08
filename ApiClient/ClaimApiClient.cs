using BusinessObjects;
using BusinessObjects.Documents;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ClaimApiClient
    {
        private string BaseURL;

        public ClaimApiClient(string baseURL)
        {
            BaseURL = baseURL;
        }

        public async Task<ClaimEntity> CreateClaimApiClient(ClaimEntity claimEntity)
        {
            string tokenString = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImthcnRoQGZyaWVuZGx5Y2FyZXMuY29tIiwiY2xpZW50X25hbWUiOiJVTlVNX1FBIiwibmJmIjoxNjAwNDIwODkzLCJleHAiOjE2MDA0Mzg4OTMsImlhdCI6MTYwMDQyMDg5M30.XxwARkgEih92h6p9CdUmIpjxeh4NzgErKQp4RXb8L04";
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/createclaim/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }
        public async Task<ClaimEntity> CreateClaimApiClient(string typeofclaimid, IFormFile file, string userId)
        {
            string tokenString = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImthcnRoQGZyaWVuZGx5Y2FyZXMuY29tIiwiY2xpZW50X25hbWUiOiJVTlVNX1FBIiwibmJmIjoxNjAwNDIwODkzLCJleHAiOjE2MDA0Mzg4OTMsImlhdCI6MTYwMDQyMDg5M30.XxwARkgEih92h6p9CdUmIpjxeh4NzgErKQp4RXb8L04";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimpdflistbyuserid/{0}{1}{2}", typeofclaimid, file, userId);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }
        public async Task<T> ClientAPIGet<T>(string path)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                return await ApiGetClient.ExecuteAsync<T>(client, path);
            }
        }
        public async Task<ClaimEntity> CreateClaimByUserIdApiClient(ClaimEntity claimEntity, string tokenString)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/createclaimbyuserid/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<List<ClaimEntity>> GetClaimPdfListApiClient()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimpdflist");

                var results = await ApiGetClient.ExecuteAsync<List<ClaimEntity>>(client, path);
                return results;
            }
        }
        public async Task<List<ClaimEntity>> GetDocumentClaimList(int userid, int currentPage, string folderSearchText)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();

                    var path = "/api/Claim/formprocessing/getdocumentclaimlist";
                    var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var searchInputData = JsonConvert.SerializeObject(new { UserId = userid, CurrentPage = currentPage, FolderSearchText = folderSearchText });
                    HttpContent content = new StringContent(searchInputData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonAsString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<ClaimEntity>>(jsonAsString);
                    }
                    throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClaimEntity> GetDocumentClaim(int userid, string policy, string fileName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();

                    var path = "/api/Claim/formprocessing/getdocumentclaim";
                    var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var searchInputData = JsonConvert.SerializeObject(new { UserId = userid, Policy = policy, FileName = fileName });
                    HttpContent content = new StringContent(searchInputData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonAsString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                    }
                    throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<ClaimEntity>> GetClaimPdfListByUserIdApiClient(string tokenString, string page_no, string claimsearch, bool shareDocumentAccess, bool viewAllDocument, int claimType = 0)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                    //var path = string.Format("/api/Claim/formprocessing/getclaimpdflistbyuserid/{0}/{1}/{2}", page_no, claimsearch, shareDocumentAccess);

                    //var results = await ApiGetClient.ExecuteAsync<List<ClaimEntity>>(client, path);
                    //return results;
                    var path = "/api/Claim/formprocessing/getclaimpdflistbyuserid";
                    var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                    client.DefaultRequestHeaders.Accept.Clear();
                    var searchInputData = JsonConvert.SerializeObject(new { Page_no = page_no, Claimsearch = claimsearch, ShareDocumentAccess = shareDocumentAccess, ViewAllDocument = viewAllDocument, ClaimType = claimType });
                    HttpContent content = new StringContent(searchInputData);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                    var response = await client.PostAsync(uri, content);

                    if (response.IsSuccessStatusCode)
                    {
                        var jsonAsString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<ClaimEntity>>(jsonAsString);
                    }
                    throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
                }
            }
            catch (Exception ex)

            {
                throw;
            }

        }

        public async Task<List<ClaimEntity>> GetClaimPdfListByUserIdApiClient_Listcount(string tokenString, bool shareDocumentAccess)
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimpdflistbyuserid/{0}", shareDocumentAccess);

                var results = await ApiGetClient.ExecuteAsync<List<ClaimEntity>>(client, path);
                return results;
            }
        }
        public async Task<List<ClaimEntity>> GetClaimPdfListByUserIdandClaimTypeApiClient(int userid, int claimtype)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimpdflistbyuseridandclaimtype/{0}/{1}", userid, claimtype);

                var results = await ApiGetClient.ExecuteAsync<List<ClaimEntity>>(client, path);
                return results;
            }
        }

        public async Task<ListFieldsEntity> UpdateClaimApiClient(ClaimEntity claimEntity)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/updateclaimpdf/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);


                var response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ListFieldsEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<ClaimEntity> GetClaimPdfFileApiClient(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimpdf/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }

        public async Task<ClaimEntity> GetClaimPageFileApiClient(string claimId, int pageno)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var path = string.Format("/api/Claim/formprocessing/getclaimblendedpage/{0}/{1}", claimId, pageno);

                    var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                    return results;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<ResultData> ClaimRandomProcess(string tokenstring)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenstring);

                    var path = string.Format("/api/Claim/formprocessing/ClaimRandomProcess");

                    var results = await ApiGetClient.ExecuteAsync<ResultData>(client, path);
                    return results;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<ClaimEntity> GetClaimPagePDFApiClient(string claimId, int pageno, bool compMed)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var path = string.Format("/api/Claim/formprocessing/getclaimpdfandblendedimage/{0}/{1}/{2}", claimId, pageno, compMed);

                    var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                    return results;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<ClaimEntity> PreviewModelIODownloadPDF(string claimId, int pageno)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var path = string.Format("/api/Claim/formprocessing/getmodelinputoutput/{0}/{1}", claimId, pageno);

                    var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                    return results;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<ClaimEntity> GetClaimPdfImageApiClient(string directory, string keyname, int pageCount)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimpdfImage/{0}/{1}/{2}", directory, keyname, pageCount);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }

        public async Task<ClaimEntity> GetPdfImageApiClient(string claimId, int pageNumber)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getpdfimage/{0}/{1}", claimId, pageNumber);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }

        public async Task<ResultData> ClaimOCRAsyncApiClient(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/claimocrasync/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ResultData>(client, path);
                return results;
            }
        }

        public async Task<ResultData> ClaimOCRAsyncApiClientForMultiplePdf(ClaimRequestModel claimreqModel)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = "/api/Claim/formprocessing/claimocrasyncformultiplepdf/";
                var jsonString = JsonConvert.SerializeObject(claimreqModel);
                var results = await ApiGetClient.PostAsync<ResultData>(client, path, jsonString);
                return results;
            }
        }
        public async Task<ResultData> ClaimOCRAsyncApiClientForSinglePage(SingleClaimRequestModel singlerequest)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = "/api/Claim/formprocessing/claimocrasyncforsinglepdf/";
                var jsonString = JsonConvert.SerializeObject(singlerequest);
                var results = await ApiGetClient.PostAsync<ResultData>(client, path, jsonString);
                return results;
            }
        }
        public async Task<string> ClaimApproveprocess([FromBody] JToken ARProcess)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var path = $"/api/Claim/formprocessing/reviewstatusupdate";
                    var url = string.Format("{0}{1}", this.BaseURL, path);

                    return await ApiGetClient.PostApiClient(url, ARProcess);                    
                }
            }
            catch(Exception)
            {
                throw new ApplicationException(string.Format("Error posting data to API service"));
            }
        }
        public async Task<bool> GetClaimApproveStatus(string claimId)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetReviewStatus/{claimId}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                //var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var ButtonStatus = response.Content.ReadAsStringAsync().Result;
                    return bool.Parse(ButtonStatus);
                }
                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }
        public async Task<ResultData> DocumentClaimStartProcess(DocumentRequestModel documentRequestModel)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = "/api/Claim/formprocessing/DocumentClaimStartProcess/";
                var jsonString = JsonConvert.SerializeObject(documentRequestModel);
                var results = await ApiGetClient.PostAsync<ResultData>(client, path, jsonString);
                return results;
            }
        }

        public async Task<ClaimEntity> GetNextClaimOnPolicy(string policyNumber, string fileName, int userid)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/GetNextClaimOnPolicy/{0}/{1}/{2}", policyNumber, fileName, userid);
                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }
        public async Task<List<ProcessedPolicyClaims>> GetAllProcessedPolicyClaims(string policyNumber, int userid)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/GetAllProcessedPolicyClaims/{0}/{1}", policyNumber, userid);
                var results = await ApiGetClient.ExecuteAsync<List<ProcessedPolicyClaims>>(client, path);
                return results;
            }
        }


        public async Task<ResultData> ClaimOCRAsyncApiClientForMultiplePdfFromBucket(string bucketName, int UserId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/uploadpdftos3/{0}/{1}/{2}", UserId, bucketName, 1);

                var results = await ApiGetClient.ExecuteAsync<ResultData>(client, path);
                return results;
            }
        }
        public async Task<ResultData> ClaimOClaimOCRAsyncApiClientForSinglePage(int pageno, string claimid)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/uploadpdftos3/{0}/{1}/{2}", pageno, claimid, 1);

                var results = await ApiGetClient.ExecuteAsync<ResultData>(client, path);
                return results;
            }
        }

        public async Task<ResultData> ClaimMergeAsyncApiClient(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/claimmergeasync/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ResultData>(client, path);
                return results;
            }
        }

        public async Task<ResultData> ClaimMergeAsyncApiClientForMultiplePdf(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/claimmergeasyncformultiplepdf/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ResultData>(client, path);
                return results;
            }
        }

        public async Task<ClaimEntity> ClaimOCRFormProcessApiClient(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/claimocrformprocess/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }

        public async Task<ClaimEntity> GetModifiedClaimJsonApiClient(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimmodifiedjson/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }

        public async Task<ClaimEntity> UpdateClaimModifiedJsonApiClient(string tokenString, Guid claimid, List<EditFieldEntity> editFieldList)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/updateclaimmodifiedjson/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                ClaimEntity claimEntity = new ClaimEntity();
                claimEntity.ClaimId = claimid;
                claimEntity.EditFieldList = editFieldList;
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<ClaimEntity> GetClaimJsonApiClient(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimjson/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }

        public async Task<ProcessedClaimEntity> GetClaimJsonApiClientForMultiplePdf(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimjsonformultiple/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ProcessedClaimEntity>(client, path);
                return results;
            }
        }


        public async Task<List<TemplateIndex>> GetTemplateIndex(string claimId)
        {
            List<TemplateIndex> resultsss = new List<TemplateIndex>();
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getTemplateIndex/{0}", claimId);

                List<TemplateIndex> results = await ApiGetClient.ExecuteAsync<List<TemplateIndex>>(client, path);
                return results;
            }
        }
        public async Task<List<InterPageList>> GetInterPageOrderBy(string claimId)
        {
           
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/GetInterPageOrderBy/{0}", claimId);

                List<InterPageList> results = await ApiGetClient.ExecuteAsync<List<InterPageList>>(client, path);
            
                return results;
            }
        }
        public async Task<ClaimEntity> GetClaimJsonApiClientByPage(string claimId, int pageNo)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimjsonbypage/{0}/{1}", claimId, pageNo);

                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }
        public async Task<List<ClaimEntity>> GetClaimJsonApiClientByClaimid(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimjsonbyclaimid/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<List<ClaimEntity>>(client, path);
                return results;
            }
        }
        public async Task<ClaimEntity> DeleteRow(Guid claimid, List<EditFieldEntity> editField)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/deleterow/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                ClaimEntity claimEntity = new ClaimEntity();
                claimEntity.ClaimId = claimid;
                claimEntity.EditFieldList = editField;
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<ClaimEntity> DeleteClaimForm(string claimid)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/removeclaimpdf/{claimid}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<ClaimEntity> CancelClaimForm(string claimid)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/cancelclaimpdf/{claimid}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<ClaimEntity> AddMissedField(Guid claimid, List<EditFieldEntity> editField)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/addmissedfield/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                ClaimEntity claimEntity = new ClaimEntity();
                claimEntity.ClaimId = claimid;
                claimEntity.EditFieldList = editField;
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }
        public async Task<ClaimEntity> AddMissedField_Index(Guid claimid, List<EditFieldEntity> editField)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/Claim/formprocessing/addmissedfield_Index/claimdata";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                ClaimEntity claimEntity = new ClaimEntity();
                claimEntity.ClaimId = claimid;
                claimEntity.EditFieldList = editField;
                var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                HttpContent content = new StringContent(claimdataJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }
        public async Task<ClaimEntity> IgnoreField(string fieldName)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/ignoreexistfield";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent("{fieldName : " + fieldName + "}");
                //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                content.Headers.Add("fieldName", fieldName);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }
        [HttpPost]
        public async Task<CommonEntity> DeleteAllClaim(string claimId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var path = "/api/Claim/formprocessing/deleteallclaim/claimdata";
                    var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                    client.DefaultRequestHeaders.Accept.Clear();
                    ClaimEntity claimEntity = new ClaimEntity();
                    claimEntity.ClaimIds = JsonConvert.DeserializeObject<List<string>>(claimId);
                    var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                    HttpContent content = new StringContent(claimdataJson);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonAsString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<CommonEntity>(jsonAsString);
                    }

                    throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
                }


            }
            catch (Exception ex)
            {
                return new CommonEntity();
            }
        }
        public async Task<CommonEntity> DeleteSelectedClaims(string[] claimIds)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var path = "/api/Claim/formprocessing/deleteallclaim/claimdata";
                    var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                    client.DefaultRequestHeaders.Accept.Clear();
                    ClaimEntity claimEntity = new ClaimEntity();
                    claimEntity.ClaimIds = claimIds.ToList<string>();
                    var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                    HttpContent content = new StringContent(claimdataJson);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonAsString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<CommonEntity>(jsonAsString);
                    }

                    throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
                }


            }
            catch (Exception ex)
            {
                return new CommonEntity();
            }
        }

        public async Task<string> deletePostMessage(string claimId)
        {
            try
            {
              
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                    var path = string.Format("/api/Claim/formprocessing/PosDeleteMessage/{0}", claimId);
                    var results = await ApiGetClient.ExecuteAsync<string>(client, path);
                    return results;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<CommonEntity> AddUpdateSpecialCharacter(string character, string description, bool isupdate)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/addupdatespecialchar/{character}/{description}/{isupdate}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<ClaimEntity>(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public List<string> GetDateFieldJson()
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetDateFieldJson";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;



                    return JsonConvert.DeserializeObject<List<string>>(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }
        public List<string> GetSsnFieldJson()
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetSsnFieldJson";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;



                    return JsonConvert.DeserializeObject<List<string>>(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }
        public List<string> GetTeleFieldJson()
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetTeleFieldJson";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;



                    return JsonConvert.DeserializeObject<List<string>>(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }

        public JToken GetStructuredJson(string claimId)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetStructuredJsonNew/{claimId}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }

        public JToken GetStructuredCsv(string claimId)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetStructuredCsv/{claimId}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }

        public JToken PrincipalAPISubmit(string claimId)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/PrincipalAPISubmit/{claimId}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                HttpContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.PostAsync(uri, content).Result;
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(result);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }

        public JToken GetClaimFileName(string claimId)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetClaimFIleName/{claimId}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }

        public JToken GetClaimFileInfo(string claimId)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetClaimFileInfo/{claimId}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = client.GetAsync(uri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString);
                }
                throw new ApplicationException(string.Format("Error getting data to API service with URL: {0}", path));
            }
        }

        public async Task<ViewClaimDetails> GetClaimResultJsonApiClient(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimjson/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<ViewClaimDetails>(client, path);
                return results;
            }
        }

        public async Task<bool> TemplateMail(string Email, string clientName, string claimFileName, string ClaimId, string TemplateName, string PageNo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/TemplateMailRequest/{0}/{1}/{2}/{3}/{4}/{5}", Email, clientName, claimFileName, ClaimId, TemplateName, PageNo);
                bool results = await ApiGetClient.ExecuteAsync<bool>(client, path);
                return results;
            }
        }

        public async Task<bool> UpdateUserZoomLevel(float zoomLevel, int userId)
        {
            using (var client = new HttpClient())
            {
                var path = "/api/User/formprocessing/UpdateUserZoomLevel";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                client.DefaultRequestHeaders.Accept.Clear();
                var configJson = JsonConvert.SerializeObject(new
                {
                    ZoomLevel = zoomLevel,
                    UserId = userId
                });
                HttpContent content = new StringContent(configJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var response = await client.PostAsync(uri, content);
                return true;
            }
        }
        public async Task<float> GetUserZoomLevel(int userId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/GetUserZoomLevel/{0}", userId);
                var results = await ApiGetClient.ExecuteAsync<float>(client, path);
                return results;
            }
        }
        public async Task<List<ModelTypesErrorItem>> GetModelTypesError(string claimId)
        {
            List<ModelTypesErrorItem> resultsss = new List<ModelTypesErrorItem>();
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimerrorlistbyclaimid/{0}", claimId);

                var results = await ApiGetClient.ExecuteAsync<List<ModelTypesErrorItem>>(client, path);
                return results;

            }
        }

        public async Task<List<ClaimModelInput>> GetModelInput(string claimId, int pageNo)
        {
            List<ClaimModelInput> resultsss = new List<ClaimModelInput>();
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/getclaimmodelinputbyclaimid/{0}/{1}", claimId, pageNo);

                List<ClaimModelInput> results = await ApiGetClient.ExecuteAsync<List<ClaimModelInput>>(client, path);
                return results;

            }
        }

        public async Task<JToken> GetMedicalData(string claimId)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/GetMedicalData/{claimId}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<JToken> SetMedicalData(JToken medAlert)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/SetMedicalData";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent(medAlert.ToString());
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        //public async Task<JToken> SetMedicalDataPaymentUpadte(JToken medAlert)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        var path = $"/api/Claim/formprocessing/SetMedicalDataPaymentUpadte";
        //        var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
        //        HttpContent content = new StringContent(medAlert.ToString());
        //        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

        //        var response = await client.PostAsync(uri, content);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var jsonAsString = response.Content.ReadAsStringAsync().Result;
        //            return JToken.Parse(jsonAsString);
        //        }

        //        throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
        //    }
        //}
        public async Task<JToken> SetMedicalDataPaymentUpadte([FromBody]JToken medAlert)
        {
            try
            {
                var path = "/api/Claim/formprocessing/SetMedicalDataPaymentUpadte";
                return await ApiClientPost<JToken>(path, medAlert);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JToken> RemoveMedicalData(JToken medAlert)
        {
            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/RemoveMedicalData";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent(medAlert.ToString());
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString);
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        public async Task<JToken> GetMedicalAutoComplete()
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/GetMedicalAutoComplete");

                var results = await ApiGetClient.ExecuteAsync<JToken>(client, path);
                return results;

            }
        }
      public async Task<bool> NonMatchingTemplateMail(string clientName, string claimFileName, string ClaimId)
        {
          

                var path = string.Format("/api/User/formprocessing/NonMatchingTemplateMail/{0}/{1}/{2}", clientName, claimFileName, ClaimId);
                bool results = await this.ClientAPIGet<bool>(path);
                return results;
          
        }
       
        public async Task<List<PostUserMessage>> GetClaimPostMessageApi( string ClaimId)
        {
            var response = new List<PostUserMessage>();
           
                var path = string.Format("/api/Claim/formprocessing/GetClaimPostMessage/{0}",  ClaimId);
                response = await this.ClientAPIGet<List<PostUserMessage>>(path);
                return response;
           
        }

        public async Task<bool> UpdateClaimTypeApi(string claimId,int ClaimTypeId)
        {
            var path = string.Format("/api/Claim/formprocessing/UpdateClaimType/{0}/{1}", claimId, ClaimTypeId);
            var response = await this.ClientAPIGet<bool>(path);
            return response;
        }

        public async Task<bool> NonTemplatePagesMail(string clientName, string claimFileName, string ClaimId, string pageNo)
        {
         
             
                var path = string.Format("/api/User/formprocessing/NonTemplatePagesMail/{0}/{1}/{2}/{3}", clientName, claimFileName, ClaimId, pageNo);
             
                bool results = await this.ClientAPIGet<bool>(path);
                return results;
            
        }

        public List<TempalteModel> CheckTemplateAlign(string claimId)
        {
            List<TempalteModel> resultsss = new List<TempalteModel>();
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromMilliseconds(Timeout.Infinite);
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format("/api/Claim/formprocessing/checkTemplateModel/{0}", claimId);

                List<TempalteModel> results = ApiGetClient.Execute<List<TempalteModel>>(client, path);
                return results;
            }

        }

        public async Task<bool> TemplateModelPipeLine(string clientName, string claimFileName, string ClaimId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var path = string.Format("/api/User/formprocessing/CheckTemplatePipelineMail/{0}/{1}/{2}", clientName, claimFileName, ClaimId);
                bool results = await ApiGetClient.ExecuteAsync<bool>(client, path);
                return results;
            }
        }

        [HttpPost]
        public async Task<bool> ShareClaimDocument([FromBody] JToken sharedDocument)
        {
            try
            {
                var path = "/api/Claim/formprocessing/shareclaimdocument";
                var url = string.Format("{0}{1}", this.BaseURL, path);

                var result = await ApiGetClient.PostApiClient(url, sharedDocument);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       
        public async Task<List<UserEntityData>> SharedFiluserBasedClaimType([FromBody] JToken shareData)
        {
            //try
            //{


               string[] claimIds = shareData["claimIds"].ToObject<string[]>();

            //    var path = string.Format("/api/User/formprocessing/sharedFiluserBasedClaimType/{0}", shareData.ToString);
            //    var response = await this.ClientAPIGet<List<UserEntityData>>(path);
            //    return response;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
            try
            {
                using (var client = new HttpClient())
                {
                    var path = "/api/User/formprocessing/sharedFiluserBasedClaimType";
                    var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                    client.DefaultRequestHeaders.Accept.Clear();
                    ClaimEntity claimEntity = new ClaimEntity();
                    claimEntity.ClaimIds = claimIds.ToList<string>();
                    var claimdataJson = JsonConvert.SerializeObject(claimEntity);

                    HttpContent content = new StringContent(claimdataJson);
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                    var response = await client.PostAsync(uri, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonAsString = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<List<UserEntityData>>(jsonAsString);
                    }

                    throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ClaimEntity> GetClaimShareDetails(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                var path = string.Format("/api/Claim/formprocessing/GetClaimShareDetails/{0}", claimId);
                var results = await ApiGetClient.ExecuteAsync<ClaimEntity>(client, path);
                return results;
            }
        }

        public async Task<JToken> UpdateTemplateNameApiClient(string tokenString, string claimId, int pageNo,string templateName)
        {

            using (var client = new HttpClient())
            {
                var path = $"/api/Claim/formprocessing/UpdateTemplateName/{claimId}/{pageNo}/{templateName}";
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                HttpContent content = new StringContent("");
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = response.Content.ReadAsStringAsync().Result;
                    return JToken.Parse(jsonAsString); // return value is boolean
                }

                throw new ApplicationException(string.Format("Error posting data to API service with URL: {0}", path));
            }
        }

        [HttpPost]
        public async Task<bool> UnShareClaimDocument(List<Guid> claimIds)
        {
            try
            {
                var path = "/api/Claim/formprocessing/unshareclaimdocument/claimdata";
                var url = string.Format("{0}{1}", this.BaseURL, path);

                var result = await ApiGetClient.PostApiClient(url, claimIds);

                var jresult = JToken.Parse(result);
                return jresult.Value<bool>();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<UserEntityData>> GetSharedDetailData(string claimId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var path = string.Format($"/api/Claim/formprocessing/getshareddetaildata/{claimId}");

                var results = await ApiGetClient.ExecuteAsync<List<UserEntityData>>(client, path);
                return results;
            }
        }

        public async Task<JObject> GetMedicalJsonData(string clientName)
        {
            try
            {
                var path = string.Format($"/api/Claim/formprocessing/getmedicalpaymentdata/{clientName}");
                return await ApiClientGet<JObject>(path);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JObject> RemoveMedicalPaymentData([FromBody] JToken paymentData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/removemedicalpaymentdata";
                return await ApiClientPost<JObject>(path, paymentData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public async Task<JObject> RemoveTerminologyData([FromBody] JToken TerminologyData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/removemeterminologydata";
                return await ApiClientPost<JObject>(path, TerminologyData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JObject> SetMedicalPaymentData([FromBody] JToken paymentData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/setmedicalpaymentdata";
                return await ApiClientPost<JObject>(path, paymentData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JObject> SetTerminologyData([FromBody] JToken TerminologyData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/setterminologydata";
                return await ApiClientPost<JObject>(path, TerminologyData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JObject> SetMedicalContractData([FromBody] JToken contractData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/setmedicalcontractdata";
                return await ApiClientPost<JObject>(path, contractData);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<JObject> RemoveContractData([FromBody] JToken contractData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/removecontractdata";
                return await ApiClientPost<JObject>(path, contractData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JObject> SetFieldTypeData([FromBody] JToken FieldTypeData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/setfieldtypedata";
                return await ApiClientPost<JObject>(path, FieldTypeData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<JObject> RemoveFieldTypeData([FromBody] JToken FieldTypeData)
        {
            try
            {
                var path = "/api/Claim/formprocessing/removefieldtypedata";
                return await ApiClientPost<JObject>(path, FieldTypeData);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<T> ApiClientGet<T>(string path)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.BaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);
                    return await ApiGetClient.ExecuteAsync<T>(client, path);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<T> ApiClientPost<T>(string path, object contentObj)
        {
            T output = default(T);

            using (var client = new HttpClient())
            {
                var uri = new Uri(string.Format("{0}{1}", this.BaseURL, path));
                var contentJson = contentObj != null ? JsonConvert.SerializeObject(contentObj) : "";
                HttpContent content = new StringContent(contentJson);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionObj.TokenString);

                var response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonAsString = await response.Content.ReadAsStringAsync();
                    output = JsonConvert.DeserializeObject<T>(jsonAsString);
                }
            }

            return output;
        }

        public string GetContractLookupId(string claimId)
        {
            try
            {
                var path = string.Format($"/api/Claim/formprocessing/getcontractlookupid/{claimId}");
                return ApiClientGet<string>(path).Result.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

