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
    /// <seealso cref="IDocumentedTypeSelector" />
    public class AssemblyDocumentedTypeSelector : IDocumentedTypeSelector
    {
        private readonly Assembly _assembly;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssemblyDocumentedTypeSelector"/> class.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public AssemblyDocumentedTypeSelector(Assembly assembly)
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
