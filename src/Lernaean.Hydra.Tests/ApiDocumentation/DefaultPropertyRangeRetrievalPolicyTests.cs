using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultPropertyRangeRetrievalPolicyTests
    {
        private readonly DefaultPropertyRangeRetrievalPolicy _rangePolicy;
        private readonly IPropertyRangeMappingPolicy _propertyType;

        public DefaultPropertyRangeRetrievalPolicyTests()
        {
            _propertyType = A.Fake<IPropertyRangeMappingPolicy>();
            var mappings = new[]
            {
                _propertyType
            };
            _rangePolicy = new DefaultPropertyRangeRetrievalPolicy(mappings);
        }

        [Fact]
        public void Should_use_RangAttribute_if_present()
        {
            // when
            var iriRef = _rangePolicy.GetRange(typeof (Issue).GetProperty("ProjectId"), new Dictionary<Type, Uri>());

            // then
            iriRef.Should().Be((IriRef)"http://example.api/o#project");
        }

        [Fact]
        public void Should_map_property_range_to_RDF_type()
        {
            // given
            var propertyInfo = typeof(Issue).GetProperty("Content");
            var mappedPredicate = new Uri(Xsd.@string);
            var classIds = new Dictionary<Type, Uri>
            {
                { typeof(Issue), new Uri("http://example.com/issue") }
            };
            A.CallTo(() => _propertyType.MapType(propertyInfo, classIds)).Returns(mappedPredicate);

            // when
            var range = _rangePolicy.GetRange(propertyInfo, classIds);

            // then
            range.Should().Be((IriRef)mappedPredicate);
        }
    }
}