using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Argolis.Hydra.Annotations;
using Argolis.Hydra.Core;
using JetBrains.Annotations;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;

namespace Argolis.Hydra.Resources
{
    /// <summary>
    /// Hydra collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    [SupportedClass(Vocab.Hydra.Collection)]
    [Description("A collection of related resources")]
    public class Collection<T> : Resource, IResourceWithViews
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Collection{T}"/> class
        /// </summary>
        public Collection()
        {
            this.Views = new IView[0];
            this.Members = new T[0];
            this.Manages = new List<ManagesBlock>();

            var memberRdfType = this.GetMemberRdfType();
            if (memberRdfType != null)
            {
                this.Manages.Add(new ManagesBlock(
                    property: (IriRef)Rdf.type,
                    obj: (IriRef)memberRdfType));
            }
        }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonProperty("member")]
        [Description("The members of this collection")]
        [ReadOnly(true)]
        public T[] Members { get; set; }

        /// <summary>
        /// Gets or sets the views
        /// </summary>
        [JsonProperty("view")]
        [Description("The views of this collection")]
        [ReadOnly(true)]
        public IView[] Views { get; set; }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        [Description("The number of members of this collection")]
        [ReadOnly(true)]
        [Range(Xsd.nonNegativeInteger)]
        public long TotalItems { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        [JsonProperty, UsedImplicitly]
        public virtual string Type
        {
            get { return Vocab.Hydra.Collection; }
        }

        /// <summary>
        /// Gets the collection's manages blocks
        /// </summary>
        [JsonProperty("manages")]
        public ICollection<ManagesBlock> Manages { get; private set; }

        /// <summary>
        /// Gets the JSON-LD context.
        /// </summary>
        protected static JToken Context
        {
            get
            {
                var collectionContext = new JObject(
                    "hydra".IsPrefixOf(Vocab.Hydra.BaseUri),
                    "member".IsProperty(Vocab.Hydra.member).Container().Set(),
                    "manages".IsProperty(Vocab.Hydra.manages).Container().Set(),
                    "property".IsProperty(Vocab.Hydra.property),
                    "subject".IsProperty(Vocab.Hydra.subject),
                    "object".IsProperty(Vocab.Hydra.@object),
                    "totalItems".IsProperty(Vocab.Hydra.totalItems));

                var propertyInfo = typeof(T).GetProperty("Context", BindingFlags.Static | BindingFlags.NonPublic);
                if (propertyInfo != null)
                {
                    var memberContext = (JToken)propertyInfo.GetValue(null, null);

                    return Hydra.Context.MergeWith(collectionContext, memberContext);
                }

                return Hydra.Context.MergeWith(collectionContext);
            }
        }
    }
}
