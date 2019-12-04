using System.Collections.Generic;
using Argolis.Hydra.Discovery.SupportedOperations;
using Dynamitey;
using FluentAssertions;
using JsonLD.Entities;
using Vocab;
using Xunit;

namespace Argolis.Tests.ApiDocumentation
{
    public class SupportedOperationBuilderTests
    {
        private readonly SupportedOperationCollection builder;
        private readonly List<OperationMeta> operations;

        public SupportedOperationBuilderTests()
        {
            this.operations = new List<OperationMeta>();
            this.builder = new SupportedOperationCollection(this.operations);
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
            Dynamic.InvokeMember(this.builder, operationMethodName);

            // then
            this.operations.Should().Contain(meta => meta.Method == expectedMethod);
        }

        [Fact]
        public void Should_store_operation_meta_with_correct_custom_method()
        {
            // given
            const string method = "RESERVE";

            // when
            this.builder.Supports(method);

            // then
            this.operations.Should().Contain(meta => meta.Method == method);
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
            this.builder.Supports(method)
                .Title(title)
                .Description(description)
                .Expects(expects)
                .Returns(returns);

            // then
            this.operations.Should()
                .Contain(meta => meta.Title == title &&
                                 meta.Description == description &&
                                 meta.Expects == expects &&
                                 meta.Returns == returns);
        }
    }
}