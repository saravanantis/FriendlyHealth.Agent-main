using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ClaimReview
    {
        public ClaimReview()
        {
            PdfImageDataListURL = new List<string>();
        }
        public Review PdfClaimNameList { get; set; }

        public Review PdfContractNameList { get; set; }

        public string PdfClaimFileURL { get; set; }

        public string PdfContractFileURL { get; set; }

        public List<PageFields> FieldsList { get; set; }

        public string PdfClaimImageFileURL { get; set; }

        public byte[] PdfData { get; set; }

        public string ClaimId { get; set; }

        public byte[] PdfImageData { get; set; }

        public List<string> PdfImageDataListURL { get; set; }

        public ErrorMessages ErrorMessages { get; set; }
       
        public string DateCheck { get; set; }

        public int PageNo { get; set; }
    }
}
