using System;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Contract for creating supported hydra:<see cref="Class"/>
    /// </summary>
    public interface IRdfTypeProviderPolicy
    {
        /// <summary>
        /// Creates a hydra:Class from <see cref="Type"/>
        /// </summary>
        /// <param name="documentedType">Type to document.</param>
        Uri Create(Type documentedType);
    }
}
