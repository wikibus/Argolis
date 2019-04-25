using System;
using System.Collections.Generic;
using Argolis.Hydra.Resources;
using FluentAssertions;
using JsonLD.Entities;
using Xunit;

namespace Argolis.Tests.Resources
{
    public class PartialCollectionViewTests
    {
        [Fact]
        public void Should_be_created_with_empty_links()
        {
            // when
            var view = new PartialCollectionView();

            // then
            view.Id.Should().BeNull();
            view.First.Should().BeNull();
            view.Last.Should().BeNull();
            view.Next.Should().BeNull();
            view.Previous.Should().BeNull();
        }

        [Fact]
        public void Should_be_of_correct_type()
        {
            // when
            var view = new PartialCollectionView();

            // then
            view.Type.Should().Be(Vocab.Hydra.PartialCollectionView);
        }

        [Fact]
        public void Should_calculate_page_URIs_given_item_count_and_template()
        {
            // given
            var template = new UriTemplate.Core.UriTemplate("/some/collection{?page}");
            long totalItems = 86;
            int page = 5;
            int pageSize = 10;
            string pageVariable = "page";

            // when
            var view = new TemplatedPartialCollectionView(template, pageVariable, totalItems, page, pageSize);

            // then
            view.Id.Should().Be(new Uri("/some/collection?page=5", UriKind.Relative));
            view.First.Should().Be((IriRef)new Uri("/some/collection?page=1", UriKind.Relative));
            view.Last.Should().Be((IriRef)new Uri("/some/collection?page=9", UriKind.Relative));
            view.Next.Should().Be((IriRef)new Uri("/some/collection?page=6", UriKind.Relative));
            view.Previous.Should().Be((IriRef)new Uri("/some/collection?page=4", UriKind.Relative));
        }

        [Fact]
        public void Should_calculate_page_URIs_given_last_page()
        {
            // given
            var template = new UriTemplate.Core.UriTemplate("/some/collection{?page}");
            long totalItems = 86;
            int page = 9;
            int pageSize = 10;
            string pageVariable = "page";

            // when
            var view = new TemplatedPartialCollectionView(template, pageVariable, totalItems, page, pageSize);

            // then
            view.Id.Should().Be(new Uri("/some/collection?page=9", UriKind.Relative));
            view.First.Should().Be((IriRef)new Uri("/some/collection?page=1", UriKind.Relative));
            view.Last.Should().Be((IriRef)new Uri("/some/collection?page=9", UriKind.Relative));
            view.Next.Should().BeNull();
            view.Previous.Should().Be((IriRef)new Uri("/some/collection?page=8", UriKind.Relative));
        }

        [Fact]
        public void Should_calculate_page_URIs_given_first_page()
        {
            // given
            var template = new UriTemplate.Core.UriTemplate("/some/collection{?page}");
            long totalItems = 86;
            int page = 1;
            int pageSize = 10;
            string pageVariable = "page";

            // when
            var view = new TemplatedPartialCollectionView(template, pageVariable, totalItems, page, pageSize);

            // then
            view.Id.Should().Be(new Uri("/some/collection?page=1", UriKind.Relative));
            view.First.Should().Be((IriRef)new Uri("/some/collection?page=1", UriKind.Relative));
            view.Last.Should().Be((IriRef)new Uri("/some/collection?page=9", UriKind.Relative));
            view.Next.Should().Be((IriRef)new Uri("/some/collection?page=2", UriKind.Relative));
            view.Previous.Should().BeNull();
        }

        [Fact]
        public void Should_allow_filling_template_with_additional_parameters()
        {
            // given
            var template = new UriTemplate.Core.UriTemplate("/some{;subfilter}/collection{/page}{?filter}");
            long totalItems = 86;
            int page = 1;
            int pageSize = 10;
            string pageVariable = "page";
            var routeParams = new Dictionary<string, object>
            {
                { "subfilter", "people" },
                { "filter", "Tomasz" }
            };

            // when
            var view = new TemplatedPartialCollectionView(template, pageVariable, totalItems, page, pageSize, routeParams);

            // then
            view.Id.Should().Be(new Uri("/some;subfilter=people/collection/1?filter=Tomasz", UriKind.Relative));
        }
    }
}