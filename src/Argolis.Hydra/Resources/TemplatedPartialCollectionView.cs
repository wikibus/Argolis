using System;
using System.Collections.Generic;
using JsonLD.Entities;
using NullGuard;

namespace Argolis.Hydra.Resources
{
    /// <summary>
    /// A hydra:PartialCollectionView constructed from a URI Template
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class TemplatedPartialCollectionView : PartialCollectionView
    {
        private readonly UriTemplate.Core.UriTemplate template;
        private readonly string pageVariable;
        private readonly IDictionary<string, object> templateParams;
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
        /// <param name="templateParams">additional template parameters</param>
        public TemplatedPartialCollectionView(
            UriTemplate.Core.UriTemplate template,
            string pageVariable,
            long totalItems,
            int page,
            int pageSize,
            IDictionary<string, object> templateParams = null)
        {
            this.template = template;
            this.pageVariable = pageVariable;
            this.templateParams = templateParams ?? new Dictionary<string, object>();
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

            this.templateParams[this.pageVariable] = actualPage;
            return this.template.BindByName(this.templateParams);
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
