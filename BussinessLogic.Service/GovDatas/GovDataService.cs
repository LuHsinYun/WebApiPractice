using System.Collections.Generic;
using BussinessLogic.Entity.ApiResponses;
using BussinessLogic.Entity.GovDatas;
using BussinessLogic.Service.Apis;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace BussinessLogic.Service.GovDatas
{
    /// <summary>
    /// 政府資料開放平台 Service
    /// </summary>
    public class GovDataService : IGovDataService
    {
        /// <summary>
        /// Http Reqeust Service
        /// </summary>
        private IHttpRequestService _httpRequestService;

        public GovDataService()
        {
            this._httpRequestService = new HttpRequestService();
        }

        /// <summary>
        /// 取得YouBike2.0臺北市公共自行車即時資訊
        /// </summary>
        /// <returns>YouBike2.0臺北市公共自行車即時資訊</returns>
        public ApiResponseBaseEntity<List<Youbike2ResponseEntity>> GetYoubike2Data()
        {
            var url = "https://tcgbusfs.blob.core.windows.net/dotapp/youbike/v2/youbike_immediate.json";

            var dataList = this._httpRequestService.Get<List<Youbike2ResponseEntity>>(url);

            if (dataList != null)
            {
                return dataList;
            }

            return new ApiResponseBaseEntity<List<Youbike2ResponseEntity>>()
            {
                Status = ApiStatusEnum.Failure,
                Data = null,
                ErrorMessage = "查無資料"
            };
        }

        /// <summary>
        /// 取得YouBike2.0臺北市公共自行車即時資訊
        /// </summary>
        /// <returns>YouBike2.0臺北市公共自行車即時資訊</returns>
        public ApiResponseBaseEntity<List<Youbike2ResponseEntity>> GetYoubike2Data()
        {
            var url = "https://tcgbusfs.blob.core.windows.net/dotapp/youbike/v2/youbike_immediate.json";

            var dataList = this._httpRequestService.Get<List<Youbike2ResponseEntity>>(url);

            if (dataList != null)
            {
                return dataList;
            }

            return new ApiResponseBaseEntity<List<Youbike2ResponseEntity>>()
            {
                Status = ApiStatusEnum.Failure,
                Data = null,
                ErrorMessage = "查無資料"
            };
        }
    }
}