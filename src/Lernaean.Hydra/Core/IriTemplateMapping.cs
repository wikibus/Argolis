using JetBrains.Annotations;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra.Core
{
    /// <summary>
    /// Represents a Hydra IRI Template Mapping
    /// </summary>
    [NullGuard(ValidationFlags.ReturnValues)]
    public class IriTemplateMapping
    {
        /// <summary>
        /// Gets or sets the template variable
        /// </summary>
        [JsonProperty(Hydra.variable)]
        public string Variable { get; set; }

        /// <summary>
        /// Gets or sets the hydra:property
        /// </summary>
        [JsonProperty(Hydra.property)]
        public IriRef Property { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Variable"/> is required
        /// </summary>
        [JsonProperty(Hydra.required)]
        public bool Required { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type => Hydra.IriTemplateMapping;
    }
}