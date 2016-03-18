using Hydra.Core;
using NullGuard;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Basic information about a Supported <see cref="Class" />
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class SupportedClassMeta
    {
        /// <summary>
        /// Gets or sets the description of a supported class.
        /// </summary>
        public string Description { get; set; }
    }
}