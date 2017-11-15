using FluentAssertions;
using Hydra.Resources;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class IriTemplateTests
    {
        [Fact]
        public void Should_use_BasicRepresentation_by_default()
        {
            // given
            var iriTemplate = new IriTemplate();

            // then
            iriTemplate.VariableRepresentation.Should().Be(VariableRepresentation.BasicRepresentation);
        }
    }
}
