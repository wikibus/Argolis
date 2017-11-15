using Newtonsoft.Json;

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
    }
}