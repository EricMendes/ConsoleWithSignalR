using Microsoft.Extensions.Configuration;

namespace ConsoleHub
{
    public class AppSettings
    {
        private readonly IConfiguration _config;
        public AppSettings(IConfiguration config)
        {
            _config = config;
        }

        public string HubUrl
        {
            get 
            {
                return _config.GetValue<string>("HubUrl");
            }
        }
    }
}