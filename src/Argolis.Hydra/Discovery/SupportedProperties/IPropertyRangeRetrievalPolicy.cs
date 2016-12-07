using System;
using System.Collections.Generic;
using System.Reflection;
using JsonLD.Entities;
using Vocab;

namespace Argolis.Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Contract for selecting a <see cref="Rdfs.range"/> for a <see cref="PropertyInfo"/>
    /// </summary>
    public interface IPropertyRangeRetrievalPolicy
    {
        /// <summary>
        /// Gets the range for the <paramref name="property"/>
        /// </summary>
        /// <param name="property">The model property</param>
        /// <param name="classIds">identifiers mapped to supported classes</param>
        IriRef? GetRange(PropertyInfo property, IReadOnlyDictionary<Type, Uri> classIds);
    }
}
