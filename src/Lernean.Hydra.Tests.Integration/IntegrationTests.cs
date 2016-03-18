using System;
using Nancy;
using Nancy.Testing;
using VDS.RDF.Query.Builder;
using Vocab;
using Xunit;
using HCore = Hydra.Hydra;

namespace Lernean.Hydra.Tests.Integration
{
    public class IntegrationTests
    {
        private readonly Browser _browser;

        public IntegrationTests()
        {
            _browser = new Browser(new DefaultNancyBootstrapper(), context => context.HostName("hydra.guru"));
        }

        [Fact]
        public void Should_include_supported_class_in_documentation_response()
        {
            // when
            var response = _browser.Get("api", context =>
            {
                context.Accept("text/turtle");
            });
            var documentation = response.Body.AsRdf();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(Rdf.type)).Object(new Uri(HCore.ApiDocumentation)))
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(HCore.supportedClass)).Object("class"))
                .Filter(exb => exb.Variable("class") == exb.Constant(new Uri("http://example.api/o#Issue")))
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Theory]
        [InlineData("Title", Xsd.@string)]
        [InlineData("LikesCount", Xsd.@int)]
        [InlineData("DateCreated", Xsd.dateTime)]
        [InlineData("DateDeleted", Xsd.dateTime)]
        [InlineData("IsResolved", Xsd.boolean)]
        public void Should_map_default_predicates_for_property_types(string title, string predicate)
        {
            // when
            var response = _browser.Get("api", context =>
            {
                context.Accept("text/turtle");
            });
            var documentation = response.Body.AsRdf();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(HCore.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.title)).Object("title"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.property)).Object(new Uri(predicate)))
                .Filter(exb => exb.Str(exb.Variable("title")) == title)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public void Should_fill_class_description_from_DescriptionAttribute()
        {
            // given
            string expectedDescription = "The number of people who liked this issue";

            // when
            var response = _browser.Get("api", context =>
            {
                context.Accept("text/turtle");
            });
            var documentation = response.Body.AsRdf();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(HCore.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.description)).Object("description"))
                .Filter(exb => exb.Variable("description") == expectedDescription)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public void Should_fill_property_description_from_DescriptionAttribute()
        {
            // given
            string expectedDescription = "An issue reported by our users";

            // when
            var response = _browser.Get("api", context =>
            {
                context.Accept("text/turtle");
            });
            var documentation = response.Body.AsRdf();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject(new Uri("http://example.api/o#Issue")).PredicateUri(new Uri(HCore.description)).Object("description"))
                .Filter(exb => exb.Variable("description") == expectedDescription)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }
    }
}