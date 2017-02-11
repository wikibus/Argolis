using System;
using System.Collections.Generic;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Discovery.SupportedClasses
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
