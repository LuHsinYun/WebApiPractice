using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.Entity.ApiResponses;
using BussinessLogic.Entity.GovDatas;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BussinessLogic.Service.Apis
{
    /// <summary>
    /// 交通部「運輸資料流通服務平臺(TDX)」 Service
    /// </summary>
    public class TDXApiService
    {
        /// <summary>
        /// IHttpClientFactory
        /// </summary>
        private IHttpClientFactory _clientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TDXApiService" /> class.
        /// </summary>
        public TDXApiService()
        {
            var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
            this._clientFactory = serviceProvider.GetService<IHttpClientFactory>();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="parameters">Parameters</param>
        /// <param name="requestUri">Uri</param>
        /// <param name="token">Token</param>
        /// <returns>Response</returns>
        public async Task<T> Get<T>(Dictionary<string, string> parameters, string requestUri, string token)
        {
            var client = _clientFactory.CreateClient();

            if (!string.IsNullOrWhiteSpace(token))
            {
                client.DefaultRequestHeaders.Add("authorization", $"Bearer {token}");
                client.DefaultRequestHeaders.Add("Accept-Encoding", "br,gzip");
            }

            if (parameters.Any())
            {
                var strParam = string.Join("&", parameters.Where(d => !string.IsNullOrWhiteSpace(d.Value)).Select(o => o.Key + "=" + o.Value));
                requestUri = string.Concat(requestUri, '?', strParam);
            }

            client.BaseAddress = new Uri(requestUri);

            var response = await client.GetAsync(requestUri).ConfigureAwait(false);

            var responseContent = await DecompressResponse(response);

            var result = JsonConvert.DeserializeObject<T>(responseContent);

            return result;
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

        public async Task<AccessTokenEntity> GetToken(string requestUri)
        {
            string baseUrl = $"https://tdx.transportdata.tw/auth/realms/TDXConnect/protocol/openid-connect/token";

            var parameters = new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials"},
                { "client_id", "XXXXXXXXXX-XXXXXXXX-XXXX-XXXX" },
                { "client_secret", "XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX"}
            };

            var formData = new FormUrlEncodedContent(parameters);

            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            client.DefaultRequestHeaders.Add("Accept-Encoding", "br,gzip");
            var response = await client.PostAsync(baseUrl, formData);

            var responseContent = await DecompressResponse(response);

            return JsonConvert.DeserializeObject<AccessTokenEntity>(responseContent);
        }

        public async Task<string> DecompressResponse(HttpResponseMessage response)
        {
            //if (response.Content.Headers.ContentEncoding.Contains("br"))
            //{
            //    using (var stream = new BrotliStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
            //    {
            //        using (var reader = new StreamReader(stream))
            //        {
            //            return await reader.ReadToEndAsync();
            //        }
            //    }
            //}
            //else
            if (response.Content.Headers.ContentEncoding.Contains("gzip"))
            {
                using (var stream = new GZipStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(stream))
                    {
                        return await reader.ReadToEndAsync();
                    }
                }
            }
            else
            {
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}