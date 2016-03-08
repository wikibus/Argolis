using System;
using FluentAssertions;
using Hydra;
using Hydra.Resources;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using Resourcer;
using Xunit;

namespace Lernean.Hydra.Tests.Serialization
{
    public class CollectionSerializationTests
    {
        private readonly IEntitySerializer _serializer;

        public CollectionSerializationTests()
        {
            _serializer = new EntitySerializer();
        }

        [Fact]
        public void Should_serialize_collection_of_ints_without_a_view()
        {
            // given
            var expected = JObject.Parse(Resource.AsString("Expected.CollectionWithoutViews.jsonld"));
            var collection = new Collection<int>
            {
                Id = new Uri("http://example.org/collection"),
                Members = new[] {1, 2, 3, 4, 5},
                TotalItems = 5
            };

            // when
            var jsonLd = _serializer.Serialize(collection);

            // then
            JToken.DeepEquals(jsonLd, expected).Should().BeTrue();
        }
    }
}