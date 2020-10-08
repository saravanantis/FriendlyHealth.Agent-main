using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiClient
{
    public class ResultModel
    {
        public bool Status { get; set; }
        public bool ShowConfirmForm { get; set; }
        public string ErrorMessage { get; set; }
        public bool ForgotPasswordConfirmed { get; set; }
        public bool ForgotPasswordRequestStatus { get; set; }
        public string TokenString { get; set; }
        public bool ResetPasswordStatus { get; set; }
    }
}
