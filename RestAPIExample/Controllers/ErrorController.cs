using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace RestAPIExample.Controllers
{
    [ApiController]
    public class ErrorController : Controller
    {
        [HttpGet("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var stackTrace = context.Error.StackTrace;
            var errorMessage = context.Error.Message;

            // log this error somewhere

            return Problem();
        }
    }
}
