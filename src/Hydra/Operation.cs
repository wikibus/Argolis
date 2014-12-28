namespace Hydra
{
    /// <summary>
    /// A Hydra operation
    /// </summary>
    [NullGuard(ValidationFlags.ReturnValues)]
    public class Operation
    {
        /// <summary>
        /// Gets or sets the HTTP method.
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the returned type.
        /// </summary>
        public string Returns { get; set; }

        /// <summary>
        /// Gets or sets the expected type.
        /// </summary>
        public string Expects { get; set; }
    }
}
