using System;
using Argolis.Hydra.Core;
using FluentAssertions;
using JsonLD.Entities;
using Xunit;

namespace Argolis.Tests.Core
{
    public class ManagesBlockTests
    {
        [Fact]
        public void Constructed_with_no_terms_Should_throw()
        {
            // given
            Action construct = () => new ManagesBlock();

            // then
            construct.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Constructed_with_single_term_Should_throw()
        {
            // given
            Action construct = () => new ManagesBlock((IriRef)"http://example.com/subject");

            // then
            construct.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Constructed_with_subject_predicate_object_Should_throw()
        {
            // given
            Action construct = () => new ManagesBlock(
                (IriRef)"http://example.com/subject",
                (IriRef)"http://example.com/pred",
                (IriRef)"http://example.com/object");

            // then
            construct.Should().Throw<InvalidOperationException>();
        }
    }
}