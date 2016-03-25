﻿using FluentAssertions;
using Hydra.Discovery.SupportedProperties;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultPropertiesSelectionPolicyTests
    {
        private readonly DefaultPropertiesSelectionPolicy _policy;

        public DefaultPropertiesSelectionPolicyTests()
        {
            _policy = new DefaultPropertiesSelectionPolicy();
        }

        [Fact]
        public void Should_ignore_members_marked_with_IgnoreDataMember()
        {
            // when
            var shouldIncludeProperty = _policy.ShouldIncludeProperty(typeof(User).GetProperty("DataMemberIgnored"));

            // then
            shouldIncludeProperty.Should().BeFalse();
        }

        [Fact]
        public void Should_ignore_members_marked_with_JsonIgnore()
        {
            // when
            var shouldIncludeProperty = _policy.ShouldIncludeProperty(typeof(User).GetProperty("JsonIgnored"));

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
            var shouldIncludeProperty = _policy.ShouldIncludeProperty(typeof(User).GetProperty(property));

            // then
            shouldIncludeProperty.Should().BeFalse();
        }
    }
}