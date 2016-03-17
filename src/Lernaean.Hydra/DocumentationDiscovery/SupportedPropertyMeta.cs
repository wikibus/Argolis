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
    }
}