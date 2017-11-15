using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;
using TunnelVisionLabs.Net;

namespace Hydra.Resources
{
    /// <summary>
    /// Represents a Hydra IRI Template
    /// </summary>
    public class IriTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IriTemplate"/> class.
        /// </summary>
        public IriTemplate()
        {
            this.VariableRepresentation = VariableRepresentation.BasicRepresentation;
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
        public UriTemplate Template { [return: AllowNull] get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type => Hydra.IriTemplate;
    }
}