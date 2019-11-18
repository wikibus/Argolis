using System.Collections.Generic;
using JsonLD.Entities;

namespace Argolis.Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Fluent interface to simplify creating of supported operations
    /// </summary>
    public class SupportedOperationBuilder
    {
        private readonly IList<OperationMeta> operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperationBuilder"/> class.
        /// </summary>
        public SupportedOperationBuilder(IList<OperationMeta> operations)
        {
            this.operations = operations;
        }

        /// <summary>
        /// Includes the GET operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsGet(
            string title = null,
            string description = null)
        {
            return this.Supports(HttpMethod.Get, title, description);
        }

        /// <summary>
        /// Includes the PUT operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPut(
            string title = null,
            string description = null,
            IriRef? expects = null)
        {
            return this.Supports(HttpMethod.Put, title, description, expects);
        }

        /// <summary>
        /// Includes the POST operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPost(
            string title = null,
            string description = null,
            IriRef? expects = null,
            IriRef? returns = null)
        {
            return this.Supports(HttpMethod.Post, title, description, expects, returns);
        }

        /// <summary>
        /// Includes the DELETE operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsDelete(
            string title = null,
            string description = null,
            IriRef? returns = null)
        {
            return this.Supports(HttpMethod.Delete, title, description, returns: returns);
        }

        /// <summary>
        /// Includes the PATCH operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPatch(
            string title = null,
            string description = null,
            IriRef? expects = null,
            IriRef? returns = null)
        {
            return this.Supports(HttpMethod.Patch, title, description, expects, returns);
        }

        /// <summary>
        /// Includes an operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder Supports(
            string method,
            string title = "",
            string description = "",
            IriRef? expects = null,
            IriRef? returns = null)
        {
            this.operations.Add(new OperationMeta(method, title, description, expects, returns));

            return this;
        }
    }
}
