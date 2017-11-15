using FluentAssertions;
using Hydra.Core;
using Xunit;

namespace Lernaean.Hydra.Tests.Core
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