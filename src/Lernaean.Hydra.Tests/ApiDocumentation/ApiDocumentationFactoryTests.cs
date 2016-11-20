using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Hydra;
using Hydra.Discovery;
using Hydra.Discovery.SupportedClasses;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class ApiDocumentationFactoryTests
    {
        private readonly ApiDocumentationFactory factory;
        private readonly IDocumentedTypeSelector documentedTypeSource;

        public ApiDocumentationFactoryTests()
        {
            this.documentedTypeSource = A.Fake<IDocumentedTypeSelector>();
            this.factory = new ApiDocumentationFactory(
                A.Fake<IHydraDocumentationSettings>(),
                new[] { this.documentedTypeSource },
                A.Fake<IRdfTypeProviderPolicy>(),
                A.Fake<ISupportedClassFactory>());
        }

        [Fact]
        public void Should_only_include_each_type_once()
        {
            // given
            A.CallTo(() => this.documentedTypeSource.FindTypes()).Returns(new List<Type>
            {
                typeof(Issue),
                typeof(Issue)
            });

            // when
            var apiDocumentation = this.factory.Create();

            // then
            apiDocumentation.SupportedClasses.Should().HaveCount(1);
        }
    }
}