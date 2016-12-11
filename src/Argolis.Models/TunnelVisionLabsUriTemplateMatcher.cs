using System;
using System.Linq;
using TunnelVisionLabs.Net;

namespace Argolis.Models
{
    /// <summary>
    /// Implements <see cref="IUriTemplateMatcher"/> with TunnelVisionLabs.Net.UriTemplate package
    /// </summary>
    /// <seealso cref="Argolis.Models.IUriTemplateMatcher" />
    public class TunnelVisionLabsUriTemplateMatcher : IUriTemplateMatcher
    {
        private readonly IModelTemplateProvider templates;

        /// <summary>
        /// Initializes a new instance of the <see cref="TunnelVisionLabsUriTemplateMatcher"/> class.
        /// </summary>
        /// <param name="templates">The template provider.</param>
        public TunnelVisionLabsUriTemplateMatcher(IModelTemplateProvider templates)
        {
            this.templates = templates;
        }

        /// <inheritdoc/>
        public UriTemplateMatches Match<T>(Uri uri)
        {
            var template = uri.IsAbsoluteUri
                ? this.templates.GetAbsoluteTemplate(typeof(T))
                : this.templates.GetTemplate(typeof(T));

            var tunnelVisionMatches = new UriTemplate(template).Match(uri);
            if (tunnelVisionMatches != null)
            {
                var matchDict = tunnelVisionMatches.Bindings.Values.ToDictionary(m => m.Key.Name, m => m.Value);
                return new UriTemplateMatches(matchDict);
            }

            return UriTemplateMatches.Failure();
        }
    }
}