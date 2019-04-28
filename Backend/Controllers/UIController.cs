using Checkpromise.Charts;
using Checkpromise.UIData;
using Checkpromise.Promises;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Checkpromise.Controllers
{
    [EnableCors]
    [ApiController]
    public class UIController : ControllerBase
    {

        private readonly IChartRepository _chartRepository;
        private readonly IPromiseRepository _promiseRepository;

        public UIController(IChartRepository chartRepository, IPromiseRepository promiseRepository)
        {
            _chartRepository = chartRepository;
            _promiseRepository = promiseRepository;
        }

        [Route("GetData")]
        [HttpGet]
        public ActionResult<Data> GetData()
        {
            var chartData = _chartRepository.GetAll().Select(c => new ChartData(c)).ToArray();
            var promisesData = _promiseRepository.GetAll().Select(p => new PromiseData(p)).ToArray();
            return new Data {
                ChartData = chartData,
                PromiseData = promisesData
            };
        }
    }
}
