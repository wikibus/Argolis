using System.Linq;
using JsonLD.Core;
using JsonLD.Entities;
using Newtonsoft.Json.Linq;
using NullGuard;

namespace Argolis.Hydra
{
    /// <summary>
    /// Helpers to retrieve terms mapped in the @context
    /// </summary>
    public static class ContextHelpers
    {
        /// <summary>
        /// Gets the mapped term.
        /// </summary>
        /// <param name="context">The JSON-LD @context.</param>
        /// <param name="term">The term name.</param>
        /// <returns>mapped IRI or null if not found</returns>
        [return: AllowNull]
        public static string GetExpandedIri(JToken context, string term)
        {
            if (term == JsonLdKeywords.Context)
            {
                return null;
            }

            var value = new JObject
            {
                { JsonLdKeywords.Context, context },
                { term, "value" }
            };

            var expanded = JsonLdProcessor.Expand(value, new JsonLdOptions());
            if (expanded.Any())
            {
                return ((JProperty)expanded[0].First).Name;
            }

            return null;
        }
    }
}
