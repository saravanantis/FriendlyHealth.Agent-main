
namespace ppsha.Models
{
    public class MySettings
    {
        public string BaseURL { get; set; }

        public string GoogleAPIKey { get; set; } = "";

        public string GoogleAPILink { get; set; } = "";

        public string Environment { get; set; }

        public string EnvironmentName { get; set; } = "CLIENT_CONFIG_FILE";

        public string APIConnectionString { get; set; } = "API_BASE_URL";

        public string AllowedOrigins { get; set; } = "ALLOWED_ORIGINS";

        public bool OnPremise { get; set; }
        public string UserEmail { get; set; }
        public string UserPassword { get; set; }
        public string Service_Thread_Sleep { get; set; }
        public string VPUsernName { get; set; }
        public string VPPassword { get; set; }
    }
}
