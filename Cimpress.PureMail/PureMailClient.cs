using System.Threading.Tasks;
using Cimpress.PureMail.Exceptions;
using InvoiceDataStore.BL.Clients.PureMail;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace Cimpress.PureMail
{
    public class PureMailClient : IPureMailClient
    {
        private readonly IRestClient _client;
     
        private static ILogger _logger;

        public PureMailClient(PureMailClientOptions options, ILogger logger, IRestClient restClient)
        {
            _client = restClient;
            _logger = logger ?? new LoggerFactory().CreateLogger("StereotypeClient");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PureMailClient"/> class.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        public PureMailClient(PureMailClientOptions options, ILogger logger = null) : this(options, logger, new RestClient(options.PureMailUrlBaseUrl))
        {
        }

        public PureMailClient() : this(new PureMailClientOptions(), null, new RestClient("https://puremail.trdlnk.cimpress.io"))
        {
        }

        
        public async Task SendTemplatedEmail<T>(string accessToken, string templateId, T payload)
        {
            var request = new RestRequest("/v1/send/{templateId}", Method.POST);
            request.JsonSerializer = new JsonSerializer();

            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.AddHeader("Content-type", "application/json");
            request.AddUrlSegment("templateId", templateId);

            _logger.LogDebug($">> POST /v1/send/{templateId}");
            request.AddJsonBody(payload);

            var response = await this._client.ExecuteTaskAsync(request);
            _logger.LogDebug($"<< POST /v1/send/{templateId} :: {response.StatusCode}");
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Accepted:
                    return;
                case System.Net.HttpStatusCode.Unauthorized:
                    throw new AuthenticationException("Incorrect authentication");
                case System.Net.HttpStatusCode.Forbidden:
                    throw new AuthorizationException("Insufficient permission level to access materialization");     
                default:
                    throw new PureMailException($"Unexpected status code {response.StatusCode}", null);
            }
        }
    }
}
