using System;
using System.Collections.Generic;
using Argolis.Models;
using Argolis.Models.TunnelVisionLabs;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests.Models
{
    public class TunnelVisionLabsUriTemplateExpanderTests
    {
        private readonly TunnelVisionLabsUriTemplateExpander expander;
        private readonly IModelTemplateProvider templates;

        public TunnelVisionLabsUriTemplateExpanderTests()
        {
            this.templates = A.Fake<IModelTemplateProvider>(options => options.Strict());
            this.expander = new TunnelVisionLabsUriTemplateExpander(this.templates);
        }

        [Fact]
        public void Should_expand_URI_reference_using_dictionary()
        {
            // given
            A.CallTo(() => this.templates.GetTemplate(typeof(object)))
                .Returns("/{user}{;include}{?page}");
            var parameters = new Dictionary<string, object>
            {
                ["page"] = 10,
                ["user"] = "Tomasz",
                ["include"] = new[] { "stars", "likes" }
            };

            // when
            var uri = this.expander.Expand<object>(parameters);

            // then
            uri.Should().Be(new Uri("/Tomasz;include=stars,likes?page=10", UriKind.Relative));
        }

        [Fact]
        public void Should_expand_URI_reference_using_anynymous_object()
        {
            // given
            A.CallTo(() => this.templates.GetTemplate(typeof(object)))
                .Returns("/{user}{;include}{?page}");
            var parameters = new
            {
                page = 10,
                user = "Tomasz",
                include = new[] { "stars", "likes" }
            };

            // when
            var uri = this.expander.Expand<object>(parameters);

            // then
            uri.Should().Be(new Uri("/Tomasz;include=stars,likes?page=10", UriKind.Relative));
        }

        [Fact]
        public void Should_expand_URI_reference_using_dictionary_passed_as_object()
        {
            // given
            A.CallTo(() => this.templates.GetTemplate(typeof(object)))
                .Returns("/{user}{;include}{?page}");
            var parameters = new Dictionary<string, object>
            {
                ["page"] = 10,
                ["user"] = "Tomasz",
                ["include"] = new[] { "stars", "likes" }
            };

            // when
            var uri = this.expander.Expand<object>((object)parameters);

            // then
            uri.Should().Be(new Uri("/Tomasz;include=stars,likes?page=10", UriKind.Relative));
        }

        [Fact]
        public void Should_expand_absolute_URI_using_dictionary()
        {
            // given
            A.CallTo(() => this.templates.GetAbsoluteTemplate(typeof(object)))
                .Returns("http://example.com/{user}{;include}{?page}");
            var parameters = new Dictionary<string, object>
            {
                ["page"] = 10,
                ["user"] = "Tomasz",
                ["include"] = new[] { "stars", "likes" }
            };

            // when
            var uri = this.expander.ExpandAbsolute<object>(parameters);

            // then
            uri.Should().Be(new Uri("http://example.com/Tomasz;include=stars,likes?page=10"));
        }

        [Fact]
        public void Should_expand_absolute_URI_using_anynymous_object()
        {
            // given
            A.CallTo(() => this.templates.GetAbsoluteTemplate(typeof(object)))
                .Returns("http://example.com/{user}{;include}{?page}");
            var parameters = new
            {
                page = 10,
                user = "Tomasz",
                include = new[] { "stars", "likes" }
            };

            // when
            var uri = this.expander.ExpandAbsolute<object>(parameters);

            // then
            uri.Should().Be(new Uri("http://example.com/Tomasz;include=stars,likes?page=10"));
        }

        [Fact]
        public void Should_expand_absolute_URI_using_dictionary_passed_as_object()
        {
            // given
            A.CallTo(() => this.templates.GetAbsoluteTemplate(typeof(object)))
                .Returns("http://example.com/{user}{;include}{?page}");
            var parameters = new Dictionary<string, object>
            {
                ["page"] = 10,
                ["user"] = "Tomasz",
                ["include"] = new[] { "stars", "likes" }
            };

            // when
            var uri = this.expander.ExpandAbsolute<object>((object)parameters);

            // then
            uri.Should().Be(new Uri("http://example.com/Tomasz;include=stars,likes?page=10"));
        }
    }
}