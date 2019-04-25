using System;
using Argolis.Hydra.Models;
using Argolis.Models;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests.Models
{
    public class DefaultUriTemplateMatcherTests
    {
        private readonly DefaultUriTemplateMatcher matcher;
        private readonly IModelTemplateProvider templates;

        public DefaultUriTemplateMatcherTests()
        {
            this.templates = A.Fake<IModelTemplateProvider>(options => options.Strict());
            this.matcher = new DefaultUriTemplateMatcher(this.templates);
        }

        [Fact]
        public void Should_match_relative_URI_reference_with_existing_template()
        {
            // given
            A.CallTo(() => this.templates.GetTemplate(typeof(object)))
                .Returns("/{p1}/{p2}/{p3}");

            // when
            var match = this.matcher.Match<object>(new Uri("/one/two/three", UriKind.Relative));

            // then
            match.Success.Should().BeTrue();
            match["p1"].Should().Be("one");
            match["p2"].Should().Be("two");
            match["p3"].Should().Be("three");
        }

        [Fact]
        public void Should_match_absolute_URI_with_existing_template()
        {
            // given
            A.CallTo(() => this.templates.GetAbsoluteTemplate(typeof(object)))
                .Returns("http://example.com/{p1}/{p2}/{p3}");

            // when
            var match = this.matcher.Match<object>(new Uri("http://example.com/one/two/three"));

            // then
            match.Success.Should().BeTrue();
            match["p1"].Should().Be("one");
            match["p2"].Should().Be("two");
            match["p3"].Should().Be("three");
        }

        [Fact]
        public void Should_return_failed_result_when_URI_doesnt_match()
        {
            // given
            A.CallTo(() => this.templates.GetAbsoluteTemplate(typeof(object)))
                .Returns("http://example.com/{p1}/{p2}/{p3}");

            // when
            var match = this.matcher.Match<object>(new Uri("http://example.com/one/two"));

            // then
            match.Success.Should().BeFalse();
        }
    }
}