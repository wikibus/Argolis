using System.Collections.Generic;
using FluentAssertions;
using Hydra.Discovery.SupportedOperations;
using ImpromptuInterface;
using JsonLD.Entities;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class SupportedOperationBuilderTests
    {
        private readonly SupportedOperationBuilder _builder;
        private readonly List<OperationMeta> _operations;

        public SupportedOperationBuilderTests()
        {
            _operations = new List<OperationMeta>();
            _builder = new SupportedOperationBuilder(_operations);
        }

        [Theory]
        [InlineData("SupportsGet", HttpMethod.Get)]
        [InlineData("SupportsPost", HttpMethod.Post)]
        [InlineData("SupportsDelete", HttpMethod.Delete)]
        [InlineData("SupportsPatch", HttpMethod.Patch)]
        [InlineData("SupportsPut", HttpMethod.Put)]
        public void Should_store_operation_meta_with_correct_method(string operationMethodName, string expectedMethod)
        {
            // when
            Impromptu.InvokeMember(_builder, operationMethodName);

            // then
            _operations.Should().Contain(meta => meta.Method == expectedMethod);
        }

        [Fact]
        public void Should_store_operation_meta_with_correct_custom_method()
        {
            // given
            const string method = "RESERVE";

            // when
            _builder.Supports(method);

            // then
            _operations.Should().Contain(meta => meta.Method == method);
        }

        [Fact]
        public void Should_store_operation_meta_with_with_correct_extended_meta()
        {
            // given
            const string method = "RESERVE";
            const string title = "Reserves a seat";
            const string description = "Used to create a new reservation";
            var expects = (IriRef)Foaf.Document;
            var returns = (IriRef)Foaf.Project;

            // when
            _builder.Supports(
                method,
                title,
                description,
                expects,
                returns);

            // then
            _operations.Should()
                .Contain(meta => meta.Title == title &&
                                 meta.Description == description &&
                                 meta.Expects == expects &&
                                 meta.Returns == returns);
        }
    }
}