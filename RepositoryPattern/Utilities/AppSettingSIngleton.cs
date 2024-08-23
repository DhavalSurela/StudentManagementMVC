using System.Diagnostics.Metrics;

namespace RepositoryPattern.Utilities
{
    public sealed class AppSettingSIngleton
    {
        private static AppSettingSIngleton Instance = null;

        private readonly IConfiguration _configuration;
        public static AppSettingSIngleton GetInstance(IConfiguration configuration)
        {

            if (Instance == null)
            {
                Instance = new AppSettingSIngleton(configuration);
            }
            return Instance;
        }


        private AppSettingSIngleton(IConfiguration configuration)
        {

            _configuration = configuration;
        }

        public string JwtKey
        {
            get
            {
                return Convert.ToString(_configuration.GetValue<string>("JwtKey"));
            }
        }
        public string JwtExpireDays
        {
            get
            {
                return Convert.ToString(_configuration.GetValue<string>("JwtExpireDays"));
            }
        }
        public string JwtIssuer
        {
            get
            {
                return Convert.ToString(_configuration.GetValue<string>("JwtIssuer"));
            }
        }
        public string JwtAudience
        {
            get
            {
                return Convert.ToString(_configuration.GetValue<string>("JwtAudience"));
            }
        }


    }
}
