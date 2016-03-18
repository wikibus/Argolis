using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Hydra.Annotations;
using Hydra.Core;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Finds types marked with <see cref="SupportedClassAttribute"/>
    /// </summary>
    /// <seealso cref="IDocumentedTypeSelector" />
    public abstract class AssemblyAnnotatedTypeSelector : IDocumentedTypeSelector
    {
        /// <summary>
        /// Gets the assemblies to scan for supported classes
        /// </summary>
        protected abstract IEnumerable<Assembly> Assemblies { get; }

        /// <summary>
        /// Scans <see cref="Assemblies"/> and retrieves types to include in
        /// <see cref="ApiDocumentation"/>
        /// </summary>
        public ICollection<Type> FindTypes()
        {
            return (from assembly in Assemblies
                    from type in assembly.GetTypes()
                    where type.GetCustomAttributes<SupportedClassAttribute>().Any()
                    select type).ToList();
        }
    }
}
