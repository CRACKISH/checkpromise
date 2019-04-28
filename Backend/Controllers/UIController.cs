using Checkpromise.Charts;
using Checkpromise.UIData;
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
            return new Data {};
        }
    }
}
