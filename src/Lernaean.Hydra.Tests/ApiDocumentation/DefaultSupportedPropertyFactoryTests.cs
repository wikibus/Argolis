using System;
using System.Collections.Generic;
using System.Reflection;
using FakeItEasy;
using FluentAssertions;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedOperations;
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
        private readonly DefaultSupportedPropertyFactory factory;
        private readonly ISupportedPropertyMetaProvider metaProvider;
        private readonly IPropertyRangeRetrievalPolicy propertyRangeRetrievalPolicy;
        private readonly IPropertyPredicateIdPolicy propertyPredicateIdPolicy;

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
            this.metaProvider = A.Fake<ISupportedPropertyMetaProvider>();
            this.propertyRangeRetrievalPolicy = A.Fake<IPropertyRangeRetrievalPolicy>();
            this.propertyPredicateIdPolicy = A.Fake<IPropertyPredicateIdPolicy>();
            this.factory = new DefaultSupportedPropertyFactory(
                this.propertyRangeRetrievalPolicy,
                this.metaProvider,
                this.propertyPredicateIdPolicy,
                A.Fake<ISupportedOperationFactory>());
        }

        [Fact]
        public void Should_copy_writeable_property_from_meta()
        {
            // given
            A.CallTo(() => this.metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Writeable = true });

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Writeable.Should().BeTrue();
        }

        [Fact]
        public void Should_copy_readble_property_from_meta()
        {
            // given
            A.CallTo(() => this.metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Readable = true });

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Readable.Should().BeTrue();
        }

        [Fact]
        public void Should_copy_required_property_from_meta()
        {
            // given
            A.CallTo(() => this.metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Required = true });

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Required.Should().BeTrue();
        }

        [Fact]
        public void Should_copy_description_property_from_meta()
        {
            // given
            const string description = "dummy descr";
            A.CallTo(() => this.metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Description = description });

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Description.Should().Be(description);
        }

        [Fact]
        public void Should_copy_title_property_from_meta()
        {
            // given
            const string title = "title";
            A.CallTo(() => this.metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { Title = title });

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Title.Should().Be(title);
        }

        [Fact]
        public void Should_set_discovered_property_id()
        {
            // given
            const string propertyId = "http://argolis.test/prop";
            A.CallTo(() => this.propertyPredicateIdPolicy.GetPropertyId(PropertyInfo))
                .Returns(propertyId);

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Property.Id.Should().Be(propertyId);
        }

        [Fact]
        public void Should_set_discovered_property_range()
        {
            // given
            const string expectedRange = "xsd:int";
            A.CallTo(() => this.propertyRangeRetrievalPolicy.GetRange(PropertyInfo, ClassIds))
                .Returns(new IriRef(expectedRange));

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Property.Range.Should().Be((IriRef)expectedRange);
        }

        [Fact]
        public void Should_throw_if_property_id_was_not_found()
        {
            // given
            A.CallTo(() => this.propertyPredicateIdPolicy.GetPropertyId(PropertyInfo)).Returns(null);

            // then
            Assert.Throws<ApiDocumentationException>(() => this.factory.Create(PropertyInfo, ClassIds));
        }

        [Fact]
        public void Should_add_hydra_Link_type_When_property_is_marked_as_link()
        {
            // given
            A.CallTo(() => this.metaProvider.GetMeta(PropertyInfo))
                .Returns(new SupportedPropertyMeta { IsLink = true });

            // when
            var property = this.factory.Create(PropertyInfo, ClassIds);

            // then
            property.Property.Types.Should().Contain(global::Hydra.Hydra.Link);
        }
    }
}