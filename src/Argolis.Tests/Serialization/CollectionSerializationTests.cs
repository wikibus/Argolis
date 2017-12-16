using System;
using Argolis.Hydra.Resources;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Argolis.Tests.Serialization
{
    public class CollectionSerializationTests : SerializationTestsBase
    {
        [Fact]
        public void Should_serialize_collection_of_ints_without_a_view()
        {
            // given
            var collection = new Collection<int>
            {
                Id = new Uri("http://example.org/collection"),
                Members = new[] { 1, 2, 3, 4, 5 },
                TotalItems = 5
            };

            // when
            dynamic jsonLd = this.Serialize(collection);

            // then
            Assert.Equal(5, ((JArray)jsonLd[Vocab.Hydra.member]).Count);
            Assert.Equal(5, (int)jsonLd[Vocab.Hydra.totalItems]);
            Assert.Equal("http://example.org/collection", (string)jsonLd["@id"]);
        }
    }
}