using System;
using System.Linq.Expressions;
using FluentAssertions;
using Hydra.Discovery.SupportedOperations;
using ImpromptuInterface;
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

        [Theory]
        [InlineData("SupportsGet", HttpMethod.Get)]
        [InlineData("SupportsPost", HttpMethod.Post)]
        [InlineData("SupportsDelete", HttpMethod.Delete)]
        [InlineData("SupportsPatch", HttpMethod.Patch)]
        [InlineData("SupportsPut", HttpMethod.Put)]
        public void Should_store_operation_meta_with_with_correct_method(string operationMethodName, string expectedMethod)
        {
            // given
            var builder = _operations.Prop(i => i.Title);

            // when
            Impromptu.InvokeMember(builder, operationMethodName);

            // then
            _operations.GetSupportedPropertyOperations(typeof(Issue).GetProperty("Title"))
                .Should().Contain(meta => meta.Method == expectedMethod);
        }
        
        [Theory]
        [InlineData("SupportsGet", HttpMethod.Get)]
        [InlineData("SupportsPost", HttpMethod.Post)]
        [InlineData("SupportsDelete", HttpMethod.Delete)]
        [InlineData("SupportsPatch", HttpMethod.Patch)]
        [InlineData("SupportsPut", HttpMethod.Put)]
        public void Should_contain_defaults_for_an_operation(string operationMethodName, string expectedMethod)
        {
            // given
            _operations.Supports(operationMethodName);

            // then
            _operations.GetSupportedClassOperations()
                .Should().Contain(meta => meta.Method == expectedMethod);
        }

        private class TestOperations : SupportedOperations<Issue>
        {
            public SupportedOperationBuilder Prop(Expression<Func<Issue, string>> propertyExpression)
            {
                return Property(propertyExpression);
            }

            public SupportedOperationBuilder Supports(string protectedMethodName)
            {
                return Impromptu.InvokeMember(Class, protectedMethodName);
            }
        }
    }
}