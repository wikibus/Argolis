using System;
using System.Collections.Generic;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Contract for creating supported classes
    /// </summary>
    public interface ISupportedClassFactory
    {
        /// <summary>
        /// Creates a supported class
        /// </summary>
        /// <param name="supportedClassType">Underlying model types of the supported class.</param>
        /// <param name="types">Identifiers of all discovered supported types.</param>
        Class Create(Type supportedClassType, IReadOnlyDictionary<Type, Uri> types);
    }
}
