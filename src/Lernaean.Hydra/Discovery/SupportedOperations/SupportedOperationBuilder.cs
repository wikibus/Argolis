using System;
using System.Collections.Generic;
using JsonLD.Entities;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Fluent interface to simplify creating of supported operations
    /// </summary>
    public class SupportedOperationBuilder
    {
        [Obsolete("use set?")]
        private readonly IList<OperationMeta> _operations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperationBuilder"/> class.
        /// </summary>
        public SupportedOperationBuilder(IList<OperationMeta> operations)
        {
            _operations = operations;
        }

        /// <summary>
        /// Includes the GET operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsGet(
            string title = null,
            string description = null)
        {
            return Supports(HttpMethod.Get, title, description);
        }

        /// <summary>
        /// Includes the PUT operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPut(
            string title = null,
            string description = null,
            IriRef? expects = null)
        {
            return Supports(HttpMethod.Put, title, description, expects);
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
            return Supports(HttpMethod.Post, title, description, expects, returns);
        }

        /// <summary>
        /// Includes the DELETE operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsDelete(
            string title = null,
            string description = null,
            IriRef? returns = null)
        {
            return Supports(HttpMethod.Delete, title, description, returns: returns);
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
            return Supports(HttpMethod.Patch, title, description, expects, returns);
        }

        /// <summary>
        /// Includes an operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder Supports(
            string method,
            string title = null,
            string description = null,
            IriRef? expects = null,
            IriRef? returns = null)
        {
            _operations.Add(new OperationMeta
            {
                Method = method,
                Expects = expects,
                Returns = returns,
                Title = title,
                Description = description
            });

            return this;
        }
    }
}
