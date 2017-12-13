using Hydra.Core;
using JsonLD.Entities;
using Xunit;

namespace Lernaean.Hydra.Tests.Serialization
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
            Assert.Equal(global::Hydra.Hydra.BaseUri + vr, jsonLd[global::Hydra.Hydra.variableRepresentation]["@id"].ToString());
        }

        [Fact]
        public void Serializes_Correct_Type()
        {
            // given
            var iriTemplate = new IriTemplate();

            // when
            var jsonLd = this.Serializer.Serialize(iriTemplate);

            // then
            Assert.Equal(global::Hydra.Hydra.IriTemplate, jsonLd[JsonLdKeywords.Type].ToString());
        }
    }
}