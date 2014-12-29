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
        /// <param name="method">The method.</param>
        public Operation(string method)
        {
            Method = method;
        }

        /// <summary>
        /// Gets or sets the HTTP method.
        /// </summary>
        public string Method { get; private set; }

        /// <summary>
        /// Gets or sets the returned type.
        /// </summary>
        public string Returns { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the expected type.
        /// </summary>
        public string Expects { [return: AllowNull] get; set; }
    }
}
