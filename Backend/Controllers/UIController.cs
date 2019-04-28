using Checkpromise.Charts;
using Checkpromise.Models;
using Checkpromise.Promises;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Checkpromise.Controllers
{
    [EnableCors]
    [Route("GetData")]
    [ApiController]
    public class UIController : ControllerBase
    {
       [HttpPost]
        public ActionResult<Data> GetData()
        {
            var chartManager = new ChartManager();
            var promiseManager = new PromiseManager();
            return new Data() {
                ChartData = chartManager.GetAll(),
                PromiseData = promiseManager.GetAll()
            };
        }
    }
}
