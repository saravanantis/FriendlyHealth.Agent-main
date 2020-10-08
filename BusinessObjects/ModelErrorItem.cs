using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class ModelErrorItem
    {
        public Guid ClaimId { get; set; }

        public int PageNo { get; set; }

        public List<string> Errors { get; set; }
    }
}
