using System;
using System.Collections.Generic;
using System.Reflection;
using Argolis.Hydra.Discovery.SupportedProperties;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Argolis.Tests.ApiDocumentation
{
    public class DefaultPropertyRangeRetrievalPolicyTests
    {
        private readonly DefaultPropertyRangeRetrievalPolicy rangePolicy;
        private readonly IPropertyRangeMappingPolicy propertyType;

        public DefaultPropertyRangeRetrievalPolicyTests()
        {
            this.propertyType = A.Fake<IPropertyRangeMappingPolicy>();
            var mappings = new[]
            {
                this.propertyType
            };
            this.rangePolicy = new DefaultPropertyRangeRetrievalPolicy(mappings);
        }

        [Fact]
        public void Should_use_RangAttribute_if_present()
        {
            // when
            var iriRef = this.rangePolicy.GetRange(typeof(Issue).GetProperty("ProjectId"), new Dictionary<Type, Uri>());

            // then
            iriRef.Should().Be((IriRef)"http://example.api/o#project");
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
            A.CallTo(() => this.propertyType.MapType(A<PropertyInfo>._, A<IReadOnlyDictionary<Type, Uri>>._)).Returns(mappedPredicate);

            // when
            var range = this.rangePolicy.GetRange(typeof(Issue).GetProperty("Content"), classIds);

            // then
            range.Should().Be((IriRef)mappedPredicate);
        }
    }
}