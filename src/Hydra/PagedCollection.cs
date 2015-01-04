using System;
using System.Reflection;
using JetBrains.Annotations;
using JsonLD.Entities;
using JsonLD.Entities.Context;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NullGuard;

namespace Hydra
{
    /// <summary>
    /// Hydra paged collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class PagedCollection<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }

        /// <summary>
        /// Gets or sets the total items.
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int ItemsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        [JsonIgnore]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets the next page URI.
        /// </summary>
        public Uri NextPage
        {
            get
            {
                if (IsLastPage)
                {
                    return null;
                }

                var builder = new UriBuilder(Id);
                builder.Query = "page=" + (CurrentPage + 1);
                return builder.Uri;
            }
        }

        /// <summary>
        /// Gets the last page URI.
        /// </summary>
        public Uri PreviousPage
        {
            get
            {
                if (CurrentPage == 1)
                {
                    return null;
                }

                if (IsLastPage)
                {
                    return LastPage;
                }

                return GetUriForPage(CurrentPage - 1);
            }
        }

        /// <summary>
        /// Gets the last page URI.
        /// </summary>
        public Uri LastPage
        {
            get
            {
                if (TotalItems == 0)
                {
                    return null;
                }

                return GetUriForPage(LastPageIndex);
            }
        }

        /// <summary>
        /// Gets or sets the members.
        /// </summary>
        [JsonProperty("member")]
        public T[] Members { get; set; }

        [UsedImplicitly]
        private static JToken Context
        {
            get
            {
                var collectionContext = new JObject(
                    "hydra".IsPrefixOf(Hydra.BaseUri),
                    "xsd".IsPrefixOf("http://www.w3.org/2001/XMLSchema#"),
                    "member".IsProperty(Hydra.BaseUri + "member").Container().Set(),
                    "totalItems".IsProperty(Hydra.totalItems).Type().Is("xsd:integer"),
                    "nextPage".IsProperty(Hydra.BaseUri + "nextPage").Type().Id(),
                    "previousPage".IsProperty(Hydra.BaseUri + "previousPage").Type().Id());

                var memberContext = typeof(T).GetProperty("Context", BindingFlags.Static | BindingFlags.NonPublic).GetValue(null, null);

                return new JArray(Hydra.Context, collectionContext, memberContext);
            }
        }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Hydra.PagedCollection; }
        }

        private bool IsLastPage
        {
            get { return CurrentPage >= LastPageIndex; }
        }

        private int LastPageIndex
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        private Uri GetUriForPage(int i)
        {
            var builder = new UriBuilder(Id);
            builder.Query = "page=" + i;

            return builder.Uri;
        }
    }
}
