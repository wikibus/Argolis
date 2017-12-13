using Hydra.Discovery;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedOperations;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using Nancy;
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
        public HydraRegistrations(ITypeCatalog catalog)
            : base(catalog)
        {
            this.RegisterWithDefault<IApiDocumentationFactory>(typeof(ApiDocumentationFactory), Lifetime.PerRequest);
            this.RegisterWithDefault<IRdfTypeProviderPolicy>(typeof(AttributeRdfTypeProviderPolicy));
            this.RegisterWithDefault<ISupportedPropertySelectionPolicy>(typeof(DefaultPropertiesSelectionPolicy));
            this.RegisterWithDefault<ISupportedPropertyFactory>(typeof(DefaultSupportedPropertyFactory), Lifetime.PerRequest);
            this.RegisterWithDefault<ISupportedClassFactory>(typeof(DefaultSupportedClassFactory), Lifetime.PerRequest);
            this.RegisterWithDefault<ISupportedPropertyMetaProvider>(typeof(DefaultSupportedPropertyMetaProvider));
            this.RegisterWithDefault<ISupportedClassMetaProvider>(typeof(DefaultSupportedClassMetaProvider));
            this.RegisterWithDefault<IPropertyPredicateIdPolicy>(typeof(DefaultPropertyIdPolicy));
            this.RegisterWithDefault<IPropertyRangeRetrievalPolicy>(typeof(DefaultPropertyRangeRetrievalPolicy), Lifetime.PerRequest);
            this.RegisterWithDefault<ISupportedOperationFactory>(typeof(DefaultSupportedOperationFactory), Lifetime.PerRequest);
            this.RegisterWithDefault<IContextProvider>(typeof(NullContextProvider));
            this.RegisterWithDefault<IIriTemplateFactory>(typeof(IriTemplateFactory), Lifetime.PerRequest);
            this.RegisterWithUserThenDefault<IDocumentedTypeSelector>(new[]
            {
                typeof(HydraBuiltInTypesSelector)
            });
            this.Register<IPropertyRangeMappingPolicy>(new[]
            {
                typeof(XsdDatatypesMappingPolicy),
                typeof(XsdDatatypesNullablesMappingPolicy),
                typeof(SupportedClassRangeMappingPolicy),
            });
        }
    }
}
