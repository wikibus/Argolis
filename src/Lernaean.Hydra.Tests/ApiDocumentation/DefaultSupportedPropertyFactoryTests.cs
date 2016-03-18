using System;
using System.Collections.Generic;
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
        private readonly IPropertyRangeMapper _propertyType;

        public DefaultSupportedPropertyFactoryTests()
        {
            _propertyType = A.Fake<IPropertyRangeMapper>();
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
            A.CallTo(() => _propertyType.MapType(A<PropertyInfo>._, A<IReadOnlyDictionary<Type, Uri>>._)).Returns(mappedPredicate);

            // when
            var property = _factory.Create(typeof(Issue).GetProperty("Id"), new Dictionary<Type, Uri>());

            // then
            property.Property.Range.Should().Be((IriRef)mappedPredicate);
        }
    }
}