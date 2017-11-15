using System.Collections.Generic;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;
using TunnelVisionLabs.Net;

namespace Hydra.Core
{
    /// <summary>
    /// Represents a Hydra IRI Template
    /// </summary>
    [NullGuard(ValidationFlags.ReturnValues)]
    public class IriTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IriTemplate"/> class.
        /// </summary>
        public IriTemplate()
        {
            this.VariableRepresentation = VariableRepresentation.BasicRepresentation;
            this.Mappings = new List<IriTemplateMapping>();
        }

        /// <summary>
        /// Gets or sets the variable representation
        /// </summary>
        [JsonProperty(Hydra.variableRepresentation)]
        public VariableRepresentation VariableRepresentation { get; set; }

        /// <summary>
        /// Gets or sets the template
        /// </summary>
        [JsonProperty(Hydra.template)]
        public UriTemplate Template { get; set; }

        /// <summary>
        /// Gets the IRI Template variable mappings
        /// </summary>
        public IList<IriTemplateMapping> Mappings { get; }

        [JsonProperty, UsedImplicitly]
        private string Type => Hydra.IriTemplate;
    }
}