using System;
using Argolis.Hydra.Core;
using Xunit;

namespace Argolis.Tests.Serialization
{
    public class ClassSerializationTests : SerializationTestsBase
    {
        [Fact]
        public void Should_serialize_hydraClass_title()
        {
            // given
            var collection = new Class(new Uri("http://example.api/Class")) { Title = "Some class" };

            // when
            dynamic jsonLd = this.Serialize(collection);

            // then
            Assert.Equal("Some class", jsonLd[Vocab.Hydra.title].ToString());
        }
    }
}