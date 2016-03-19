using System;
using System.Collections.Generic;
using System.Reflection;
using NullGuard;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Maps underlying type of <see cref="Nullable{T}"/> properties to RDF type.
    /// </summary>
    public class XsdDatatypesNullablesMappingPolicy : XsdDatatypesMappingPolicy, IPropertyRangeMappingPolicy
    {
        [return: AllowNull]
        Uri IPropertyRangeMappingPolicy.MapType(PropertyInfo property, IReadOnlyDictionary<Type, Uri> classIds)
        {
            if (property.PropertyType.IsConstructedGenericType)
            {
                var genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();

                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return GetMappedXsdTypeUri(property.PropertyType.GenericTypeArguments[0]);
                }
            }

            return null;
        }
    }
}
