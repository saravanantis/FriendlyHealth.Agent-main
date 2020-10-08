using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessObjects
{
    public class SingleSignOnSettings
    {
        public string IsSingleSignOnEnabled { get; set; }
        public string AuthorizeRequestUrl { get; set; }
        public string AccessTokenUrl { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public string ClientSecret { get; set; }
        public string LogOutRequestUrl { get; set; }
    }
}
