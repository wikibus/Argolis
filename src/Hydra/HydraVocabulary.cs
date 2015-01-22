using Newtonsoft.Json.Linq;

namespace Hydra
{
    /// <summary>
    /// Hydra Vocabulary
    /// </summary>
    public static partial class Hydra
    {
        /// <summary>
        /// URL of default Hydra JSON-LD @context
        /// </summary>
        public static readonly JObject Context = JObject.Parse(Resourcer.Resource.AsString("context.jsonld"));
    }
}
