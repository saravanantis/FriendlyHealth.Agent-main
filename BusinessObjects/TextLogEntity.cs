using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class TextLogEntity : CommonEntity
    {
        public string FileName { get; set; }
        public DateTime Date { get; set; }        
        public string LogTime { get; set; }
    }
}
