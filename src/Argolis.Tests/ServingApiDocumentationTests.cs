﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Argolis.Hydra;
using Argolis.Hydra.Discovery;
using Argolis.Hydra.Discovery.SupportedClasses;
using Argolis.Hydra.Discovery.SupportedOperations;
using Argolis.Hydra.Nancy;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using Nancy;
using Nancy.Testing;
using Xunit;

namespace Argolis.Tests
{
    public class ServingApiDocumentationTests
    {
        private const string ExpectedApiDocPath = "http://hydra.guru/uber/documentation/path";
        private const string ExpectedLinkHeader = "<" + ExpectedApiDocPath + ">; rel=\"" + Vocab.Hydra.apiDocumentation + "\"";

        private readonly Browser browser;

        public ServingApiDocumentationTests()
        {
            var rdfTypeProviderPolicy = A.Fake<IRdfTypeProviderPolicy>();
            A.CallTo(() => rdfTypeProviderPolicy.Create(A<Type>._))
                .ReturnsLazily(call => new Uri("http://example.com/class"));

            Debugger.Break();
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
                    configurator.Dependency(A.Fake<IApiDocumentationFactory>());
                    configurator.ApplicationStartupTask<HydraDocumentationStartup>();
                },
                context => context.HostName("hydra.guru"));
        }

        [Fact]
        public async Task Should_append_api_doc_header_to_GET_query()
        {
            // when
            var response = await this.browser.Post("test");

            // then
            response.Headers.Should().ContainKey("Link");
            response.Headers["Link"].Should().Match(ExpectedLinkHeader);
        }

        [Fact]
        public async Task Should_append_api_doc_header_to_POST_query()
        {
            // when
            var response = await this.browser.Get("test");

            // then
            response.Headers.Should().ContainKey("Link");
            response.Headers["Link"].Should().Match(ExpectedLinkHeader);
        }

        [Fact]
        public async Task Should_not_replace_other_links_with_doc_link()
        {
            // when
            var response = await this.browser.Get("has-link");

            // then
            response.Headers.Should().ContainKey("Link");
            response.Headers["Link"].Should().MatchRegex(ExpectedLinkHeader);
            response.Headers["Link"].Should().MatchRegex("<http://test.com/canonical>; rel=\"canonical\"");
        }

        private class TestModule : NancyModule
        {
            public TestModule()
            {
                this.Get("test", _ => "GET test");
                this.Post("test", _ => "POST test");
                this.Get("has-link", _ => new Response
                {
                    Headers = new Dictionary<string, string>
                    {
                        { "Link", "<http://test.com/canonical>; rel=\"canonical\"" }
                    }
                });
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