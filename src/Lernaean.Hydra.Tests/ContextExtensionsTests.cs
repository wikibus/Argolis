using FluentAssertions;
using Hydra;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests
{
    public class ContextExtensionsTests
    {
        private static readonly JToken EmptyContext = JObject.Parse("{}");
        private static readonly JToken SingleContext;

        static ContextExtensionsTests()
        {
            SingleContext = new JObject(
                "xsd".IsPrefixOf(Xsd.BaseUri),
                "givenName".IsProperty(Foaf.givenName),
                new JProperty("time_prop", "xsd:time"));
        }

        [Fact]
        public void Should_return_null_if_it_is_no_mapped()
        {
            // given
            const string term = "no_such_term";

            // when
            var uri = ContextHelpers.GetExpandedIri(EmptyContext, term);

            // then
            uri.Should().BeNull();
        }

        [Fact]
        public void Should_return_directly_mapped_term()
        {
            // given
            const string term = "givenName";

            // when
            var uri = ContextHelpers.GetExpandedIri(SingleContext, term);

            // then
            uri.Should().Be(Foaf.givenName);
        }

        [Fact]
        public void Should_return_mapped_prefixed_term()
        {
            // given
            const string term = "xsd:integer";

            // when
            var uri = ContextHelpers.GetExpandedIri(SingleContext, term);

            // then
            uri.Should().Be(Xsd.integer);
        }

        [Fact]
        public void Should_return_term_mapped_to_prefixed_name()
        {
            // given
            const string term = "time_prop";

            // when
            var uri = ContextHelpers.GetExpandedIri(SingleContext, term);

            // then
            uri.Should().Be(Xsd.time);
        }
    }
}