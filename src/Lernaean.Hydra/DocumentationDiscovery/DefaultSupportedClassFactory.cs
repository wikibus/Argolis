using System;
using System.Reflection;
using Hydra.Annotations;
using Hydra.Core;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedClassFactory"/>
    /// </summary>
    public class DefaultSupportedClassFactory : ISupportedClassFactory
    {
        /// <inheritdoc />
        public Class Create(Type documentedType)
        {
            var supportedClassAttribute = documentedType.GetCustomAttribute<SupportedClassAttribute>(true);

            return new Class(supportedClassAttribute.RdfClass.ToString());
        }
    }
}