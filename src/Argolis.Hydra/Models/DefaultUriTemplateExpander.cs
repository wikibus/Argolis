using System;
using System.Collections.Generic;
using Argolis.Models;

namespace Argolis.Hydra.Models
{
    /// <summary>
    /// URI Template expander implemented with TunnelVisionLabs.Net.UriTemplate package
    /// </summary>
    public class DefaultUriTemplateExpander : IUriTemplateExpander
    {
        private readonly IModelTemplateProvider templates;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultUriTemplateExpander"/> class.
        /// </summary>
        /// <param name="templates">The template provider.</param>
        public DefaultUriTemplateExpander(IModelTemplateProvider templates)
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
            return new UriTemplate.Core.UriTemplate(templateString).BindByName(parameters);
        }
    }
}