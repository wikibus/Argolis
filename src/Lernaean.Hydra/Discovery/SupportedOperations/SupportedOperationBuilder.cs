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
        private readonly IList<OperationMeta> _propertyOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperationBuilder"/> class.
        /// </summary>
        internal SupportedOperationBuilder(IList<OperationMeta> propertyOperations)
        {
            _propertyOperations = propertyOperations;
        }

        /// <summary>
        /// Includes the GET operation in the supported property's supported operations
        /// </summary>
        public SupportedOperationBuilder SupportsGet()
        {
            _propertyOperations.Add(new OperationMeta
            {
                Method = "GET"
            });

            return this;
        }
    }
}
