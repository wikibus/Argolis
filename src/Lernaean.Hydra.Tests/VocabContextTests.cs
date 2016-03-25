using System;
using FluentAssertions;
using Hydra.Serialization;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests
{
    public class VocabContextTests
    {
        [Fact]
        public void When_created_should_map_property_using_namespace_prefix()
        {
            // given
            var context = new VocabContext<Issue>("http://example.api/o#");

            // then
            context["titel"].ToString().Should().Be("http://example.api/o#titel");
        }
    }
}
