using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Annotations;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Finds types in assembly marked with <see cref="SupportedClassAttribute"/>
    /// </summary>
    /// <seealso cref="ISupportedClassSource" />
    public class AssemblySupportedClassSource : ISupportedClassSource
    {
        private readonly Assembly _assembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblySupportedClassSource"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public AssemblySupportedClassSource(Assembly assembly)
        {
            _assembly = assembly;
        }

        /// <inheritdoc />
        public ICollection<Type> FindTypes()
        {
            return (from type in _assembly.GetTypes()
                    where type.GetCustomAttributes<SupportedClassAttribute>().Any()
                    select type).ToList();
        }
    }
}
