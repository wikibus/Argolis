using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra.Resources
{
    /// <summary>
    /// Hydra partial collection view
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class PartialCollectionView : IView
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the next page URI.
        /// </summary>
        public Uri Next { get; set; }

        /// <summary>
        /// Gets or sets the last page URI.
        /// </summary>
        public Uri Previous { get; set; }

        /// <summary>
        /// Gets or sets the last page URI.
        /// </summary>
        public Uri Last { get; set; }

        /// <summary>
        /// Gets or sets the first page URI.
        /// </summary>
        public Uri First { get; set; }

        /// <summary>
        /// Gets the RDF type.
        /// </summary>
        [JsonProperty, UsedImplicitly]
        public string Type
        {
            get { return Hydra.PartialCollectionView; }
        }
    }
}
