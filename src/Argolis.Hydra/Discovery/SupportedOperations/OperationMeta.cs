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
        /// Initializes a new instance of the <see cref="OperationMeta"/> class.
        /// </summary>
        public OperationMeta(
            string method,
            string title = "",
            string description = "",
            IriRef? expects = null,
            IriRef? returns = null)
        {
            this.Method = method;
            this.Expects = expects;
            this.Returns = returns;
            this.Title = title;
            this.Description = description;
        }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        public string Method { get; }

        /// <summary>
        /// Gets the operation request model type identifier.
        /// </summary>
        public IriRef? Expects { get; }

        /// <summary>
        /// Gets the operation response type identifier.
        /// </summary>
        public IriRef? Returns { get; }

        /// <summary>
        /// Gets the operation title.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the operation description.
        /// </summary>
        public string Description { get; }
    }
}
