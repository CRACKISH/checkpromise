using Checkpromise.Models;
using Microsoft.AspNetCore.Mvc;

namespace Checkpromise.Controllers
{
    [Route("GetData")]
    [ApiController]
    public class UIController : ControllerBase
    {
       [HttpGet]
        public ActionResult<Data> GetData()
        {
            return new Data();
        }
    }
}
