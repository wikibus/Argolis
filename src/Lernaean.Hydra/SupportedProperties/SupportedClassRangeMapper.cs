using System;
using System.Collections.Generic;
using System.Reflection;
using Hydra.DocumentationDiscovery;
using NullGuard;

namespace Hydra.SupportedProperties
{
    /// <summary>
    /// Looks up property's range in discovered supported classes
    /// </summary>
    public class SupportedClassRangeMapper : IPropertyRangeMapper
    {
        /// <inheritdoc />
        [return: AllowNull]
        public Uri MapType(PropertyInfo property, IReadOnlyDictionary<Type, Uri> classIds)
        {
            if (classIds.ContainsKey(property.PropertyType))
            {
                return classIds[property.PropertyType];
            }

            return null;
        }
    }
}