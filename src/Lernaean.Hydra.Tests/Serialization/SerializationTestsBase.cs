using JsonLD.Core;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;

namespace Lernaean.Hydra.Tests.Serialization
{
    public abstract class SerializationTestsBase
    {
        private readonly IEntitySerializer _serializer;

        protected SerializationTestsBase()
        {
            _serializer = new EntitySerializer();
        }

        public IEntitySerializer Serializer
        {
            get { return _serializer; }
        }

        protected JObject Serialize(object obj)
        {
            var jObject = _serializer.Serialize(obj);
            return JsonLdProcessor.Compact(jObject, new JObject(), new JsonLdOptions());
        }
    }
}