using BussinessLogic.Entity.ApiResponses;

namespace BussinessLogic.Service.Apis
{
    internal interface IHttpRequestService
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="endPoint">End Point</param>
        /// <returns>Response</returns>
        ApiResponseBaseEntity<T> Get<T>(string endPoint);

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="T">Response Type</typeparam>
        /// <param name="request">Request</param>
        /// <param name="endPoint">End Point</param>
        /// <returns>Response</returns>
        ApiResponseBaseEntity<T1> Post<T, T1>(T request, string endPoint);
    }
}