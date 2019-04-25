using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Argolis.Hydra.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using TestHydraApp;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query.Builder;
using Vocab;
using Xunit;

namespace Argolis.Tests.Integration
{
    public class IntegrationTests
    {
        private const string ExpectedApiDocPath = "http://hydra.guru/api";
        private readonly HttpClient browser;

        public IntegrationTests()
        {
            this.browser = new WebApplicationFactory<Startup>().CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri("http://hydra.guru")
            });
        }

        [Fact]
        public async Task Should_include_supported_class_in_documentation_response()
        {
            // when
            var documentation = await this.GetDocumentationGraph();

            Console.WriteLine(documentation.Triples.Count);

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(Rdf.type)).Object(new Uri(Vocab.Hydra.ApiDocumentation)))
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(Vocab.Hydra.supportedClass)).Object("class"))
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
        public async Task Should_map_default_predicate_ranges_for_primitive_property_types(string title, string predicate)
        {
            // when
            var documentation = await this.GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(Vocab.Hydra.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(Vocab.Hydra.title)).Object("title"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(Vocab.Hydra.property)).Object("pred"))
                .Where(tpb => tpb.Subject("pred").PredicateUri(new Uri(Rdfs.range)).Object(new Uri(predicate)))
                .Filter(exb => exb.Str(exb.Variable("title")) == title)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async Task Should_map_predicate_range_for_property_with_supported_property_type()
        {
            // given
            const string expectedRange = "http://example.api/o#User";

            // when
            var documentation = await this.GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(Vocab.Hydra.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(Vocab.Hydra.title)).Object("title"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(Vocab.Hydra.property)).Object("pred"))
                .Where(tpb => tpb.Subject("pred").PredicateUri(new Uri(Rdfs.range)).Object(new Uri(expectedRange)))
                .Filter(exb => exb.Str(exb.Variable("title")) == "submitter")
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async Task Should_map_predicate_range_for_unknown_type_to_Thing()
        {
            // given
            var expectedRange = new Uri(Vocab.Hydra.Resource);

            // when
            var documentation = await this.GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(Vocab.Hydra.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(Vocab.Hydra.title)).Object("title"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(Vocab.Hydra.property)).Object("pred"))
                .Where(tpb => tpb.Subject("pred").PredicateUri(new Uri(Rdfs.range)).Object(expectedRange))
                .Filter(exb => exb.Str(exb.Variable("title")) == "undocumentedClassProperty")
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async Task Should_fill_class_description_from_DescriptionAttribute()
        {
            // given
            string expectedDescription = "The number of people who liked this issue";

            // when
            var documentation = await this.GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("class").PredicateUri(new Uri(Vocab.Hydra.supportedProperty)).Object("prop"))
                .Where(tpb => tpb.Subject("prop").PredicateUri(new Uri(Vocab.Hydra.description)).Object("description"))
                .Filter(exb => exb.Variable("description") == expectedDescription)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async Task Should_fill_property_description_from_DescriptionAttribute()
        {
            // given
            string expectedDescription = "An issue reported by our users";

            // when
            var documentation = await this.GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject(new Uri("http://example.api/o#Issue")).PredicateUri(new Uri(Vocab.Hydra.description)).Object("description"))
                .Filter(exb => exb.Variable("description") == expectedDescription)
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Theory]
        [InlineData(Vocab.Hydra.Collection)]
        [InlineData(Vocab.Hydra.Resource)]
        public async Task Should_include_hydra_base_types_as_supported_classes(string expectedType)
        {
            // when
            var documentation = await this.GetDocumentationGraph();

            // then
            var query = QueryBuilder.Ask()
                .Where(tpb => tpb.Subject("doc").PredicateUri(new Uri(Vocab.Hydra.supportedClass)).Object(new Uri(expectedType)))
                .BuildQuery();
            documentation.Should().MatchAsk(query);
        }

        [Fact]
        public async Task Should_serve_API_doc_with_correct_Id()
        {
            // when
            var response = await this.browser.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api")
            {
                Headers =
                {
                    { "Accept", "application/json" }
                }
            });

            // then
            var asString = await response.Content.ReadAsStringAsync();
            dynamic apiDoc = JsonConvert.DeserializeObject(asString);

            ((string)apiDoc.id).Should().Be(ExpectedApiDocPath);
        }

        [Fact]
        public async Task Should_not_contain_duplicate_operations()
        {
            var serialize = new JsonLD.Entities.EntitySerializer().Serialize(new Class(new Uri("http://ex.com/issue")));

            // when
            var documentation = await this.GetDocumentationGraph();

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
            var response = await this.browser.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api")
            {
                Headers =
                {
                    { "Accept", "text/turtle" }
                }
            });

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            return (await response.Content.ReadAsStreamAsync()).AsRdf();
        }
    }
}