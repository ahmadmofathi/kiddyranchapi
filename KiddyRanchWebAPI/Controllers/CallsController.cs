using KiddyRanchWeb.BL;
using KiddyRanchWeb.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KiddyRanchWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallsController : ControllerBase
    {
        private readonly ICallManager _callManager;
        public CallsController(AppDbContext context, ICallManager callManager)
        {
            _callManager = callManager;
        }

        [HttpGet]
        public IActionResult GetAllCalls()
        {
            var Calls = _callManager.GetCalls();
            return Ok(Calls);
        }

        [HttpGet("his-calls/{id}")]
        public IActionResult GetAllHisCalls(string id)
        {
            var Calls = _callManager.GetHisCalls(id);
            return Ok(Calls);
        }

        [HttpGet("{id}")]
        public IActionResult GetCallById(string id)
        {
            var Call = _callManager.GetCall(id);
            if (Call == null)
            {
                return BadRequest("Call not found");
            }
            return Ok(Call);
        }
        [HttpPost]
        public IActionResult AddCall(CallAddDTO call)
        {
            var emp = _callManager.Add(call);
            if (emp != null)
            {
                return Ok(emp);
            }
            return BadRequest("Error Adding Call");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCall(string id)
        {
            CallDTO? Call = _callManager.GetCall(id);
            if (Call == null)
            {
                return NotFound("Call not found");
            }
            _callManager.Delete(id);
            return Ok("Call " + id + " has been deleted successfully");
        }

        [HttpPut("{id}")]
        public ActionResult<Call> UpdateCall(string id, CallDTO Call)
        {
            if (id != Call.callId)
            {
                return BadRequest();
            }
            _callManager.Update(Call);
            return NoContent();
        }

    }
}
