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
    }
}