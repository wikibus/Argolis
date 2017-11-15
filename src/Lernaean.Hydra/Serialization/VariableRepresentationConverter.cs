using System;
using Hydra.Core;
using Hydra.Resources;
using JsonLD.Entities;
using Newtonsoft.Json;

namespace Hydra.Serialization
{
    /// <inheritdoc />
    internal class VariableRepresentationConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is VariableRepresentation variableRepresentation)
            {
                var uri = Hydra.BaseUri + variableRepresentation;

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
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(VariableRepresentation);
        }
    }
}