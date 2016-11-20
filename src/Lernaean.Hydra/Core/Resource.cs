using Newtonsoft.Json;
using NullGuard;

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
        [JsonProperty(Hydra.title)]
        public string Title { [return: AllowNull] get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty(Hydra.description)]
        public string Description { [return: AllowNull] get; set; }
    }
}
