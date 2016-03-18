using FakeItEasy;
using FluentAssertions;
using Hydra;
using Hydra.DocumentationDiscovery;
using Hydra.Nancy;
using JsonLD.Entities;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Lernaean.Hydra.Tests
{
    public class ServingApiDocumentationTests
    {
        private const string ExpectedApiDocPath = "http://hydra.guru/uber/documentation/path";
        private const string ExpectedLinkHeader = "<" + ExpectedApiDocPath + ">; rel=\"" + global::Hydra.Hydra.apiDocumentation + "\"";

        private readonly Browser _browser;

        public ServingApiDocumentationTests()
        {
            _browser = new Browser(configurator =>
            {
                configurator.Module<TestModule>();
                configurator.Module<HydraApiDocumentationModule>();
                configurator.Dependency<IHydraDocumentationSettings>(new TestSettings());
                configurator.Dependency(A.Fake<IRdfTypeProviderPolicy>());
                configurator.Dependency(A.Fake<ISupportedPropertyFactory>());
                configurator.Dependency(A.Fake<ISupportedClassMetaProvider>());
                configurator.ApplicationStartupTask<HydraDocumentationStartup>();
            }, 
            context => context.HostName("hydra.guru"));
        }

        [Fact]
        public void Should_append_api_doc_header_to_GET_query()
        {
            // when
            var response = _browser.Post("test");

            // then
            response.Headers.Should().ContainKey("Link");
            response.Headers["Link"].Should().Be(ExpectedLinkHeader);
        }

        [Fact]
        public void Should_append_api_doc_header_to_POST_query()
        {
            // when
            var response = _browser.Get("test");

            // then
            response.Headers.Should().ContainKey("Link");
            response.Headers["Link"].Should().Be(ExpectedLinkHeader);
        }

        [Fact]
        public void Should_serve_API_doc_with_correct_Id()
        {
            // when
            var response = _browser.Get("/uber/documentation/path", context => context.Accept(new MediaRange("application/json")));

            // then
            var asString = response.Body.AsString();
            dynamic apiDoc = JsonConvert.DeserializeObject(asString);

            ((string)apiDoc.id).Should().Be(ExpectedApiDocPath);
        }

        private class TestModule : NancyModule
        {
            public TestModule()
            {
                Get["test"] = _ => "GET test";
                Post["test"] = _ => "POST test";
            }
        }

        private class TestSettings : IHydraDocumentationSettings
        {
            public string DocumentationPath
            {
                get { return "uber/documentation/path"; }
            }

            public IriRef EntryPoint { get; }
        }
    }
}