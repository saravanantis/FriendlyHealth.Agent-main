using System;
using System.Collections.Generic;

namespace ppsha.Models
{
    public class ClaimEntity : CommonEntity
    {
        public ClaimEntity()
        {
            ProcessedPages = new List<int>();
            NonProcessedPages = new List<int>();
        }

        public Guid ClaimId { get; set; }

        public Guid ContractId { get; set; }

        public int UserId { get; set; }

        public int TypeOfClaimId { get; set; }

        public string TypeOfClaimName { get; set; }

        public string ClaimPdf { get; set; }

        public string PdfSize { get; set; }

        public byte[] ClaimPdfData { get; set; }

        public byte[] ClaimPdfImageData { get; set; }

        public string StructuredJson { get; set; }

        public int TotalPages { get; set; }

        public int Status { get; set; }

        public int PDFId { get; set; }

        public string PDFName { get; set; }

        public int PDFNameWithoutExtension { get; set; }

        public bool IsDeleted { get; set; }

        public string S3FileName { get; set; }

        public string S3Url { get; set; }

        public string ExtractFieldBoundaryList { get; set; }

        public string FieldBoundariesList { get; set; }

        public string PrincipalAPIMatchList { get; set; }

        public string PrincipalAPISubmit { get; set; }

        public string ConditionAlertstList { get; set; }

        public string ModelErrorList { get; set; }

        public List<EditFieldEntity> EditFieldList { get; set; }


        public string ImageStatus { get; set; }

        public List<byte[]> ClaimPdfByteDataList { get; set; }

        public List<int> ProcessedPages { get; set; }

        public List<int> NonProcessedPages { get; set; }

        public bool IsPageEmpty { get; set; }

        public string MLResult { get; set; }

        public string TemplateResult { get; set; }
        public string SegmentationResult { get; set; }
        public bool SegmentationProcess { get; set; }


        public List<SplCharToText> LstSplCharToText { get; set; }

        public string ProcessStatus { get; set; }

        public int ProcessedPercent { get; set; }

        public List<string> ClaimIds { get; set; }

        public string TemplateIndex { get; set; }

        public string PageClassifier { get; set; }

        public string CompanyID { get; set; }

        public string ContractNumber { get; set; }

        public string FormMetaTag { get; set; }

        public string RevisionMetaTag { get; set; }

        public double ThresholdValue { get; set; }

        public bool CorrectTemplate { get; set; }

        public double Flag { get; set; }

        public bool IsUpdated { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsDebug { get; set; }

        public int PageNumber { get; set; }

        public double Readability { get; set; }

        public string DocumentStatus { get; set; }

        public bool isLocked { get; set; }

        public bool isTemplate { get; set; }

        public bool IsShared { get; set; }

        public int ModifiedBy { get; set; }

        public int EditTimeInterval { get; set; }

        public bool IsManualReview { get; set; }

        public bool IsRequiredType { get; set; }

        public string ManualReviewPages { get; set; }
        public long DocumentId { get; set; } = 0;

        public long DocumentQueueEntryId { get; set; } = 0;

        public bool IsDocumentPostBack { get; set; }
        public string ClientJson { get; set; }

        public List<PostUserMessage> PostMessage { get; set; }

    }

    public class SplCharToText
    {
        public string specialcharacter { get; set; }

        public string textabbrevations { get; set; }
    }
}
