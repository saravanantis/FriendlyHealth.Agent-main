using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class ClaimModelInput
    {
        public Guid ClaimId { get; set; }

        public int PageNo { get; set; }

        public string ModelName { get; set; }

        public string ModelInput { get; set; }

        public string ModelOutput { get; set; }

    }
}
