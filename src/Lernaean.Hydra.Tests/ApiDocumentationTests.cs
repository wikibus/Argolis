using System;
using FluentAssertions;
using Hydra.Core;
using Hydra.Nancy;
using Nancy;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Lernaean.Hydra.Tests
{
    public class ApiDocumentationTests
    {
        private const string ExpectedApiDocPath = "http://hydra.guru/uber/documentation/path";
        private const string ExpectedLinkHeader = "<" + ExpectedApiDocPath + ">; rel=\"" + global::Hydra.Hydra.apiDocumentation + "\"";

        private readonly Browser _browser;

        public ApiDocumentationTests()
        {
            _browser = new Browser(configurator =>
            {
                configurator.Module<TestModule>();
                configurator.Module<HydraApiDocumentationModule>();
                configurator.Dependency<IHydraDocumentationSettings>(new TestSettings());
                configurator.Dependency<IApiDocumentationFactory>(new NullFactory());
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
            dynamic apiDoc = JsonConvert.DeserializeObject(response.Body.AsString());

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
        }

        private class NullFactory : IApiDocumentationFactory
        {
            public ApiDocumentation CreateApiDocumentation()
            {
                return new TestApiDocumentation(new Uri("http://example.com/start"));
            }
        }

        private class TestApiDocumentation : ApiDocumentation
        {
            public TestApiDocumentation(Uri entrypoint) : base(entrypoint)
            {
            }

            protected override JToken GetLocalContext()
            {
                return global::Hydra.Hydra.Context;
            }
        }
    }
}