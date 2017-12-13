using Microsoft.AspNetCore.Mvc;
using WaesApi.Services;
using WaesApi.Utils;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WaesApi.Controllers
{
    [Route("v1/[controller]")]
    public class DiffController : Controller
    {
        readonly IDiffService diffService;

        public DiffController(IDiffService diffService)
        {
            this.diffService = diffService;
        }

        /// <summary>
        /// Route for handling input of values that will be diff'ed. Call either /left or /right. 
        /// </summary>
        /// <returns>A result indicating the http status code</returns>
        /// <param name="id">Id of the string to be diffed</param>
        /// <param name="direction">The direction is defined by the route called</param>
        /// <param name="input">The string to be diffed</param>
        [HttpPost("{id}/{direction}")]
        public IActionResult Input(int id, string direction, [FromBody]string input)
        {
            string decodedJson = DecodingHelper.Decode(input.ToString());
            diffService.Input(id,direction,decodedJson);
            return Ok();
        }

        /// <summary>
        /// Get the Diff for the two strings provided with the same Id
        /// </summary>
        /// <returns>A JsonResult containing the serialization of a DiffResultView, containing the Result of the comparison </returns>
        /// <param name="id">Id for the strings compared</param>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var diffResult = diffService.GetDiff(id);

            var response = new {
                Result = diffResult.Result,
                Extra = diffResult.GetDifferencesFlat()
            };

            return new JsonResult(response);

        }
    }
}
