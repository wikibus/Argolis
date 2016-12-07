using System;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Contract for returning basic information about a Supported <see cref="Class"/>
    /// </summary>
    public interface ISupportedClassMetaProvider
    {
        /// <summary>
        /// Gets the basic information about a supported class.
        /// </summary>
        /// <returns>
        /// An object containing the title, description, etc.
        /// </returns>
        SupportedClassMeta GetMeta(Type type);
    }
}
