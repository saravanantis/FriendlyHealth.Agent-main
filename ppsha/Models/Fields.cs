using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Fields
    {
        public Fields()
        {
            this.TableNum = -1;
            this.RowNum = -1;
        }

        public string Category { get; set; }

        public string FieldName { get; set; }

        public int FieldNameWithoutExtension { get; set; }

        public string FieldValue { get; set; }

        public string ClaimId { get; set; }

        public string ExtractValue { get; set; }

        public string ExtractName { get; set; }

        public string NameBoundaries { get; set; }

        public string ValueBoundaries { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string FormattedCreatedDate { get; set; }

        public string FormattedModifiedDate { get; set; }

        public int TableNum { get; set; }

        public int RowNum { get; set; }

        public float FieldNameConfidence { get; set; }

        public float FieldValueConfidence { get; set; }

        public bool IsDeleted { get; set; }

        public string ProcessStatus { get; set; }

        public int ProcessedPercent { get; set; }

        public string ConditionAlertText { get; set; }
        public string ConditionAlertGreenText { get; set; }
        public string ConditionAlertYellowText { get; set; }

        public List<ModelErrorItem> ModelErrorList { get; set; }

        public string CompanyID { get; set; }

        public string ContractNumber { get; set; }

        public string FormMetaTag { get; set; }

        public string RevisionMetaTag { get; set; }

        public string ErrorHeader { get; set; }

        public string FormattedErrors { get; set; }

        public string FormType { get; set; }

        public string SectionName { get; set; }


        public bool IsDebugMode { get; set; }

        public int TotalPages { get; set; }
        public int ErrorCounts { get; set; }
        public int TypeOfClaimId { get; set; }
        public string TypeOfClaimName { get; set; }
        public bool InUse { get; set; }

        public bool Flag { get; set; }
        public string PostCheckType { get; set; }
        public double ThresholdValue { get; set; }
        public bool IsUpdated { get; set; }
        public bool Hidden { get; set; }
        public bool Mandatory { get; set; }

        public bool isTemplate { get; set; }
        public bool SharedFrom { get; set; }
        public bool SharedTo { get; set; }
        public string PostListMessage { get; set; }

        public bool IsManualReview { get; set; }

        public string ManualReviewPages { get; set; }


    }

}
