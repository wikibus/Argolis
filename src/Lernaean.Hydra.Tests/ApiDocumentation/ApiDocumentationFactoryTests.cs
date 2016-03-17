using System;
using System.Collections.Generic;
using FakeItEasy;
using FakeItEasy.Core;
using FluentAssertions;
using Hydra;
using Hydra.DocumentationDiscovery;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class ApiDocumentationFactoryTests
    {
        private readonly ApiDocumentationFactory _factory;
        private readonly IHydraDocumentationSettings _hydraDocumentationSettings;

        public ApiDocumentationFactoryTests()
        {
            _hydraDocumentationSettings = A.Fake<IHydraDocumentationSettings>();
            _factory = new ApiDocumentationFactory(
                _hydraDocumentationSettings,
                A.Fake<IRdfTypeProviderPolicy>(),
                A.Fake<ISupportedPropertyFactory>());
        }

        [Fact]
        public void Should_only_include_each_type_once()
        {
            // given
            A.CallTo(() => _hydraDocumentationSettings.Sources).ReturnsLazily(SourceWithDuplicateTypes);

            // when
            var apiDocumentation = _factory.Create();

            // then
            apiDocumentation.SupportedClasses.Should().HaveCount(1);
        }

        private IEnumerable<IDocumentedTypeSelector> SourceWithDuplicateTypes(IFakeObjectCall arg)
        {
            var sourceWithDuplicateTypes = A.Fake<IDocumentedTypeSelector>();
            A.CallTo(() => sourceWithDuplicateTypes.FindTypes()).Returns(new List<Type>
            {
                typeof (Issue),
                typeof (Issue)
            });

            yield return sourceWithDuplicateTypes;
        }
    }
}