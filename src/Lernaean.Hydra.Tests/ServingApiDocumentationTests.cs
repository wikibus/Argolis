using System;
using FakeItEasy;
using FluentAssertions;
using Hydra;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedOperations;
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

        private readonly Browser browser;

        public ServingApiDocumentationTests()
        {
            var rdfTypeProviderPolicy = A.Fake<IRdfTypeProviderPolicy>();
            A.CallTo(() => rdfTypeProviderPolicy.Create(A<Type>._))
                .ReturnsLazily(call => new Uri("http://example.com/class"));

            this.browser = new Browser(
                configurator =>
                {
                    configurator.Module<TestModule>();
                    configurator.Module<HydraApiDocumentationModule>();
                    configurator.Dependency<IHydraDocumentationSettings>(new TestSettings());
                    configurator.Dependency(rdfTypeProviderPolicy);
                    configurator.Dependency(A.Fake<ISupportedPropertyFactory>());
                    configurator.Dependency(A.Fake<ISupportedClassMetaProvider>());
                    configurator.Dependency(A.Fake<ISupportedOperationFactory>());
                    configurator.ApplicationStartupTask<HydraDocumentationStartup>();
                },
                context => context.HostName("hydra.guru"));
        }

        [Fact]
        public async void Should_append_api_doc_header_to_GET_query()
        {
            // when
            var response = await this.browser.Post("test");

            // then
            response.Headers.Should().ContainKey("Link");
            response.Headers["Link"].Should().Be(ExpectedLinkHeader);
        }

        [Fact]
        public async void Should_append_api_doc_header_to_POST_query()
        {
            // when
            var response = await this.browser.Get("test");

            // then
            response.Headers.Should().ContainKey("Link");
            response.Headers["Link"].Should().Be(ExpectedLinkHeader);
        }

        private class TestModule : NancyModule
        {
            public TestModule()
            {
                this.Get("test", _ => "GET test");
                this.Post("test", _ => "POST test");
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