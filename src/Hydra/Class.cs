using System.Collections.Generic;

namespace Hydra
{
    /// <summary>
    /// A Hydra class
    /// </summary>
    [NullGuard(ValidationFlags.ReturnValues)]
    public class Class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Class"/> class.
        /// </summary>
        /// <param name="type">The class URI.</param>
        public Class(string type)
        {
            Type = type;
        }

        /// <summary>
        /// Gets the class URI.
        /// </summary>
        [JsonProperty("@type")]
        public string Type { get; private set; }

        /// <summary>
        /// Gets or sets the supported operations.
        /// </summary>
        [JsonProperty("supportedOperation")]
        public IEnumerable<Operation> SupportedOperations { get; set; }

        /// <summary>
        /// Gets or sets the supported properties.
        /// </summary>
        [JsonProperty("supportedProperty")]
        public IEnumerable<Property> SupportedProperties { get; set; }
    }
}
