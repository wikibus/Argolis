using Hydra.Serialization;
using Newtonsoft.Json;

namespace Hydra.Resources
{
    /// <summary>
    /// A template syntax only details how to fill out simple string values, but not how to derive such string
    /// values from typed values, language-tagged strings, or IRIs. Hydra addresses this by specifying
    /// how such values are to be serialized as strings
    /// </summary>
    [JsonConverter(typeof(VariableRepresentationConverter))]
    public enum VariableRepresentation
    {
        /// <summary>
        /// The BasicRepresentation represents values by their lexical form.
        /// It omits type and language information and does not differentiate between IRIs and literals
        /// </summary>
        BasicRepresentation,

        /// <summary>
        /// The ExplicitRepresentation includes type and language information
        /// and differentiates between IRIs and literals by serializing values as follows
        /// </summary>
        ExplicitRepresentation
    }
}
