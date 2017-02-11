using System.ComponentModel;
using System.Reflection;
using Hydra.Annotations;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;
using Vocab;

namespace Hydra.Resources
{
    /// <summary>
    /// Hydra collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    [SupportedClass(Hydra.Collection)]
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
            get { return Hydra.Collection; }
        }

        /// <summary>
        /// Gets the JSON-LD context.
        /// </summary>
        protected static JToken Context
        {
            get
            {
                var collectionContext = new JObject(
                    "hydra".IsPrefixOf(Hydra.BaseUri),
                    "member".IsProperty(Hydra.member).Container().Set(),
                    "totalItems".IsProperty(Hydra.totalItems));

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
