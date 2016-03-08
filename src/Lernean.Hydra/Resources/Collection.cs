using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra.Resources
{
    /// <summary>
    /// Hydra collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Collection<T> : IResourceWithViews
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonProperty(Hydra.member)]
        public T[] Members { get; set; }
        
        [JsonProperty(Hydra.view)]
        public IView[] Views { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        [JsonProperty, UsedImplicitly]
        public virtual string Type
        {
            get { return Hydra.Collection; }
        }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        [JsonProperty(Hydra.totalItems)]
        public long TotalItems { get; set; }
    }
}
