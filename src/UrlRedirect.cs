using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Cloud5mins.domain;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Cloud5mins.Function
{
    public class UrlRedirect
    {
        private readonly ILogger _logger;

        public UrlRedirect(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UrlRedirect>();
        }

        [Function("UrlRedirect")]
        public HttpResponseData Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "urlredirect/{shortUrl}")]
            HttpRequestData req,
            string shortUrl,
            ExecutionContext context)
        {
            _logger.LogInformation($"-->> Trying to Url Redirect");
            //_logger.LogInformation($"HTTP trigger function processed for Url: {shortUrl}");

            string redirectUrl = "https://azure.com";

            if (!String.IsNullOrWhiteSpace(shortUrl))
            {

                StorageTableHelper stgHelper = new StorageTableHelper("UlsDataStorage");
                //var newUrl = stgHelper.GetShortUrlEntity(tempUrl);
                
                var tempUrl = new ShortUrlEntity(string.Empty, shortUrl);

                var newUrl = "https://frankysnotes.com";
                if (newUrl != null)
                {
                    redirectUrl = WebUtility.UrlDecode(newUrl);
                }
            }
            else
            {
                _logger.LogInformation("Bad Link, resorting to fallback.");
            }

            var res = req.CreateResponse(HttpStatusCode.Redirect);
            res.Headers.Add("Location", redirectUrl);
            return res;

        }
    }
}
