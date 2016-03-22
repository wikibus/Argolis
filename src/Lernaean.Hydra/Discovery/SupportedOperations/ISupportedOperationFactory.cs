using System;
using System.Collections.Generic;
using System.Reflection;
using Hydra.Core;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Contract for creating supported operations for a type
    /// </summary>
    public interface ISupportedOperationFactory
    {
        /// <summary>
        /// Creates the supported operations for supported class.
        /// </summary>
        IEnumerable<Operation> CreateOperations(Type supportedClassType, IReadOnlyDictionary<Type, Uri> classIds);
        
        /// <summary>
        /// Creates the supported operations for supported property.
        /// </summary>
        IEnumerable<Operation> CreateOperations(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds);
    }
}
