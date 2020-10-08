
namespace ppsha.Models
{
    public class AppSettings
    {
        public AppSettings(string connection)
        {
            StorageConnectionString = connection;
        }

        public string StorageConnectionString { get; set; }
    }
}
