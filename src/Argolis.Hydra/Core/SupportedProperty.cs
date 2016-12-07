using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;

namespace Argolis.Hydra.Core
{
    /// <summary>
    /// A Hydra property
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class SupportedProperty : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedProperty"/> class.
        /// </summary>
        public SupportedProperty()
        {
            this.Property = new Property();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SupportedProperty"/> is required.
        /// </summary>
        [JsonProperty(Vocab.Hydra.required)]
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property can be read.
        /// </summary>
        [JsonProperty(Vocab.Hydra.readable)]
        public bool Readable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether property can be written.
        /// </summary>
        [JsonProperty(Vocab.Hydra.writeable)]
        public bool Writeable { get; set; }

        /// <summary>
        /// Gets or sets the property.
        /// </summary>
        [JsonProperty(Vocab.Hydra.property)]
        public Property Property { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Vocab.Hydra.SupportedProperty; }
        }
    }
}
