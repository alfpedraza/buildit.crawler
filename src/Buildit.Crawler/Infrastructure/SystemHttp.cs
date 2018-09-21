using System;
using System.Net.Http;

namespace Buildit.Crawler.Infrastructure
{
    public class SystemHttp : IHttp
    {
        public HttpGetResponse Get(Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                using (var httpResponse = httpClient.GetAsync(uri).Result)
                {
                    var response = new HttpGetResponse();
                    response.IsSuccess = httpResponse.IsSuccessStatusCode;
                    response.Content = httpResponse.Content.ReadAsStringAsync().Result;
                    response.RequestUri = httpResponse.RequestMessage.RequestUri;
                    return response;
                }
            }
        }
    }
}
