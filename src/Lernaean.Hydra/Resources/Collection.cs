using System;
using System.Reflection;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public Collection()
        {
            Views = new IView[0];
            Members = new T[0];
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonProperty("member")]
        public T[] Members { get; set; }

        [JsonProperty("view")]
        public IView[] Views { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        [JsonProperty, UsedImplicitly]
        public virtual string Type
        {
            get { return Hydra.Collection; }
        }

        [UsedImplicitly]
        private static JToken Context
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
                    var memberContext = propertyInfo.GetValue(null, null);

                    return new JArray(Hydra.Context, collectionContext, memberContext);
                }

                return new JArray(Hydra.Context, collectionContext);
            }
        }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        public long TotalItems { get; set; }
    }
}
