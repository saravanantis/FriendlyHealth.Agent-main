using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class Review
    {
        public int PdfNameListCount { get; set; }

        public List<Fields> PdfNameList { get; set; }
    }
}
