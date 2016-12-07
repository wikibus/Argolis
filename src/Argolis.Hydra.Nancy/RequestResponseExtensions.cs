using System;
using Nancy;

namespace Argolis.Hydra.Nancy
{
    /// <summary>
    /// Wires Hydra with the application
    /// </summary>
    public static class RequestResponseExtensions
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

        /// <summary>
        /// Gets the API documentation URI.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="documentationPath">The documentation path.</param>
        /// <returns>absolute URI string</returns>
        public static string GetApiDocumentationUri(this Request request, string documentationPath)
        {
            var uriBuilder = new UriBuilder(request.Url.SiteBase)
            {
                Path = request.Url.BasePath + documentationPath
            };

            string apiDocPath = uriBuilder.Uri.ToString();
            if (uriBuilder.Port == 80)
            {
                apiDocPath = uriBuilder.Uri.GetComponents(UriComponents.AbsoluteUri & ~UriComponents.Port, UriFormat.UriEscaped);
            }

            return apiDocPath;
        }
    }
}
