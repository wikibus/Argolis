using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedProperties;
using Nancy.Bootstrapper;

namespace Hydra.Nancy
{
    /// <summary>
    /// Registrations of all necessary components
    /// </summary>
    public class HydraRegistrations : Registrations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraRegistrations"/> class.
        /// </summary>
        public HydraRegistrations()
        {
            RegisterWithDefault<IRdfTypeProviderPolicy>(typeof(AttributeRdfTypeProviderPolicy));
            RegisterWithDefault<ISupportedPropertySelectionPolicy>(typeof(AllPublicPropertiesSelectionPolicy));
            RegisterWithDefault<ISupportedPropertyFactory>(typeof(DefaultSupportedPropertyFactory));
            RegisterWithDefault<ISupportedPropertyMetaProvider>(typeof(DefaultSupportedPropertyMetaProvider));
            RegisterWithDefault<ISupportedClassMetaProvider>(typeof(DefaultSupportedClassMetaProvider));
            RegisterWithDefault<IPropertyPredicateIdPolicy>(typeof(DefaultPropertyIdPolicy));
            RegisterWithUserThenDefault<IDocumentedTypeSelector>(new[]
            {
                typeof(HydraBuiltInTypesSelector)
            });
            RegisterWithUserThenDefault<IPropertyRangeMappingPolicy>(new[]
            {
                typeof(XsdDatatypesMappingPolicy),
                typeof(XsdDatatypesNullablesMappingPolicy),
                typeof(SupportedClassRangeMappingPolicy),
            });
        }
    }
}
