using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Annotations;
using JsonLD.Entities;
using NullGuard;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Default implementation of <see cref="IPropertyRangeMappingPolicy"/>, which
    /// looks at <see cref="RangeAttribute"/> and all available instances of
    /// <see cref="IPropertyRangeMappingPolicy"/>
    /// </summary>
    public class DefaultPropertyRangeRetrievalPolicy : IPropertyRangeRetrievalPolicy
    {
        private readonly IEnumerable<IPropertyRangeMappingPolicy> _propertyTypeMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPropertyRangeRetrievalPolicy"/> class.
        /// </summary>
        /// <param name="propertyTypeMappings">The property type mappings.</param>
        public DefaultPropertyRangeRetrievalPolicy(IEnumerable<IPropertyRangeMappingPolicy> propertyTypeMappings)
        {
            _propertyTypeMappings = propertyTypeMappings;
        }

        /// <inheritdoc />
        [return: AllowNull]
        public IriRef? GetRange(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            var rangeAttribute = prop.GetCustomAttribute<RangeAttribute>();

            if (rangeAttribute != null)
            {
                return (IriRef)rangeAttribute.Range;
            }

            var mappedType = (from mapping in _propertyTypeMappings
                              let mapType = mapping.MapType(prop, classIds)
                              where mapType != null
                              select mapType).FirstOrDefault();
            if (mappedType == null)
            {
                return null;
            }

            return (IriRef)mappedType;
        }
    }
}