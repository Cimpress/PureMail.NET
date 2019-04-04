﻿using System.Threading.Tasks;
using Cimpress.PureMail.Exceptions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;

namespace Cimpress.PureMail.Implementation
{
    internal class TemplatedEmailRequest : ITemplatedEmailRequest
    {
        private readonly ILogger<PureMailClient> _logger;
        
        private readonly PureMailClientOptions _pureMailClientOptions;
        
        private readonly string _accessToken;
        
        private readonly IRestClient _restClient;
        
        private string _templateId;
        
        public TemplatedEmailRequest(string accessToken, PureMailClientOptions options, ILogger<PureMailClient> logger, IRestClient restClient)
        {
            _pureMailClientOptions = options;
            _accessToken = accessToken;
            _logger = logger;
            _restClient = restClient;
        }
        
        public TemplatedEmailRequest(string accessToken, PureMailClientOptions options, ILogger<PureMailClient> logger) : this(accessToken, options, logger,  new RestClient(options.ServiceBaseUrl))
        {
           
        }

        public ITemplatedEmailRequest SetTemplateId(string templateId)
        {
            _templateId = templateId;
            return this;
        }
               
        public async Task<IPureMailResponse> Send<TO>(TO payload)
        {
            var request = new RestRequest("/v1/send/{templateId}", Method.POST);
            request.JsonSerializer = new JsonSerializer();

            request.AddHeader("Authorization", $"Bearer {_accessToken}");
            request.AddHeader("Content-type", "application/json");
            request.AddUrlSegment("templateId", _templateId);
            request.AddHeader("x-cimpress-accept-preference", _pureMailClientOptions.AcceptPreference);
            
            _logger?.LogDebug($">> POST /v1/send/{_templateId} :: payload={JsonConvert.SerializeObject(payload)}");
            request.AddJsonBody(payload);

            var response = await this._restClient.ExecuteTaskAsync(request);
            _logger?.LogDebug($"<< POST /v1/send/{_templateId} :: {response.StatusCode}");
            
            switch (response.StatusCode)
            {
                case System.Net.HttpStatusCode.Accepted:
                    return JsonConvert.DeserializeObject<PureMailResponse>(response.Content);
                
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
