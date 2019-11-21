using Argolis.Hydra.Annotations;
using Argolis.Hydra.Discovery.SupportedProperties;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Argolis.Tests.ApiDocumentation
{
    public class DefaultPropertyIdPolicyTests
    {
        private readonly DefaultPropertyIdPolicy policy;
        private readonly IContextProvider contextProvider;

        public DefaultPropertyIdPolicyTests()
        {
            this.contextProvider = A.Fake<IContextProvider>();
            this.policy = new DefaultPropertyIdPolicy(this.contextProvider);
        }

        [Fact]
        public void Should_use_actual_and_not_declaring_class()
        {
            // given
            const string expectedProperty = "http://example.org/o#Issue/dateCreated";
            A.CallTo(() => this.contextProvider.GetContext(typeof(Issue)))
                .Returns(new JObject
                {
                    "dateCreated".IsProperty(expectedProperty)
                });

            // when
            var propertyId = this.policy.GetPropertyId(typeof(Issue).GetProperty("DateCreated"));

            // then
            propertyId.Should().Be(expectedProperty);
        }

        [Fact]
        public void Should_use_property_mapped_in_jsonld_ontext()
        {
            // given
            A.CallTo(() => this.contextProvider.GetContext(typeof(Issue)))
                .Returns(new JObject
                {
                    "name".IsProperty(Foaf.givenName)
                });

            // when
            var propertyId = this.policy.GetPropertyId(typeof(User).GetProperty("Name"));

            // then
            propertyId.Should().Be(Foaf.givenName);
        }

        [Fact]
        public void Should_return_annotated_JSON_property_if_absolute_URI()
        {
            var policy = new DefaultPropertyIdPolicy();

            // when
            var propertyId = policy.GetPropertyId(typeof(NoContext).GetProperty("String"));

            // then
            propertyId.Should().BeNull();
        }

        [Theory]
        [InlineData("ArgolisPropAnnotated")]
        [InlineData("JsonPropImage")]
        public void Should_return_null_if_not_found_in_context(string property)
        {
            var policy = new DefaultPropertyIdPolicy();

            // when
            var propertyId = policy.GetPropertyId(typeof(NoContext).GetProperty(property));

            // then
            propertyId.Should().Be(Schema.image);
        }

        private class NoContext
        {
            public string String { get; set; }

            [Property(Schema.image)]
            public string ArgolisPropAnnotated { get; set; }

            [JsonProperty(Schema.image)]
            public string JsonPropImage { get; set; }
        }
    }
}