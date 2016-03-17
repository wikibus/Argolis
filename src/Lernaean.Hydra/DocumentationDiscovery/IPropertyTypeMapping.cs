using System;
using System.Reflection;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Contract for mapping property type to RDF type identifier
    /// </summary>
    public interface IPropertyTypeMapping
    {
        /// <summary>
        /// Maps a <see cref="PropertyInfo.PropertyType"/> to RDF type.
        /// </summary>
        /// <param name="property">The property to map type of</param>
        /// <returns>Absolute class identifier</returns>
        Uri MapType(PropertyInfo property);
    }
}