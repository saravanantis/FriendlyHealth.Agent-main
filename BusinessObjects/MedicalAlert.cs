using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{

    public class MedicalAlert
    {
        public MedicalAlert()
        {
            EligiblePaymentDetails = new List<AlertMessage>();
            ApprovedPaymentDetails = new List<AlertMessage>();
            Data = new List<MedData>();
        }

        public string ClaimantName { get; set; }
        public string DependentName { get; set; }
        public string AccidentDescription { get; set; }
        public string EligiblePayment { get; set; }
        public List<AlertMessage> EligiblePaymentDetails { get; set; }
        public string ApprovedPayment { get; set; }
        public List<AlertMessage> ApprovedPaymentDetails { get; set; }
        public List<MedData> Data { get; set; }

        public class AlertMessage
        {
            public string Message { get; set; }
            public bool IsMatch { get; set; }
        }

        public class MedData
        {
            public string Type { get; set; }
            public string Fieldname { get; set; }
            public string Fieldvalue { get; set; }
            public List<string> Payment { get; set; }
            public bool IsMatch { get; set; }
            [JsonProperty("user_action")]
            public UserAction UserAction { get; set; }
        }

        public class UserAction
        {
            public string diagnosisAction { get; set; }
            public string paymentAction { get; set; }
            public bool isImportant { get; set; }
        }
    }

}
