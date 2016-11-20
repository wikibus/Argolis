using System;
using System.Collections.Generic;
using JsonLD.Entities;
using NullGuard;
using TunnelVisionLabs.Net;

namespace Hydra.Resources
{
    /// <summary>
    /// A hydra:PartialCollectionView constructed from a URI Template
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class TemplatedPartialCollectionView : PartialCollectionView
    {
        private readonly UriTemplate template;
        private readonly string pageVariable;
        private readonly int totalPages;

        /// <summary>
        /// Initializes a new instance of the <see cref="TemplatedPartialCollectionView"/> class
        /// </summary>
        /// <param name="template">the URI Template</param>
        /// <param name="pageVariable">the page variable</param>
        /// <param name="totalItems">total items in collection</param>
        /// <param name="page">
        /// current page index
        /// <remarks>1-based</remarks>
        /// </param>
        /// <param name="pageSize">page size, used to calculate last page index</param>
        public TemplatedPartialCollectionView(UriTemplate template, string pageVariable, long totalItems, int page, int pageSize)
        {
            this.template = template;
            this.pageVariable = pageVariable;
            this.totalPages = (int)(totalItems / pageSize) + 1;

            this.Id = this.BindPageUri(page);
            this.Next = this.BindPageRef(page + 1);
            this.Previous = this.BindPageRef(page - 1);
            this.Last = this.BindPageRef((int)Math.Ceiling((double)totalItems / pageSize));
            this.First = this.BindPageRef(1);
        }

        private Uri BindPageUri(int page)
        {
            int? actualPage = page;
            if (actualPage < 1 || actualPage > this.totalPages)
            {
                return null;
            }

            return this.template.BindByName(new Dictionary<string, object>
            {
                { this.pageVariable, actualPage }
            });
        }

        private IriRef? BindPageRef(int page)
        {
            var bindPageUri = this.BindPageUri(page);
            if (bindPageUri == null)
            {
                return null;
            }

            return (IriRef?)bindPageUri;
        }
    }
}
