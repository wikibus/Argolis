using System;
using System.Collections.Generic;
using System.Reflection;
using JsonLD.Entities;
using Vocab;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Contract for selecting a <see cref="Rdfs.range"/> for a <see cref="PropertyInfo"/>
    /// </summary>
    public interface IPropertyRangeRetrievalPolicy
    {
        /// <summary>
        /// Gets the range for property <paramref cref="prop"/>
        /// </summary>
        /// <param name="prop">The model property</param>
        /// <param name="classIds">identifiers mapped to supported classes</param>
        /// <returns>An identifier or null if none could be determined</returns>
        IriRef? GetRange(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds);
    }
}
