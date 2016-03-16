using System;
using FluentAssertions;
using Hydra.Nancy;
using Nancy.Testing;
using TestNancyApp;
using VDS.RDF.Query.Builder;
using VDS.RDF.Query.Expressions;
using Vocab;
using Xunit;
using HCore = Hydra.Hydra;

namespace Lernaean.Hydra.Tests
{
    public class IntegrationTests
    {
        private readonly Browser _browser;

        public IntegrationTests()
        {
            _browser = new Browser(configurator =>
            {
                configurator.Assembly("TestNancyApp");
                configurator.Assembly("Lernaean.Hydra");
                configurator.Assembly("Lernaean.Hydra.Nancy");
                configurator.Dependency<IHydraDocumentationSettings>(typeof (HydraDocumentationSettings));
            },
            context => context.HostName("hydra.guru"));
        }

        [Fact]
        public void Should_include_supported_class_in_documentation_response()
        {
            // when
            var response = _browser.Get("doc", context =>
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