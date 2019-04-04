using System.Runtime.CompilerServices;
using Cimpress.PureMail.Implementation;
using Microsoft.Extensions.Logging;
using RestSharp;

[assembly:InternalsVisibleTo("Cimpress.Puremail.UnitTests")]

namespace Cimpress.PureMail
{
    public class PureMailClient : IPureMailClient
    {
        private readonly IRestClient _client;
     
        private static ILogger<PureMailClient> _logger;
        
        private readonly PureMailClientOptions _options;


        public PureMailClient() : this(null, null,  null)
        {
        }

        public PureMailClient(PureMailClientOptions options, ILogger<PureMailClient> logger) : this(options, logger, new RestClient(options.ServiceBaseUrl))
        {
        }

        // TODO: This should be internal
        internal PureMailClient(PureMailClientOptions options, ILogger<PureMailClient> logger, IRestClient restClient)
        {
            _options = options ?? new PureMailClientOptions {ServiceBaseUrl = "https://puremail.trdlnk.cimpress.io"};
            _client = restClient ?? new RestClient(_options.ServiceBaseUrl);
            _logger = logger;
        }

        public ITemplatedEmailRequest TemplatedEmail(string accessToken)
        {
            return new TemplatedEmailRequest(accessToken, _options, _logger, _client);
        }
    }
}
