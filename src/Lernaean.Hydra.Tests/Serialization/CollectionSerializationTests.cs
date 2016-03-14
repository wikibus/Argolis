using System;
using FluentAssertions;
using Hydra;
using Hydra.Resources;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Xunit;
using Vocab = Hydra.Hydra;

namespace Lernaean.Hydra.Tests.Serialization
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
                Members = new[] {1, 2, 3, 4, 5},
                TotalItems = 5
            };

            // when
            dynamic jsonLd = Serialize(collection);

            // then
            Assert.Equal(((JArray)jsonLd[Vocab.member]).Count, 5);
            Assert.Equal((int)jsonLd[Vocab.totalItems], 5);
            Assert.Equal((string)jsonLd["@id"], "http://example.org/collection");
        }
    }
}