using System;
using System.Reflection;
using Hydra.DocumentationDiscovery;
using NullGuard;

namespace Hydra.SupportedProperties
{
    /// <summary>
    /// Maps underlying type of <see cref="Nullable{T}"/> properties to RDF type.
    /// </summary>
    public class XsdDatatypesNullablesMapper : XsdDatatypesMapper, IPropertyTypeMapping
    {
        [return: AllowNull]
        Uri IPropertyTypeMapping.MapType(PropertyInfo property)
        {
            if (property.PropertyType.IsConstructedGenericType)
            {
                var genericTypeDefinition = property.PropertyType.GetGenericTypeDefinition();

                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    return GetMappedClassUri(property.PropertyType.GenericTypeArguments[0]);
                }
            }

            return null;
        }
    }
}