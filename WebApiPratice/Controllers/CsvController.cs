using BussinessLogic.Service.Csvs;
using Microsoft.AspNetCore.Mvc;

namespace WebApiPratice.Controllers
{
    /// <summary>
    /// CsvController
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class CsvController : Controller
    {
        /// <summary>
        /// Csv Service Interface
        /// </summary>
        private ICsvService _csvService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvController" /> class.
        /// </summary>
        public CsvController()
        {
            this._csvService = new CsvService();
        }

        [HttpGet]
        public ActionResult GetMemberById(int id)
        {
            var result = this._csvService.GetMemberById(id);

            return this.Ok(result);
        }
    }
}