using Newtonsoft.Json;
using NullGuard;
using Vocab;

namespace Hydra.Core
{
    /// <summary>
    /// Base class for elements of Hydra Core vocabulary
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public abstract class Resource
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty(Rdfs.label)]
        public string Label { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty(Rdfs.comment)]
        public string Description { [return: AllowNull] get; set; }
    }
}