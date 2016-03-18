using System;
using Hydra.Core;
using Xunit;

namespace Lernaean.Hydra.Tests.Serialization
{
    public class ClassSerializationTests : SerializationTestsBase
    {
        [Fact]
        public void Should_serialize_hydraClass_title()
        {
            // given
            var collection = new Class(new Uri("http://example.api/Class")) { Title = "Some class" };

            // when
            dynamic jsonLd = Serialize(collection);

            // then
            Assert.Equal("Some class", jsonLd[global::Hydra.Hydra.title].ToString());
        }
    }
}