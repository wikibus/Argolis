using System;
using System.Collections.Generic;
using System.Reflection;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Contract for creating supported properties
    /// </summary>
    public interface ISupportedPropertyFactory
    {
        /// <summary>
        /// Creates a hydra <see cref="SupportedProperty"/> from a type's property.
        /// </summary>
        SupportedProperty Create(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds);
    }
}
