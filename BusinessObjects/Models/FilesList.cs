﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class FilesList
    {
        public long CLAIMPDFSIZE { get; set; }

        public string CLAIMPDFNAME { get; set; }

        public byte[] CLAIMPDF { get; set; }
    }
}
