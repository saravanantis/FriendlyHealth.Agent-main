using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class ModelTypesErrorItem
    {
        public Guid ClaimId { get; set; }

        public int PageNo { get; set; }

        public int ModelErrorID { get; set; }

        public string ModelErrorName { get; set; }

        public string ErrorDescription { get; set; }

        public DateTime Createdtime { get; set; }
    }
}
