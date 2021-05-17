using Api.IntegrationTest.Common;
using System.Threading.Tasks;
using Web.Api;
using Xunit;

namespace Api.IntegrationTest.Controllers.SampleController
{
    class SampleCreateTest
    {
        public class Create : IClassFixture<CustomWebApplicationFactory<Startup>>
        {
            private readonly CustomWebApplicationFactory<Startup> _factory;
            public Create(CustomWebApplicationFactory<Startup> factory)
            {
                _factory = factory;
            }
            [Fact]
            public async Task Given_Valid_sampe_Text_ReturnsEqual()
            {
                var data = await Task.FromResult<string>("Hello");
                Assert.Equal("Hello", data);
            }


        }
    }
}
