using Microsoft.AspNetCore.Mvc;
using WaesApi.Utils;
using WaesApi.Views;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WaesApi.Controllers
{
    [Route("v1/[controller]")]
    public class DiffController : Controller
    {
        /// <summary>
        /// Route for handling input of values that will be diff'ed. Call either /left or /right. 
        /// </summary>
        /// <returns>A result indicating the http status code</returns>
        /// <param name="id">Id of the string to be diffed</param>
        /// <param name="direction">The direction is defined by the route called</param>
        /// <param name="input">The string to be diffed</param>
        [HttpPost("{id}/{direction}")]
        public IActionResult Input(int id, CacheDirection? direction, [FromBody]string input)
        {
            // By making the enum nullable, the binder will return a 404 when neither of the enum values is passed

            string decodedJson = DecodingHelper.Decode(input.ToString());
            CachingHelper.Add(id, decodedJson, direction.Value);
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
            // Returns 404 if Id hasn't been inserted in both directions. This prevents an unwanted 500 from raising to the user.
            if(!CachingHelper.KeyExists(id)) { 
                return new NotFoundObjectResult("Provided Id does not exist"); 
            }

            string left = CachingHelper.Get(id, CacheDirection.LEFT);
            string right = CachingHelper.Get(id, CacheDirection.RIGHT);    
       
            /* Improvement: If this application did a heavier/more complex Diff, instead of calling the Diff on the GET, there could be an async/background worker
             * that ran whenever both directions were input, making the GET speed faster */
            var diffResult = new CustomDiff(left, right).Diff();

            var response = new DiffResultView
            {
                Result = diffResult.Result,
                Extra = diffResult.HasDifferences ? diffResult.Differences.ToString() : ""
            };

            return new JsonResult(response);

        }
    }
}
