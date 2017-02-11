using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Argolis.Hydra.Annotations;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Discovery.SupportedClasses
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
            return (from assembly in this.Assemblies
                    from type in assembly.GetTypes()
                    where type.GetCustomAttributes<SupportedClassAttribute>().Any()
                    select type).ToList();
        }
    }
}
