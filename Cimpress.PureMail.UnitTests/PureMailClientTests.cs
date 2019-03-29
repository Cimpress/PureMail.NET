using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using RestSharp;
using Xunit;

namespace Cimpress.PureMail.UnitTests
{
    public class MaterializationResponseTests
    {
        private IPureMailClient _pureMailClient;

        [Fact]
        public async Task SendEmailCallsEndpointAndDoesntThrow()
        {
            var mockedLogger = new Mock<Microsoft.Extensions.Logging.ILogger>();
            mockedLogger.Setup(a => a.Log<object>(   
                It.IsAny<Microsoft.Extensions.Logging.LogLevel>(), 
                It.IsAny<EventId>(), 
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()));
            
            var mockedRestClient = new Mock<IRestClient>();
            var mockedRestResponse = new Mock<IRestResponse>();
            mockedRestResponse.Setup(a => a.StatusCode).Returns(HttpStatusCode.Accepted);

            mockedRestClient.Setup(a => a.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(mockedRestResponse.Object);
            _pureMailClient = new PureMailClient(new PureMailClientOptions() { PureMailUrlBaseUrl = "http://localhost:5000"},
                mockedLogger.Object, mockedRestClient.Object);

            await _pureMailClient.SendTemplatedEmail("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9", "demo-test", new
            {
                value = "test"
            });
        }
    }
}
