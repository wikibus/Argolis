using FakeItEasy;
using FluentAssertions;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json.Linq;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultPropertyIdPolicyTests
    {
        private readonly DefaultPropertyIdPolicy _policy;
        private readonly IContextProvider _contextProvider;

        public DefaultPropertyIdPolicyTests()
        {
            _contextProvider = A.Fake<IContextProvider>();
            _policy = new DefaultPropertyIdPolicy(_contextProvider);
        }

        [Fact]
        public void Should_use_actual_and_not_declaring_class()
        {
            // given
            const string expectedProperty = "http://example.org/o#Issue/dateCreated";
            A.CallTo(() => _contextProvider.GetContext(typeof (Issue)))
                .Returns(new JObject
                {
                    "dateCreated".IsProperty(expectedProperty)
                });

            // when
            var propertyId = _policy.GetPropertyId(typeof(Issue).GetProperty("DateCreated"));

            // then
            propertyId.Should().Be(expectedProperty);
        }

        [Fact]
        public void Should_use_property_mapped_in_jsonld_ontext()
        {
            // given
            A.CallTo(() => _contextProvider.GetContext(typeof (Issue)))
                .Returns(new JObject
                {
                    "name".IsProperty(Foaf.givenName)
                });

            // when
            var propertyId = _policy.GetPropertyId(typeof(User).GetProperty("Name"));

            // then
            propertyId.Should().Be(Foaf.givenName);
        }
    }
}