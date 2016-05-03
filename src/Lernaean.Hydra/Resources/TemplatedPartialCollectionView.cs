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
        private readonly UriTemplate _template;
        private readonly string _pageVariable;
        private readonly int _totalPages;

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
            _template = template;
            _pageVariable = pageVariable;
            _totalPages = (int)(totalItems / pageSize) + 1;

            Id = BindPageUri(page);
            Next = BindPageRef(page + 1);
            Previous = BindPageRef(page - 1);
            Last = BindPageRef((int)Math.Ceiling((double)totalItems / pageSize));
            First = BindPageRef(1);
        }

        private Uri BindPageUri(int page)
        {
            int? actualPage = page;

            if (actualPage < 1 || actualPage > _totalPages)
            {
                return null;
            }

            return _template.BindByName(new Dictionary<string, int?>
            {
                { _pageVariable, actualPage }
            });
        }

        private IriRef? BindPageRef(int page)
        {
            var bindPageUri = BindPageUri(page);
            if (bindPageUri == null)
            {
                return null;
            }

            return (IriRef?)bindPageUri;
        }
    }
}
