using FluentAssertions;
using Hydra.Discovery.SupportedProperties;
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

        [Fact]
        public void Should_set_writeable_to_true_by_default()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue).GetProperty("Title"));

            // then
            meta.Writeable.Should().BeTrue();
        }

        [Fact]
        public void Should_set_writeable_to_false_if_attribute_is_present()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue).GetProperty("DateCreated"));

            // then
            meta.Writeable.Should().BeFalse();
        }

        [Fact]
        public void Should_set_writeable_to_false_if_property_is_readonly()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue).GetProperty("LikesCount"));

            // then
            meta.Writeable.Should().BeFalse();
        }

        [Fact]
        public void Should_user_DescriptionAttribute_to_set_description()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue).GetProperty("LikesCount"));

            // then
            meta.Description.Should().Be("The number of people who liked this issue");
        }

        [Fact]
        public void Should_provide_some_default_description()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue).GetProperty("DateCreated"));

            // then
            meta.Description.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void Should_provide_some_default_title()
        {
            // when
            var meta = _metaProvider.GetMeta(typeof(Issue).GetProperty("DateCreated"));

            // then
            meta.Title.Should().NotBeNullOrWhiteSpace();
        }
    }
}