using System;
using System.Threading.Tasks;
using FluentAssertions;
using Hydra.Core;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Newtonsoft.Json;
using TestNancyApp.Bootstrap;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Builder;
using Vocab;
using Xunit;
using HCore = Hydra.Hydra;

namespace Lernaean.Hydra.Tests.Integration
{
    public class IntegrationTests
    {
        private const string ExpectedApiDocPath = "http://hydra.guru/api";
        private readonly Browser _browser;

        public IntegrationTests()
        {
            _browser = new Browser(new Bootstrapper(), context => context.HostName("hydra.guru"));
        }

        [Fact]
        public async void Should_include_supported_class_in_documentation_response()
        {
            // when
            var documentation = await GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(Rdf.type)).Object(new Uri(HCore.ApiDocumentation)))
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(HCore.supportedClass)).Object("class"))
                .Filter(exb => exb.Variable("class") == exb.Constant(new Uri("http://example.api/o#Issue")))
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Theory]
        [InlineData("title", Xsd.@string)]
        [InlineData("likesCount", Xsd.@int)]
        [InlineData("dateCreated", Xsd.dateTime)]
        [InlineData("dateDeleted", Xsd.dateTime)]
        [InlineData("isResolved", Xsd.boolean)]
        public async void Should_map_default_predicate_ranges_for_primitive_property_types(string title, string predicate)
        {
            // when
            var documentation = await GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(HCore.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.title)).Object("title"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.property)).Object("pred"))
                .Where(tpb => tpb.Subject("pred").PredicateUri(new Uri(Rdfs.range)).Object(new Uri(predicate)))
                .Filter(exb => exb.Str(exb.Variable("title")) == title)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async void Should_map_predicate_range_for_property_with_supported_property_type()
        {
            // given
            const string expectedRange = "http://example.api/o#User";

            // when
            var documentation = await GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(HCore.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.title)).Object("title"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.property)).Object("pred"))
                .Where(tpb => tpb.Subject("pred").PredicateUri(new Uri(Rdfs.range)).Object(new Uri(expectedRange)))
                .Filter(exb => exb.Str(exb.Variable("title")) == "submitter")
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async void Should_map_predicate_range_for_unknown_type_to_Thing()
        {
            // given
            var expectedRange = new Uri(HCore.Resource);

            // when
            var documentation = await GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(HCore.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.title)).Object("title"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.property)).Object("pred"))
                .Where(tpb => tpb.Subject("pred").PredicateUri(new Uri(Rdfs.range)).Object(expectedRange))
                .Filter(exb => exb.Str(exb.Variable("title")) == "undocumentedClassProperty")
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async void Should_fill_class_description_from_DescriptionAttribute()
        {
            // given
            string expectedDescription = "The number of people who liked this issue";

            // when
            var documentation = await GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(HCore.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(HCore.description)).Object("description"))
                .Filter(exb => exb.Variable("description") == expectedDescription)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async void Should_fill_property_description_from_DescriptionAttribute()
        {
            // given
            string expectedDescription = "An issue reported by our users";

            // when
            var documentation = await GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject(new Uri("http://example.api/o#Issue")).PredicateUri(new Uri(HCore.description)).Object("description"))
                .Filter(exb => exb.Variable("description") == expectedDescription)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Theory]
        [InlineData(HCore.Collection)]
        [InlineData(HCore.Resource)]
        public async void Should_include_hydra_base_types_as_supported_classes(string expectedType)
        {
            // when
            var documentation = await GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(HCore.supportedClass)).Object(new Uri(expectedType)))
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async void Should_serve_API_doc_with_correct_Id()
        {
            // when
            var response = await _browser.Get("api", context => context.Accept(new MediaRange("application/json")));

            // then
            var asString = response.Body.AsString();
            dynamic apiDoc = JsonConvert.DeserializeObject(asString);

            ((string)apiDoc.id).Should().Be(ExpectedApiDocPath);
        }

        [Fact]
        public async void Should_not_contain_duplicate_operations()
        {
            var serialize = new JsonLD.Entities.EntitySerializer().Serialize(new Class(new Uri("http://ex.com/issue")));

            // when
            var documentation = await GetDocumentationGraph();
            
            // then
            var query = @"
ASK
WHERE
{
    {
    SELECT count(?o) as ?count
    WHERE
    {
        <http://example.api/o#Issue> <http://www.w3.org/ns/hydra/core#supportedOperation> ?o .
        ?o <http://www.w3.org/ns/hydra/core#method> ""GET""^^<http://www.w3.org/2001/XMLSchema#string> .
    }
    }

    FILTER (?count = 1)
}";
            documentation.Should().MatchAsk(new SparqlQueryParser().ParseFromString(query));
        }

        private async Task<IGraph> GetDocumentationGraph()
        {
            var response = await _browser.Get("api", context => { context.Accept("text/turtle"); });

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            return response.Body.AsRdf();
        }
    }
}