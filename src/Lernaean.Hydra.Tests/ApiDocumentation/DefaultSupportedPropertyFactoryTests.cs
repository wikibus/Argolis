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
                new[]
                {
                    _propertyType
                },
                A.Fake<ISupportedPropertyMetaProvider>(),
                A.Fake<IPropertyPredicateIdPolicy>());
        }

        [Fact]
        public void Should_map_property_range_to_RDF_type()
        {
            // given
            var mappedPredicate = new Uri(Xsd.@string);
            var classIds = new Dictionary<Type, Uri>
            {
                { typeof(Issue), new Uri("http://example.com/issue") }
            };
            A.CallTo(() => _propertyType.MapType(A<PropertyInfo>._, A<IReadOnlyDictionary<Type, Uri>>._)).Returns(mappedPredicate);

            // when
            var property = _factory.Create(typeof(Issue).GetProperty("Id"), classIds);

            // then
            property.Property.Range.Should().Be((IriRef)mappedPredicate);
        }
    }
}