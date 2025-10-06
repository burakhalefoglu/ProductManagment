using System.Threading.Tasks;
using Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IResult = Core.Utilities.Results.IResult;

namespace WebAPI.Controllers;

/// <summary>
/// </summary>
[Route("api/healthcheck")]
[ApiController]
public class HealthChecksController : BaseApiController
{

    /// <summary>
    /// healthcheck.
    /// </summary>
    /// <returns></returns>
    [AllowAnonymous]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet]
    public async Task<IActionResult> getResult()
    {
      return await Task.Run(
            () => GetResponseOnlyResultMessage(new SuccessResult("Ok")));
    }
}