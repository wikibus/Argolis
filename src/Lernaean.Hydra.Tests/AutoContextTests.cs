using System;
using System.Globalization;
using System.Reflection;
using System.Threading;
using FluentAssertions;
using Hydra.Serialization;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests
{
    public class AutoContextTests
    {
        [Fact]
        public void When_created_should_include_all_properties()
        {
            // given
            var context = new AutoContext<Issue>(new Uri("http://example.api/o#Issue"));

            // then
            context.Count.Should().Be(9);
        }

        [Fact]
        public void When_created_should_not_include_reserved_keywords()
        {
            // given
            var context = new AutoContext<Issue>(new Uri("http://example.api/o#Issue"));

            // then
            context.Should().NotContainKey(JsonLdKeywords.Id);
            context.Should().NotContainKey(JsonLdKeywords.Type);
            context.Should().NotContainKey(JsonLdKeywords.Context);
        }

        [Fact]
        public void When_created_should_respect_Newtonsoft_property_attribute()
        {
            // given
            var context = new AutoContext<Issue>(new Uri("http://example.api/o#Issue"));

            // then
            context["titel"].Should().NotBeNull();
        }

        [Fact]
        public void When_created_should_extend_given_context_object()
        {
            // given
            var manualContext = JObject.Parse("{ 'titel': 'dcterms:title' }");

            // when
            var context = new AutoContext<Issue>(manualContext, new Uri("http://example.api/o#Issue"));

            // then
            context["titel"].ToString().Should().Be("dcterms:title");
        }

        [Fact]
        public void When_modified_should_allow_changing_property_definition()
        {
            // given
            var context = new AutoContext<Issue>(new Uri("http://example.api/o#Issue"));
            var expectedMapping = JObject.Parse(@"{
  '@id': 'http://example.api/o#Issue/projectId',
  '@type': '@id'
}");

            // when
            context.Property(i => i.ProjectId, propName => propName.Type().Id());

            // then
            Assert.True(JToken.DeepEquals(context["projectId"], expectedMapping));
        }

        [Fact]
        public void When_modified_should_allow_remapping_expanded_property_definition()
        {
            // given
            var contextBefore = JObject.Parse(@"{
  'projectId': {
    '@id': 'http://example.api/o#Issue/projectId',
    '@type': '@vocab'
  }
}");
            var context = new AutoContext<Issue>(contextBefore, new Uri("http://example.api/o#Issue"));
            var contextAfter = JObject.Parse(@"
{
  '@id': 'http://example.api/o#Issue/projectId',
  '@type': '@id'
}");

            // when
            context.Property(i => i.ProjectId, propName => propName.Type().Id());

            // then
            Assert.True(JToken.DeepEquals(context["projectId"], contextAfter));
        }
    }
}