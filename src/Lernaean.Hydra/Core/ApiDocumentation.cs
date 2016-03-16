using System.Collections.Generic;
using JetBrains.Annotations;
using JsonLD.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;

namespace Hydra.Core
{
    /// <summary>
    /// Base class for Hydra API documentation
    /// </summary>
    [SerializeCompacted]
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class ApiDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentation"/> class.
        /// </summary>
        /// <param name="entrypoint">The entrypoint Uri.</param>
        public ApiDocumentation(IriRef entrypoint)
        {
            Entrypoint = entrypoint;
        }

        /// <summary>
        /// Gets or sets the API documentation identifier
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the entrypoint Uri.
        /// </summary>
        [JsonProperty(Hydra.entrypoint)]
        public IriRef Entrypoint { get; private set; }

        /// <summary>
        /// Gets or sets the supported classes.
        /// </summary>
        [JsonProperty(Hydra.supportedClass)]
        public IEnumerable<Class> SupportedClasses { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Hydra.ApiDocumentation; }
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <param name="doc">The document.</param>
        [UsedImplicitly]
        protected static JToken GetContext(ApiDocumentation doc)
        {
            return Hydra.Context;
        }
    }
}
