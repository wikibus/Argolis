using Argolis.Hydra.Core;
using JsonLD.Entities;
using Xunit;

namespace Argolis.Tests.Serialization
{
    public class IriTemplateMappingTests : SerializationTestsBase
    {
        [Fact]
        public void Serializes_Correct_Type()
        {
            // given
            var iriTemplate = new IriTemplateMapping();

            // when
            var jsonLd = this.Serializer.Serialize(iriTemplate);

            // then
            Assert.Equal(Vocab.Hydra.IriTemplateMapping, jsonLd[JsonLdKeywords.Type].ToString());
        }
    }
}