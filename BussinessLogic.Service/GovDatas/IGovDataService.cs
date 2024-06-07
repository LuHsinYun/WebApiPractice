using System.Collections.Generic;
using BussinessLogic.Entity.ApiResponses;
using BussinessLogic.Entity.GovDatas;

namespace BussinessLogic.Service.GovDatas
{
    public interface IGovDataService
    {
        /// <summary>
        /// 取得YouBike2.0臺北市公共自行車即時資訊
        /// </summary>
        /// <returns>YouBike2.0臺北市公共自行車即時資訊</returns>
        ApiResponseBaseEntity<List<Youbike2ResponseEntity>> GetYoubike2Data();
    }
}