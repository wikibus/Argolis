using System;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using Hydra.DocumentationDiscovery;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultSupportedPropertyFactoryTests
    {
        private readonly DefaultSupportedPropertyFactory _factory;
        private readonly IPropertyTypeMapping _propertyType;

        public DefaultSupportedPropertyFactoryTests()
        {
            _propertyType = A.Fake<IPropertyTypeMapping>();
            _factory = new DefaultSupportedPropertyFactory(
                new[]
                {
                    _propertyType
                },
                A.Fake<ISupportedPropertyMetaProvider>());
        }

        [Fact]
        public void Should_map_property_type_to_RDF_type()
        {
            // given
            var mappedPredicate = new Uri(Xsd.@string);
            A.CallTo(() => _propertyType.MapType(A<PropertyInfo>._)).Returns(mappedPredicate);

            // when
            var property = _factory.Create(typeof(Issue).GetProperty("Id"));

            // then
            property.Predicate.Should().Be((IriRef)mappedPredicate);
        }
    }
}