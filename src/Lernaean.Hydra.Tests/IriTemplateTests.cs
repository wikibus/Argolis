using FluentAssertions;
using Hydra.Resources;
using Xunit;

namespace Lernaean.Hydra.Tests
{
    public class IriTemplateTests
    {
        [Fact]
        public void VariableRepresentation_Should_be_BasicRepresentation_by_default()
        {
            // given
            var iriTemplate = new IriTemplate();

            // then
            iriTemplate.VariableRepresentation.Should().Be(VariableRepresentation.BasicRepresentation);
        }
    }
}