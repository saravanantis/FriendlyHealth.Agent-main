using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class UserConfigUpdates
    {
        public int id { get; set; }
        public string userid { get; set; }
        public string oldvalue { get; set; }
        public string newvalue { get; set; }
        public DateTime datetime { get; set; }
    }
}
