using NullGuard;

namespace Hydra
{
    /// <summary>
    /// A Hydra operation
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        public Operation(string method)
        {
            Method = method;
        }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// Gets or sets the returned type.
        /// </summary>
        [AllowNull]
        public string Returns { get; set; }

        /// <summary>
        /// Gets or sets the expected type.
        /// </summary>
        [AllowNull]
        public string Expects { get; set; }
    }
}
