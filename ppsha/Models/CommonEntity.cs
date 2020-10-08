using System;

namespace ppsha.Models
{
    public class CommonEntity
    {
        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Message { get; set; }

        public bool StatusCode { get; set; }
    }
}
