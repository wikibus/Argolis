using System.Collections.Generic;

namespace Argolis.Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Fluent interface to simplify creating of supported operations
    /// </summary>
    public class SupportedOperationCollection
    {
        private readonly IList<OperationMeta> operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperationCollection"/> class.
        /// </summary>
        public SupportedOperationCollection(IList<OperationMeta> operations)
        {
            this.operations = operations;
        }

        /// <summary>
        /// Adds a GET supported operation
        /// </summary>
        public SupportedOperationBuilder SupportsGet()
        {
            return this.Supports(HttpMethod.Get);
        }

        /// <summary>
        /// Adds a PUT supported operation
        /// </summary>
        public SupportedOperationBuilder SupportsPut()
        {
            return this.Supports(HttpMethod.Put);
        }

        /// <summary>
        /// Adds a PATCH supported operation
        /// </summary>
        public SupportedOperationBuilder SupportsPatch()
        {
            return this.Supports(HttpMethod.Patch);
        }

        /// <summary>
        /// Adds a POST supported operation
        /// </summary>
        public SupportedOperationBuilder SupportsPost()
        {
            return this.Supports(HttpMethod.Post);
        }

        /// <summary>
        /// Adds a DELETE supported operation
        /// </summary>
        public SupportedOperationBuilder SupportsDelete()
        {
            return this.Supports(HttpMethod.Delete);
        }

        /// <summary>
        /// Adds a supported operation with any method
        /// </summary>
        public SupportedOperationBuilder Supports(string method)
        {
            var meta = new OperationMeta(method);
            this.Supports(meta);
            return new SupportedOperationBuilder(meta);
        }

        /// <summary>
        /// Adds a supported operation
        /// </summary>
        private void Supports(OperationMeta operation)
        {
            this.operations.Add(operation);
        }
    }
}