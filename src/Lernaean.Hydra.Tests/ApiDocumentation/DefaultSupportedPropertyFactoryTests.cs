using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultSupportedPropertyFactoryTests
    {
        private readonly DefaultSupportedPropertyFactory _factory;
        private readonly IPropertyRangeMappingPolicy _propertyType;

        public DefaultSupportedPropertyFactoryTests()
        {
            _propertyType = A.Fake<IPropertyRangeMappingPolicy>();
            _factory = new DefaultSupportedPropertyFactory(
                A.Fake<IPropertyRangeRetrievalPolicy>(),
                A.Fake<ISupportedPropertyMetaProvider>(),
                A.Fake<IPropertyPredicateIdPolicy>());
        }
    }
}