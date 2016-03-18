using FluentAssertions;
using Hydra.DocumentationDiscovery;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultSupportedClassMetaProviderTests
    {
        private readonly DefaultSupportedClassMetaProvider _metaProvider;

        public DefaultSupportedClassMetaProviderTests()
        {
            _metaProvider = new DefaultSupportedClassMetaProvider();
        }

        [Fact]
        public void Should_provide_some_default_description()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue));

            // then
            meta.Description.Should().NotBeNullOrWhiteSpace();
        }
    }
}