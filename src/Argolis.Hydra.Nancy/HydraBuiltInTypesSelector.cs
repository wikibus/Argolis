using System;
using System.Collections.Generic;
using Argolis.Hydra.Discovery.SupportedClasses;
using Argolis.Hydra.Resources;

namespace Argolis.Hydra.Nancy
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
                typeof(Resource),
                typeof(PartialCollectionView)
            };
        }
    }
}
