using Argolis.Hydra.Core;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests.Core
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