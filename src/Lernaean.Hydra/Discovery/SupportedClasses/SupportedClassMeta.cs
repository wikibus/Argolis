using Hydra.Core;
using NullGuard;

namespace Hydra.Discovery.SupportedClasses
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

        /// <summary>
        /// Gets or sets the class' title.
        /// </summary>
        public string Title { get; set; }
    }
}
