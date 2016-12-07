using Argolis.Hydra.Core;
using JsonLD.Entities;
using NullGuard;

namespace Argolis.Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Basic information about an <see cref="Operation" />
    /// </summary>
    [NullGuard(ValidationFlags.None)]
    public class OperationMeta
    {
        /// <summary>
        /// Gets or sets the HTTP method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the operation request model type identifier.
        /// </summary>
        public IriRef? Expects { get; set; }

        /// <summary>
        /// Gets or sets the operation response type identifier.
        /// </summary>
        public IriRef? Returns { get; set; }

        /// <summary>
        /// Gets or sets the operation title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the operation description.
        /// </summary>
        public string Description { get; set; }
    }
}
