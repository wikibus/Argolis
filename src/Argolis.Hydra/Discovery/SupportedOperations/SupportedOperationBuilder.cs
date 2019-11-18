using System.Collections.Generic;
using JsonLD.Entities;
using NullGuard;

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
            string title = "GET",
            string description = "")
        {
            return this.Supports(HttpMethod.Get, title, description);
        }

        /// <summary>
        /// Includes the PUT operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPut(
            string title = "PUT",
            string description = "",
            [AllowNull] IriRef? expects = null)
        {
            return this.Supports(HttpMethod.Put, title, description, expects);
        }

        /// <summary>
        /// Includes the POST operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPost(
            string title = "POST",
            string description = "",
            [AllowNull] IriRef? expects = null,
            [AllowNull] IriRef? returns = null)
        {
            return this.Supports(HttpMethod.Post, title, description, expects, returns);
        }

        /// <summary>
        /// Includes the DELETE operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsDelete(
            string title = "DELETE",
            string description = "",
            [AllowNull] IriRef? returns = null)
        {
            return this.Supports(HttpMethod.Delete, title, description, returns: returns);
        }

        /// <summary>
        /// Includes the PATCH operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPatch(
            string title = "PATCH",
            string description = "",
            [AllowNull] IriRef? expects = null,
            [AllowNull] IriRef? returns = null)
        {
            return this.Supports(HttpMethod.Patch, title, description, expects, returns);
        }

        /// <summary>
        /// Includes an operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder Supports(
            string method,
            [AllowNull] string title = null,
            string description = "",
            [AllowNull] IriRef? expects = null,
            [AllowNull] IriRef? returns = null)
        {
            this.operations.Add(new OperationMeta(method, title ?? method, description, expects, returns));

            return this;
        }
    }
}
