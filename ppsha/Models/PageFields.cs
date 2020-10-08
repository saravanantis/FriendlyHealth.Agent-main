using ppsha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class PageFields
    {
        public PageFields()
        {
            PageFieldsList = new List<Fields>();
        }
        public int PageNo { get; set; }

        public string PageStatus { get; set; }

        public List<Fields> PageFieldsList { get; set; }

        public string MLFieldMatch { get; set; }

        public string Template { get; set; }

        public string TemplateIndex { get; set; }

        public string PageClassifier { get; set; }

        public double ThresholdValue { get; set; }

        public bool CorrectTemplate { get; set; }

        public bool SegmentationProcess { get; set; }

        public string SegmentationResult { get; set; }

        public double Readability { get; set; }

        public List<SplCharToText> LstSplCharToText { get; set; }

        public List<PrinciplAPIValue> PrinciplAPIValueList { get; set; }

        public List<AlertItem> ConditionAlertstList { get; set; }

        public string ConditionAlertText { get; set; }

        public string ConditionAlertGreenText { get; set; }

        public string ConditionAlertYellowText { get; set; }

        public bool isLocked { get; set; }

        public bool IsRequiredType { get; set; }
    }
}
