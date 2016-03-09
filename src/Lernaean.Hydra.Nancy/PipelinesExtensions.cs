using Nancy;
using Nancy.Routing;

namespace Hydra.Nancy
{
    /// <summary>
    /// Wires Hydra with the application
    /// </summary>
    public static class ResponseExtensions
    {
        private const string LinkHeaderFormat = "<{0}>; rel=\"{1}\"";

        /// <summary>
        /// Appends a Link header to the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="uri">The link URI.</param>
        /// <param name="relation">The link relation.</param>
        /// <remarks>See https://tools.ietf.org/html/rfc5988</remarks>
        public static void AppendLinkHeader(this Response response, string uri, string relation)
        {
            const string linkHeader = "Link";
            var headerValue = string.Format(LinkHeaderFormat, uri, relation);

            if (response.Headers.ContainsKey(linkHeader))
            {
                string current = response.Headers[linkHeader];
                response.WithHeader(linkHeader, current + ", " + headerValue);
            }
            else
            {
                response.WithHeader(linkHeader, headerValue);
            }
        }
    }
}
