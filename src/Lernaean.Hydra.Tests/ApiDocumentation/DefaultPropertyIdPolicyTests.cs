using System;
using FakeItEasy;
using FluentAssertions;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultPropertyIdPolicyTests
    {
        private readonly DefaultPropertyIdPolicy _policy;

        public DefaultPropertyIdPolicyTests()
        {
            _policy = new DefaultPropertyIdPolicy(A.Fake<IContextProvider>());
        }

        [Theory]
        [InlineData("http://example.org/ontolgy#Issue", "http://example.org/ontolgy#Issue/titel")]
        [InlineData("http://example.org/ontolgy/Issue", "http://example.org/ontolgy/Issue#titel")]
        public void Should_concatenate_with_separator_depending_on_class_id(string issueClassStr, string expectedPropertyId)
        {
            // given
            Uri issueClassId = new Uri(issueClassStr);

            // when
            var propertyId = _policy.GetPropertyId(typeof(Issue).GetProperty("Title"));

            // then
            propertyId.Should().Be(expectedPropertyId);
        } 

        [Fact]
        public void Should_use_actual_and_not_declaring_class()
        {
            // given
            Uri issueClassId = new Uri("http://example.org/ontolgy#Issue");

            // when
            var propertyId = _policy.GetPropertyId(typeof(Issue).GetProperty("DateCreated"));

            // then
            propertyId.Should().Be("http://example.org/ontolgy#Issue/dateCreated");
        }

        [Fact]
        public void Should_use_value_set_to_JsonProperty_attribute_for_concatentation()
        {
            // given
            var classId = new Uri("http://example.org/ontolgy/User");

            // when
            var propertyId = _policy.GetPropertyId(typeof(User).GetProperty("NotInContextWithAttribute"));

            // then
            propertyId.Should().Be("http://example.org/ontolgy/User#with_attribute");
        }   

        [Theory]
        [InlineData("Name", Foaf.givenName)]
        [InlineData("LastName", Foaf.lastName)]
        public void Should_use_property_mapped_in_jsonld_ontext(string property, string expectedPredicat)
        {
            // given
            var classId = new Uri("http://example.org/ontolgy/User");

            // when
            var propertyId = _policy.GetPropertyId(typeof(User).GetProperty(property));

            // then
            propertyId.Should().Be(expectedPredicat);
        } 
    }
}