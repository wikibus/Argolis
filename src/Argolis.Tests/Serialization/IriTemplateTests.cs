using Argolis.Hydra.Core;
using JsonLD.Entities;
using Xunit;

namespace Argolis.Tests.Serialization
{
    public class IriTemplateTests : SerializationTestsBase
    {
        [Theory]
        [InlineData(VariableRepresentation.BasicRepresentation)]
        [InlineData(VariableRepresentation.ExplicitRepresentation)]
        public void Serializes_VariableRepresentation_as_proper_URIs(VariableRepresentation vr)
        {
            // given
            var iriTemplate = new IriTemplate
            {
                VariableRepresentation = vr
            };

            // when
            var jsonLd = this.Serializer.Serialize(iriTemplate);

            // then
            Assert.Equal(Vocab.Hydra.BaseUri + vr, jsonLd[Vocab.Hydra.variableRepresentation]["@id"].ToString());
        }

        [Fact]
        public void Serializes_Correct_Type()
        {
            // given
            var iriTemplate = new IriTemplate();

            // when
            var jsonLd = this.Serializer.Serialize(iriTemplate);

            // then
            Assert.Equal(Vocab.Hydra.IriTemplate, jsonLd[JsonLdKeywords.Type].ToString());
        }
    }
}