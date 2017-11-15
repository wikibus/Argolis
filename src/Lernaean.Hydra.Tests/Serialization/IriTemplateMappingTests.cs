using Hydra.Resources;
using JsonLD.Entities;
using Xunit;

namespace Lernaean.Hydra.Tests.Serialization
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
            Assert.Equal(global::Hydra.Hydra.IriTemplateMapping, jsonLd[JsonLdKeywords.Type].ToString());
        }
    }
}