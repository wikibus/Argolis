using FluentAssertions;
using Hydra.DocumentationDiscovery;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultSupportedPropertyMetaProviderTests
    {
        private readonly DefaultSupportedPropertyMetaProvider _metaProvider;

        public DefaultSupportedPropertyMetaProviderTests()
        {
            _metaProvider = new DefaultSupportedPropertyMetaProvider();
        }

        [Fact]
        public void Should_get_title_equal_to_property_name()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue).GetProperty("DateCreated"));

            // then
            meta.Title.Should().Be("DateCreated");
        }
    }
}