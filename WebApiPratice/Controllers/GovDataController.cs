using BussinessLogic.Service.GovDatas;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPratice.Controllers
{
    /// <summary>
    /// 政府資料開放平台 Controller
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class GovDataController : Controller
    {
        /// <summary>
        /// Gov Data Service Interface
        /// </summary>
        private IGovDataService _govDataService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GovDataController" /> class.
        /// </summary>
        public GovDataController()
        {
            this._govDataService = new GovDataService();
        }

        /// <summary>
        /// 取得YouBike2.0臺北市公共自行車即時資訊
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetYoubike2Data()
        {
            var result = this._govDataService.GetYoubike2Data();

            return this.Ok(result);
        }
    }
}