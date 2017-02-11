using System;
using System.Reflection;
using Argolis.Hydra.Annotations;

namespace Argolis.Hydra.Discovery.SupportedClasses
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
