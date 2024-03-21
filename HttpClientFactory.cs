namespace Backend
{
    public class HttpClientFactory
    {
        private static HttpClient _instance;
        private static readonly object _lock = new object();

        public static HttpClient GetInstance(ExternalApiIpstackOptions options)
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new HttpClient();
                    _instance.DefaultRequestHeaders.Add("ApiKey", options.ApiKey);
                }
            }

            return _instance;
        }
    }
}
