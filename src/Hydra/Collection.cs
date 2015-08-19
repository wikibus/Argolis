using System;
using System.Reflection;
using JetBrains.Annotations;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;

namespace Hydra
{
    /// <summary>
    /// Hydra collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Collection<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonProperty("member")]
        public T[] Members { get; set; }

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
                    "member".IsProperty(Hydra.BaseUri + "member").Container().Set());

                var memberContext = typeof(T).GetProperty("Context", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null);

                return new JArray(Hydra.Context, collectionContext, memberContext);
            }
        }
    }
}
