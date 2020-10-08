using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class InterPageEntity
    {
      
    }
    public class FormTypes
    {
        public string FormType { get; set; }
        public List<int> PageNums { get; set; }

    }

    public class MyArray
    {
        public string Applicant { get; set; }
        public List<FormTypes> FormTypes { get; set; }

    }

    public class Root
    {
        public List<MyArray> MyArray { get; set; }

    }
    public partial class InterPageList
    {
        public string Applicant { get; set; }
        public List<FormTypeList> FormTypes { get; set; }
    }

    public partial class FormTypeList
    {
        public string FormType { get; set; }
        public List<int> PageNums { get; set; }
    }
    public class InterPageUserwise
    {
        public string Applicant { get; set; }
        public List<int> PageNums { get; set; }

    }
}
