using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using BussinessLogic.Entity.ApiResponses;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BussinessLogic.Service.Apis
{
    /// <summary>
    /// HttpRequest Utility
    /// </summary>
    public class HttpRequestService : IHttpRequestService
    {
        /// <summary>
        /// IHttpClientFactory
        /// </summary>
        private IHttpClientFactory _clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestService" /> class.
        /// </summary>
        public HttpRequestService()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            this._clientFactory = serviceProvider.GetService<IHttpClientFactory>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="endPoint">End Point</param>
        /// <returns>Response</returns>
        public ApiResponseBaseEntity<T> Get<T>(string endPoint)
        {
            var client = this._clientFactory.CreateClient();

            //client.DefaultRequestHeaders.Add("Key", "Value");
            //client.Timeout = TimeSpan.FromMilliseconds(1000);

            var responseMessage = client.GetAsync(endPoint).GetAwaiter().GetResult();

            if (responseMessage.IsSuccessStatusCode)
            {
                var reponseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var result = JsonConvert.DeserializeObject<T>(reponseContent);
                return new ApiResponseBaseEntity<T>()
                {
                    Status = ApiStatusEnum.Success,
                    ErrorMessage = string.Empty,
                    Data = result
                };
            }

            throw new ApplicationException($"url:{endPoint} response:{responseMessage.StatusCode}");
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="request">Request</param>
        /// <param name="endPoint">End Point</param>
        /// <returns>Response</returns>
        public ApiResponseBaseEntity<T1> Post<T, T1>(T request, string endPoint)
        {
            var client = this._clientFactory.CreateClient();

            var requestJsonString = JsonConvert.SerializeObject(request);

            //client.DefaultRequestHeaders.Add("Key", "Value");
            //client.Timeout = TimeSpan.FromMilliseconds(1000);

            var buffer = Encoding.UTF8.GetBytes(requestJsonString);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var responseMessage = client.PostAsync(endPoint, byteContent).GetAwaiter().GetResult();

            if (responseMessage.IsSuccessStatusCode)
            {
                var reponseContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                var result = JsonConvert.DeserializeObject<T1>(reponseContent);
                return new ApiResponseBaseEntity<T1>()
                {
                    Status = ApiStatusEnum.Success,
                    ErrorMessage = string.Empty,
                    Data = result
                };
            }

            throw new ApplicationException($"url:{endPoint} response:{responseMessage.StatusCode}");
        }
    }
}