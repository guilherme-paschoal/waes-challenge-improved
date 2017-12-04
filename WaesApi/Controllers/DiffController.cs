using Microsoft.AspNetCore.Mvc;
using WaesApi.Utils;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WaesApi.Controllers
{
    [Route("v1/[controller]")]
    public class DiffController : Controller
    {
        // Improvement: similar controller actions can become one that respond differently to two routes
        [HttpPost("{id}/left")]
        public IActionResult Left(int id, [FromBody]string input)
        {
            string decodedJson = DecodingHelper.Decode(input);
            CachingHelper.Add(id, decodedJson, CacheDirection.LEFT);
            return Ok();
        }

        // Improvement: similar controller actions can become one that respond differently to two routes
        [HttpPost("{id}/right")]
        public IActionResult Right(int id, [FromBody]string input)
        {
            string decodedJson = DecodingHelper.Decode(input);
            CachingHelper.Add(id, decodedJson, CacheDirection.RIGHT);
            return Ok();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string left = CachingHelper.Get(id, CacheDirection.LEFT);
            string right = CachingHelper.Get(id, CacheDirection.RIGHT);

            var diffResult = new CustomDiff(left, right).Diff();

            var response = new
            {
                diffResult.Result,
                Extra = diffResult.HasDifferences ? diffResult.Differences.ToString() : ""
            };

            return new JsonResult(response);

        }
    }
}
