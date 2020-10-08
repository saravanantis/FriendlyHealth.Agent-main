using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class ConfigurationEntity
    {
        public string Environment { get; set; }

        public Dictionary<string, string> Settings { get; set; }

        public bool StatusCode { get; set; }

        public string Message { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
