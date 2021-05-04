using System;
using System.Net;
using System.Net.Http;
using Polly;
using Polly.Extensions.Http;

namespace MoviesApp.Client.Clients
{
    public static class HttpPolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> CircuitBreakerPatternPolicy =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30));

        public static IAsyncPolicy<HttpResponseMessage> RetryPolicy =>
            HttpPolicyExtensions.HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}