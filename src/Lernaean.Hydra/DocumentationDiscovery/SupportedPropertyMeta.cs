using Hydra.Core;
using NullGuard;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Basic information about a <see cref="SupportedProperty"/>
    /// </summary>
    public class SupportedPropertyMeta
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SupportedProperty"/> can be written.
        /// </summary>
        public bool Writeable { get; set; }
    }
}