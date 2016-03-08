using System;
using System.Collections.Generic;
using NullGuard;
using TunnelVisionLabs.Net;

namespace Hydra.Resources
{
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class TemplatedPartialCollectionView : PartialCollectionView
    {
        private readonly UriTemplate _template;
        private readonly string _pageVariable;
        private readonly int _totalPages;

        public TemplatedPartialCollectionView(UriTemplate template, string pageVariable, long totalItems, int page, int pageSize)
        {
            _template = template;
            _pageVariable = pageVariable;
            _totalPages = (int)(totalItems / pageSize) + 1;

            Id = BindPageUri(page);
            Next = BindPageUri(page + 1);
            Previous = BindPageUri(page - 1);
            Last = BindPageUri((int)Math.Ceiling((double)totalItems / pageSize));
            First = BindPageUri(1);
        }

        private Uri BindPageUri(int page)
        {
            int? actualPage = page;

            if (actualPage == 1)
            {
                actualPage = null;
            }

            if (actualPage < 1 || actualPage > _totalPages)
            {
                return null;
            }

            return _template.BindByName(new Dictionary<string, int?>
            {
                { _pageVariable, actualPage }
            });
        }
    }
}