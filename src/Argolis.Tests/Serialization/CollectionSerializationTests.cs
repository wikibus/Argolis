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
            Assert.Equal(((JArray)jsonLd[Vocab.Hydra.member]).Count, 5);
            Assert.Equal((int)jsonLd[Vocab.Hydra.totalItems], 5);
            Assert.Equal((string)jsonLd["@id"], "http://example.org/collection");
        }
    }
}