using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra.Resources
{
    /// <summary>
    /// Hydra parial collection view
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class PartialCollectionView : IView
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets the next page URI.
        /// </summary>
        [JsonProperty(Hydra.next)]
        public Uri Next { get; set; }

        /// <summary>
        /// Gets the last page URI.
        /// </summary>
        [JsonProperty(Hydra.previous)]
        public Uri Previous { get; set; }

        /// <summary>
        /// Gets the last page URI.
        /// </summary>
        [JsonProperty(Hydra.last)]
        public Uri Last { get; set; }

        /// <summary>
        /// Gets the first page URI.
        /// </summary>
        [JsonProperty(Hydra.first)]
        public Uri First { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        [JsonProperty, UsedImplicitly]
        public string Type
        {
            get { return Hydra.PartialCollectionView; }
        }
    }
}
