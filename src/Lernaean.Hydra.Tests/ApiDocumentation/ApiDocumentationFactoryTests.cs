using System;
using System.Collections.Generic;
using FakeItEasy;
using FluentAssertions;
using Hydra;
using Hydra.Discovery;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedProperties;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class ApiDocumentationFactoryTests
    {
        private readonly ApiDocumentationFactory _factory;
        private readonly IDocumentedTypeSelector _documentedTypeSource;

        public ApiDocumentationFactoryTests()
        {
            _documentedTypeSource = A.Fake<IDocumentedTypeSelector>();
            _factory = new ApiDocumentationFactory(
                A.Fake<IHydraDocumentationSettings>(),
                new[] { _documentedTypeSource },
                A.Fake<IRdfTypeProviderPolicy>(),
                A.Fake<ISupportedPropertySelectionPolicy>(),
                A.Fake<ISupportedPropertyFactory>(),
                A.Fake<ISupportedClassMetaProvider>());
        }

        [Fact]
        public void Should_only_include_each_type_once()
        {
            // given
            A.CallTo(() => _documentedTypeSource.FindTypes()).Returns(new List<Type>
            {
                typeof (Issue),
                typeof (Issue)
            });

            // when
            var apiDocumentation = _factory.Create();

            // then
            apiDocumentation.SupportedClasses.Should().HaveCount(1);
        }
    }
}