using System.ComponentModel;
using Argolis.Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;

namespace Argolis.Hydra.Resources
{
    /// <summary>
    /// Hydra partial collection view
    /// </summary>
    [Description("A sliced view of a collection (ie. a page)")]
    [SupportedClass(Vocab.Hydra.PartialCollectionView)]
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class PartialCollectionView : Resource, IView
    {
        /// <summary>
        /// Gets or sets the next page URI.
        /// </summary>
        [Description("The next page of this collection")]
        [Range(Vocab.Hydra.Collection)]
        [ReadOnly(true)]
        public IriRef? Next { get; set; }

        /// <summary>
        /// Gets or sets the last page URI.
        /// </summary>
        [Description("The previous page of this collection")]
        [Range(Vocab.Hydra.Collection)]
        [ReadOnly(true)]
        public IriRef? Previous { get; set; }

        /// <summary>
        /// Gets or sets the last page URI.
        /// </summary>
        [Description("The last page of this collection")]
        [Range(Vocab.Hydra.Collection)]
        [ReadOnly(true)]
        public IriRef? Last { get; set; }

        /// <summary>
        /// Gets or sets the first page URI.
        /// </summary>
        [Description("The first page of this collection")]
        [Range(Vocab.Hydra.Collection)]
        [ReadOnly(true)]
        public IriRef? First { get; set; }

        /// <summary>
        /// Gets the RDF type.
        /// </summary>
        [JsonProperty, UsedImplicitly]
        public string Type
        {
            get { return Vocab.Hydra.PartialCollectionView; }
        }

        /// <summary>
        /// Gets the JSON-LD context.
        /// </summary>
        [UsedImplicitly]
        protected static JObject Context
        {
            get
            {
                return new VocabContext<PartialCollectionView>(Vocab.Hydra.BaseUri);
            }
        }
    }
}
