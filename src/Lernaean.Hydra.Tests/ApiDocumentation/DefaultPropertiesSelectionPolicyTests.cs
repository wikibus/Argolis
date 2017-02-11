using FluentAssertions;
using Hydra.Discovery.SupportedProperties;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultPropertiesSelectionPolicyTests
    {
        private readonly DefaultPropertiesSelectionPolicy policy;

        public DefaultPropertiesSelectionPolicyTests()
        {
            this.policy = new DefaultPropertiesSelectionPolicy();
        }

        [Fact]
        public void Should_ignore_members_marked_with_IgnoreDataMember()
        {
            // when
            var shouldIncludeProperty = this.policy.ShouldIncludeProperty(typeof(User).GetProperty("DataMemberIgnored"));

            // then
            shouldIncludeProperty.Should().BeFalse();
        }

        [Fact]
        public void Should_ignore_members_marked_with_JsonIgnore()
        {
            // when
            var shouldIncludeProperty = this.policy.ShouldIncludeProperty(typeof(User).GetProperty("JsonIgnored"));

            // then
            shouldIncludeProperty.Should().BeFalse();
        }

        [Theory]
        [InlineData("Id")]
        [InlineData("Type")]
        [InlineData("Context")]
        public void Should_ignore_jsonld_keyword_properties(string property)
        {
            // when
            var shouldIncludeProperty = this.policy.ShouldIncludeProperty(typeof(User).GetProperty(property));

            // then
            shouldIncludeProperty.Should().BeFalse();
        }

        [Theory]
        [InlineData("DataMemberIgnored")]
        [InlineData("JsonIgnored")]
        public void Should_ignore_property_if_it_is_ignored_with_attrbiute(string property)
        {
            // when
            var shouldIncludeProperty = this.policy.ShouldIncludeProperty(typeof(User).GetProperty(property));

            // then
            shouldIncludeProperty.Should().BeFalse();
        }
    }
}