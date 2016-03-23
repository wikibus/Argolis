using Hydra.Core;
using JsonLD.Entities;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Basic information about an <see cref="Operation" />
    /// </summary>
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
    }
}
