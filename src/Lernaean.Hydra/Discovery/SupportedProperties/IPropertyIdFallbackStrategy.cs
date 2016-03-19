using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Contract for calculating supported property identifier, where not
    /// supplied explicitly
    /// </summary>
    public interface IPropertyIdFallbackStrategy
    {
        /// <summary>
        /// Gets the property identifier.
        /// </summary>
        string GetPropertyId(PropertyInfo property, string propertyName, Uri classIds);
    }
}