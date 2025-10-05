
using Business.Handlers.Colors.Commands;
using Business.Handlers.Colors.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Colors If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ColorsController : BaseApiController
    {
        ///<summary>
        ///List Colors
        ///</summary>
        ///<remarks>Colors</remarks>
        ///<return>List Colors</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Color>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetColorsQuery()));
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Colors</remarks>
        ///<return>Colors List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Color))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromQuery]int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetColorQuery { Id = id }));
        }

        /// <summary>
        /// Add Color.
        /// </summary>
        /// <param name="createColor"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateColorCommand createColor)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createColor));
        }

        /// <summary>
        /// Update Color.
        /// </summary>
        /// <param name="updateColor"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateColorCommand updateColor)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateColor));
        }

        /// <summary>
        /// Delete Color.
        /// </summary>
        /// <param name="deleteColor"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteColorCommand(){Id = id}));
        }
    }
}
