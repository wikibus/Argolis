using JsonLD.Core;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;

namespace Argolis.Tests.Serialization
{
    public abstract class SerializationTestsBase
    {
        private readonly IEntitySerializer serializer;

        protected SerializationTestsBase()
        {
            this.serializer = new EntitySerializer();
        }

        public IEntitySerializer Serializer
        {
            get { return this.serializer; }
        }

        protected JObject Serialize(object obj)
        {
            var jObject = this.serializer.Serialize(obj);
            return JsonLdProcessor.Compact(jObject, new JObject(), new JsonLdOptions());
        }
    }
}