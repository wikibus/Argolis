using System;
using System.Collections.Generic;
using Hydra.Core;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Contract for finding types, which will be included in
    /// <see cref="ApiDocumentation"/>
    /// </summary>
    public interface IDocumentedTypeSelector
    {
        /// <summary>
        /// Finds types to include in <see cref="ApiDocumentation" />.
        /// </summary>
        ICollection<Type> FindTypes();
    }
}
