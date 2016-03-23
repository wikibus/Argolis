using System;
using System.Collections.Generic;

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
        internal SupportedOperationBuilder(IList<OperationMeta> operations)
        {
            _operations = operations;
        }

        /// <summary>
        /// Includes the GET operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsGet()
        {
            return Supports(HttpMethod.Get);
        }

        /// <summary>
        /// Includes the PUT operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPut()
        {
            return Supports(HttpMethod.Put);
        }

        /// <summary>
        /// Includes the POST operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPost()
        {
            return Supports(HttpMethod.Post);
        }

        /// <summary>
        /// Includes the DELETE operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsDelete()
        {
            return Supports(HttpMethod.Delete);
        }

        /// <summary>
        /// Includes the PATCH operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsPatch()
        {
            return Supports(HttpMethod.Patch);
        }

        /// <summary>
        /// Includes an operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder Supports(string method)
        {
            _operations.Add(new OperationMeta
            {
                Method = method
            });

            return this;
        }
    }
}
