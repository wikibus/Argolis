using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Contract for mapping property type to RDF type identifier
    /// </summary>
    public interface IPropertyRangeMapper
    {
        /// <summary>
        /// Maps a <see cref="PropertyInfo.PropertyType"/> to RDF type.
        /// </summary>
        /// <param name="property">The property to map type of</param>
        /// <param name="classIds">Identifiers mapped to discovered supported classes</param>
        /// <returns>Absolute class identifier or null if type cannot be mapped</returns>
        Uri MapType(PropertyInfo property, IReadOnlyDictionary<Type, Uri> classIds);
    }
}