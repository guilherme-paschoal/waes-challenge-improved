using System.Threading.Tasks;
using Xunit;
using WaesApi.Integration.Tests.Fixtures;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using WaesApi.Views;

namespace WaesApi.Integration.Tests
{
    [Collection("SystemCollection")]
    public class DiffTest
    {
        readonly string encodedJson;
        readonly Encoding encoding;
        readonly string contentType;
        readonly TestContext context;

        public DiffTest()
        {
            encoding = Encoding.UTF8;
            // Keep in mind that the input string below needs to be wrapped in quotes so that the [FromBody] binder works properly
            encodedJson = "\"eyJqc29uS2V5IjoianNvblZhbHVlIn0=\"";
            contentType = "application/json";
            context = new TestContext();
        }

        /* xUnit likes to run tests in random order and for that reason, I had to write an integration test that will run through the whole functionality 
         * by calling POST on the input endpoints and then by calling a GET on the output endpoint because it would require a ton of code (that I don't have the time to write right now) 
         * just to guarantee the order of execution. */
        [Fact]
        public async Task WhenTestedAllTogetherTheFunctionalityDoesNotBreakAndReturnsExpectedResult() 
        {
            var requestContent = new StringContent(encodedJson, encoding, contentType);

            var responseLeft = await context.Client.PostAsync("v1/diff/1/left", requestContent);
            Assert.Equal(HttpStatusCode.OK, responseLeft.StatusCode);

            var responseRight = await context.Client.PostAsync("v1/diff/1/right", requestContent);
            Assert.Equal(HttpStatusCode.OK, responseRight.StatusCode);

            var response = await context.Client.GetAsync("v1/diff/1");


            var responseContent = JsonConvert.DeserializeObject<DiffResultView>(response.Content.ReadAsStringAsync().Result);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("The compared strings are equal", responseContent.Result);
        }
        [Fact]
        public async Task GetDiffReturns404IfIdDoesntExist()
        {
            var response = await context.Client.GetAsync("v1/diff/1");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
