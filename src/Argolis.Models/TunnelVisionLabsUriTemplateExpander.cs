using System;
using System.Collections.Generic;
using DictionaryLibrary;
using TunnelVisionLabs.Net;

namespace Argolis.Models
{
    /// <summary>
    /// URI Template expander implemented with TunnelVisionLabs.Net.UriTemplate package
    /// </summary>
    public class TunnelVisionLabsUriTemplateExpander : IUriTemplateExpander
    {
        private readonly IModelTemplateProvider templates;

        /// <summary>
        /// Initializes a new instance of the <see cref="TunnelVisionLabsUriTemplateExpander"/> class.
        /// </summary>
        /// <param name="templates">The template provider.</param>
        public TunnelVisionLabsUriTemplateExpander(IModelTemplateProvider templates)
        {
            this.templates = templates;
        }

        /// <inheritdoc />
        public Uri Expand<T>(IDictionary<string, object> parameters)
        {
            var templateString = this.templates.GetTemplate(typeof(T));
            return this.Expand(templateString, parameters);
        }

        /// <inheritdoc />
        public Uri Expand<T>(object parameters)
        {
            var dictionary = parameters as IDictionary<string, object> ?? DictionaryMaker.Make(parameters);
            return this.Expand<T>(dictionary);
        }

        /// <inheritdoc />
        public Uri ExpandAbsolute<T>(IDictionary<string, object> parameters)
        {
            var templateString = this.templates.GetAbsoluteTemplate(typeof(T));
            return this.Expand(templateString, parameters);
        }

        /// <inheritdoc />
        public Uri ExpandAbsolute<T>(object parameters)
        {
            var dictionary = parameters as IDictionary<string, object> ?? DictionaryMaker.Make(parameters);
            return this.ExpandAbsolute<T>(dictionary);
        }

        private Uri Expand(string templateString, IDictionary<string, object> parameters)
        {
            return new UriTemplate(templateString).BindByName(parameters);
        }
    }
}