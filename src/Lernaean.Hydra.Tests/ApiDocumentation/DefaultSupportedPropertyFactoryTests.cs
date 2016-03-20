using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultSupportedPropertyFactoryTests
    {
        private static readonly PropertyInfo PropertyInfo;
        private static readonly Dictionary<Type, Uri> ClassIds;
        private readonly DefaultSupportedPropertyFactory _factory;
        private readonly IPropertyRangeMappingPolicy _propertyType;
        private readonly ISupportedPropertyMetaProvider _metaProvider;
        private readonly IPropertyRangeRetrievalPolicy _propertyRangeRetrievalPolicy;
        private readonly IPropertyPredicateIdPolicy _propertyPredicateIdPolicy;

        static DefaultSupportedPropertyFactoryTests()
        {
            PropertyInfo = typeof(Issue).GetProperty("Title");
            ClassIds = new Dictionary<Type, Uri>
            {
                { typeof(Issue), new Uri("http://argolis.test/Issue") }
            };
        }

        public DefaultSupportedPropertyFactoryTests()
        {
            _propertyType = A.Fake<IPropertyRangeMappingPolicy>();
            _metaProvider = A.Fake<ISupportedPropertyMetaProvider>();
            _propertyRangeRetrievalPolicy = A.Fake<IPropertyRangeRetrievalPolicy>();
            _propertyPredicateIdPolicy = A.Fake<IPropertyPredicateIdPolicy>();
            _factory = new DefaultSupportedPropertyFactory(
                _propertyRangeRetrievalPolicy,
                _metaProvider,
                _propertyPredicateIdPolicy);
        }

        [Fact]
        public void Should_copy_writeable_property_from_meta()
        {
            // given
            A.CallTo(() => _metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Writeable = true });

            // when
            var property = _factory.Create(PropertyInfo, ClassIds);

            // then
            property.Writeable.Should().BeTrue();
        }

        [Fact]
        public void Should_copy_readble_property_from_meta()
        {
            // given
            A.CallTo(() => _metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Readable = true });

            // when
            var property = _factory.Create(PropertyInfo, ClassIds);

            // then
            property.Readable.Should().BeTrue();
        }

        [Fact]
        public void Should_copy_description_property_from_meta()
        {
            // given
            const string description = "dummy descr";
            A.CallTo(() => _metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Description = description });

            // when
            var property = _factory.Create(PropertyInfo, ClassIds);

            // then
            property.Description.Should().Be(description);
        }

        [Fact]
        public void Should_copy_title_property_from_meta()
        {
            // given
            const string title = "title";
            A.CallTo(() => _metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Title = title});

            // when
            var property = _factory.Create(PropertyInfo, ClassIds);

            // then
            property.Title.Should().Be(title);
        }

        [Fact]
        public void Should_set_discovered_property_id()
        {
            // given
            const string propertyId = "http://argolis.test/prop";
            A.CallTo(() => _propertyPredicateIdPolicy.GetPropertyId(PropertyInfo, new Uri("http://argolis.test/Issue")))
                .Returns(propertyId);

            // when
            var property = _factory.Create(PropertyInfo, ClassIds);

            // then
            property.Property.Id.Should().Be(propertyId);
        }

        [Fact]
        public void Should_set_discovered_property_range()
        {
            // given
            const string expectedRange = "xsd:int";
            A.CallTo(() => _propertyRangeRetrievalPolicy.GetRange(PropertyInfo, ClassIds))
                .Returns(new IriRef(expectedRange));

            // when
            var property = _factory.Create(PropertyInfo, ClassIds);

            // then
            property.Property.Range.Should().Be((IriRef)expectedRange);
        }
    }
}