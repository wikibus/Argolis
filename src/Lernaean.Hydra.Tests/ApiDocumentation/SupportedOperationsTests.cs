using System;
using System.Linq.Expressions;
using FluentAssertions;
using Hydra.Discovery.SupportedOperations;
using TestHydraApi;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class SupportedOperationsTests
    {
        private readonly TestOperations _operations;

        public SupportedOperationsTests()
        {
            _operations = new TestOperations();
        }

        [Fact]
        public void Should_throw_when_non_propety_is_passed()
        {
            Assert.ThrowsAny<ArgumentException>(() => _operations.Prop(i => i.Method()));
        }

        [Fact]
        public void Should_return_same_builder_for_class()
        {
            // given
            var builder = _operations.Class;

            // when
            var builder2 = _operations.Class;

            // then
            builder.Should().BeSameAs(builder2);
        }

        [Fact]
        public void Should_return_same_builder_for_property()
        {
            // given
            var builder = _operations.Class;

            // when
            var builder2 = _operations.Class;

            // then
            builder.Should().BeSameAs(builder2);
        }

        private class TestOperations : SupportedOperations<Issue>
        {
            public SupportedOperationBuilder Prop(Expression<Func<Issue, string>> propertyExpression)
            {
                return Property(propertyExpression);
            }
        }
    }
}