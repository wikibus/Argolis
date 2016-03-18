using System;
using System.Reflection;
using Hydra.Annotations;
using JsonLD.Entities;

namespace Hydra.DocumentationDiscovery
{
    /// <summary>
    /// Default implementation of <see cref="IRdfTypeProviderPolicy"/>
    /// </summary>
    public class AttributeRdfTypeProviderPolicy : IRdfTypeProviderPolicy
    {
        /// <inheritdoc />
        public Uri Create(Type documentedType)
        {
            var supportedClassAttribute = documentedType.GetCustomAttribute<SupportedClassAttribute>(true);

            return supportedClassAttribute.RdfClass;
        }
    }
}