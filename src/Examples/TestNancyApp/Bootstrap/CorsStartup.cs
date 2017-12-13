using Nancy;
using Nancy.Bootstrapper;

namespace TestNancyApp.Bootstrap
{
    public class CorsStartup : IApplicationStartup
    {
        private const string AllowHeadersHeader = "Access-Control-Allow-Headers";
        private const string AllowOriginHeader = "Access-Control-Allow-Origin";
        private const string AllowMethodHeader = "Access-Control-Allow-Methods";
        private const string AllowHeader = "Allow";
        private const string AccessControlExposeHeaders = "Access-Control-Expose-Headers";

        /// <summary>
        /// Append CORS headers to reponses
        /// </summary>
        public void Initialize(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToEndOfPipeline(this.AppendCorsHeaders);
        }

        private void AppendCorsHeaders(NancyContext context)
        {
            context.Response
                .WithHeader(AllowOriginHeader, "*")
                .WithHeader(AllowMethodHeader, "POST, GET, DELETE, PUT, OPTIONS")
                .WithHeader(AllowHeadersHeader, "Accept, Origin, Content-type, X-Requested-With")
                .WithHeader(AllowHeader, "POST, GET, DELETE, PUT, OPTIONS")
                .WithHeader(AccessControlExposeHeaders, "Link, Content-Location");
        }
    }
}