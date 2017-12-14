using Argolis.Hydra.Core;
using NullGuard;

namespace Argolis.Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Basic information about a <see cref="SupportedProperty" />
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class SupportedPropertyMeta
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SupportedProperty"/> can be written.
        /// </summary>
        public bool Writeable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SupportedProperty"/> can be read.
        /// </summary>
        public bool Readable { get; set; }

        /// <summary>
        /// Gets or sets a description of a <see cref="SupportedProperty"/>.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SupportedPropertyMeta"/> is required.
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the underlying property is a hydra:Link.
        /// </summary>
        public bool IsLink { get; set; }
    }
}
