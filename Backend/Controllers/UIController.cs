using Checkpromise.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Checkpromise.Controllers
{
    [EnableCors]
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
