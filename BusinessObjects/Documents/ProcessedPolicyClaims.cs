using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects.Documents
{
    public class ProcessedPolicyClaims
    {
        public DateTime CreatedDate { get; set; }
        public Guid ClaimId { get; set; }
        public string FileName { get; set; }
        public string ContractNumber { get; set; }
        public string FormMetaTag { get; set; }
        public string RevisionMetaTag { get; set; }
    }
}
