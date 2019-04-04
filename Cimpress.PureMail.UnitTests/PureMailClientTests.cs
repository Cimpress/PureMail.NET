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
        [Fact]
        public async Task SendEmailCallsEndpointAndDoesntThrow()
        {
            var mockedLogger = new Mock<ILogger<PureMailClient>>();
            mockedLogger.Setup(a => a.Log<object>(   
                It.IsAny<Microsoft.Extensions.Logging.LogLevel>(), 
                It.IsAny<EventId>(), 
                It.IsAny<object>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<object, Exception, string>>()));
            
            var mockedRestClient = new Mock<IRestClient>();
            var mockedRestResponse = new Mock<IRestResponse>();
            mockedRestResponse.Setup(a => a.StatusCode).Returns(HttpStatusCode.Accepted);
            mockedRestResponse.Setup(a => a.Content).Returns("{\"requestId\":\"123\"}");
            
            mockedRestClient.Setup(a => a.ExecuteTaskAsync(It.IsAny<IRestRequest>())).ReturnsAsync(mockedRestResponse.Object);
            
            var pureMailClient = new PureMailClient(
                new PureMailClientOptions() { ServiceBaseUrl = "http://localhost:5000"},
                mockedLogger.Object, 
                mockedRestClient.Object);

            var response = await pureMailClient
                .TemplatedEmail("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9")
                .SetTemplateId("demo-test")
                .Send(new
                {
                    value = "test"
                });
        }
    }
}
