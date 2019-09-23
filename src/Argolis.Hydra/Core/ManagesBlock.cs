using System;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;

namespace Argolis.Hydra.Core
{
    /// <summary>
    /// Represents a "manages block"
    /// </summary>
    [NullGuard(ValidationFlags.ReturnValues)]
    public class ManagesBlock
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagesBlock"/> class.
        /// </summary>
        public ManagesBlock(IriRef? subject = null, IriRef? property = null, IriRef? obj = null)
        {
            var providedArgumentsCount = (subject.HasValue ? 1 : 0)
                                         + (property.HasValue ? 1 : 0)
                                         + (obj.HasValue ? 1 : 0);

            if (providedArgumentsCount != 2)
            {
                throw new InvalidOperationException("Manage block must be constructed with exactly two parameters");
            }

            this.Subject = subject;
            this.Property = property;
            this.Object = obj;
        }

        /// <summary>
        /// Gets or sets the manages block's subject
        /// </summary>
        [JsonProperty("subject")]
        public IriRef? Subject { get; set; }

        /// <summary>
        /// Gets or sets the manages block's property
        /// </summary>
        [JsonProperty("property")]
        public IriRef? Property { get; set; }

        /// <summary>
        /// Gets or sets the manages block's object
        /// </summary>
        [JsonProperty("object")]
        public IriRef? Object { get; set; }
    }
}