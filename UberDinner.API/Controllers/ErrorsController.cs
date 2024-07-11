using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UberDinner.Application.Common.Errors;

namespace UberDinner.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            var (status, message) = exception switch
            {
                IServiceException serviceException
                    => ((int)serviceException.StatusCode, serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occured.")
            };
            return Problem(title: message, statusCode: status);
        }
    }
}
