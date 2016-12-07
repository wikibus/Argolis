using System;
using System.Collections.Generic;
using System.Reflection;

namespace Argolis.Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Contract for getting operations supported by classes and properties
    /// </summary>
    public interface ISupportedOperations
    {
        /// <summary>
        /// Gets the type, which these operations apply to
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the supported operations for a supported class.
        /// </summary>
        IEnumerable<OperationMeta> GetSupportedClassOperations();

        /// <summary>
        /// Gets the supported operations for a supported property .
        /// </summary>
        /// <param name="property">The supported property.</param>
        IEnumerable<OperationMeta> GetSupportedPropertyOperations(PropertyInfo property);
    }
}
