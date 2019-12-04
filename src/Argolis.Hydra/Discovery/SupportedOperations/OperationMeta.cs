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

        /// <summary>
        /// Gets or sets the types of the operation.
        /// </summary>
        public IriRef[] Types { get; set; } = new IriRef[0];
    }
}
