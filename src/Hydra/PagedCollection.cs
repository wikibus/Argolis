using System;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra
{
    /// <summary>
    /// Hydra paged collection
    /// </summary>
    /// <typeparam name="T">collection element type</typeparam>
    [Class(Hydra.PagedCollection)]
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
