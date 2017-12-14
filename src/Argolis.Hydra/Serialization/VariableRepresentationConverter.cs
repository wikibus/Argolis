using System;
using System.Diagnostics.CodeAnalysis;
using Argolis.Hydra.Core;
using JsonLD.Entities;
using Newtonsoft.Json;

namespace Argolis.Hydra.Serialization
{
    /// <inheritdoc />
    internal class VariableRepresentationConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is VariableRepresentation variableRepresentation)
            {
                var uri = Vocab.Hydra.BaseUri + variableRepresentation;

                writer.WriteStartObject();
                writer.WritePropertyName(JsonLdKeywords.Id);
                writer.WriteValue(uri);
                writer.WriteEndObject();
            }
            else
            {
                writer.WriteNull();
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(VariableRepresentation);
        }
    }
}