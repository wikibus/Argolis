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
        private readonly IEnumerable<IPropertyRangeMappingPolicy> propertyTypeMappings;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultPropertyRangeRetrievalPolicy"/> class.
        /// </summary>
        /// <param name="propertyTypeMappings">The property type mappings.</param>
        public DefaultPropertyRangeRetrievalPolicy(IEnumerable<IPropertyRangeMappingPolicy> propertyTypeMappings)
        {
            this.propertyTypeMappings = propertyTypeMappings;
        }

        /// <summary>
        /// Gets the range for the <paramref name="property" />
        /// </summary>
        /// <param name="property">The model property</param>
        /// <param name="classIds">identifiers mapped to supported classes</param>
        /// <returns>An identifier or null if none could be determined</returns>
        [return: AllowNull]
        public IriRef? GetRange(PropertyInfo property, IReadOnlyDictionary<Type, Uri> classIds)
        {
            var rangeAttribute = property.GetCustomAttribute<RangeAttribute>();

            if (rangeAttribute != null)
            {
                return (IriRef)rangeAttribute.Range;
            }

            var mappedType = (from mapping in this.propertyTypeMappings
                              let mapType = mapping.MapType(property, classIds)
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
