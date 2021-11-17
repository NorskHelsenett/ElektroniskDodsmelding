using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace ElektroniskDodsmelding.Samples.Tests
{
    /// <summary>
    /// Creates an HttpClientHandler that uses a JSON Web Token as a bearer token in the Authorization header.
    /// </summary>
    internal class AuthorizedHttpClientHandler : HttpClientHandler
    {
        private readonly string _jwt;

        /// <summary>
        /// Creates the AuthorizedHttpClientHandler.
        /// </summary>
        /// <param name="jwt">JSON Web Token to add as Authorization header.</param>
        internal AuthorizedHttpClientHandler(string jwt)
        {
            _jwt = jwt;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization ??= new AuthenticationHeaderValue("Bearer", _jwt);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}