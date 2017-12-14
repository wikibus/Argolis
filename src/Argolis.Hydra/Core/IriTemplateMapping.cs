using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;

namespace Argolis.Hydra.Core
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
        [JsonProperty(Vocab.Hydra.variable)]
        public string Variable { get; set; }

        /// <summary>
        /// Gets or sets the hydra:property
        /// </summary>
        [JsonProperty(Vocab.Hydra.property)]
        public Property Property { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Variable"/> is required
        /// </summary>
        [JsonProperty(Vocab.Hydra.required)]
        public bool Required { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type => Vocab.Hydra.IriTemplateMapping;
    }
}