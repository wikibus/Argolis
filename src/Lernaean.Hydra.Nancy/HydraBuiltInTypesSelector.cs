using System;
using System.Collections.Generic;
using Hydra.DocumentationDiscovery;
using Hydra.Resources;

namespace Hydra.Nancy
{
    /// <summary>
    /// Selects built-in hydra types
    /// </summary>
    internal class HydraBuiltInTypesSelector : IDocumentedTypeSelector
    {
        /// <inheritdoc />
        public ICollection<Type> FindTypes()
        {
            return new[]
            {
                typeof(Collection<>),
                typeof(Resource)
            };
        }
    }
}