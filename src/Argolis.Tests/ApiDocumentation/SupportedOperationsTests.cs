using System;
using System.Linq.Expressions;
using Argolis.Hydra.Discovery.SupportedOperations;
using FluentAssertions;
using TestHydraApi;
using Xunit;

namespace Argolis.Tests.ApiDocumentation
{
    public class SupportedOperationsTests
    {
        private readonly TestOperations operations;

        public SupportedOperationsTests()
        {
            this.operations = new TestOperations();
        }

        [Fact]
        public void Should_throw_when_non_propety_is_passed()
        {
            Assert.ThrowsAny<ArgumentException>(() => this.operations.Prop(i => i.Method()));
        }

        [Fact]
        public void Should_return_same_builder_for_class()
        {
            // given
            var builder = this.operations.Class;

            // when
            var builder2 = this.operations.Class;

            // then
            builder.Should().BeSameAs(builder2);
        }

        [Fact]
        public void Should_return_same_builder_for_property()
        {
            // given
            var builder = this.operations.Class;

            // when
            var builder2 = this.operations.Class;

            // then
            builder.Should().BeSameAs(builder2);
        }

        private class TestOperations : SupportedOperations<Issue>
        {
            public SupportedOperationBuilder Prop(Expression<Func<Issue, string>> propertyExpression)
            {
                return this.Property(propertyExpression);
            }
        }
    }
}