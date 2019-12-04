using System;
using System.Collections.Generic;
using System.Linq;
using Argolis.Hydra.Discovery.SupportedOperations;
using Argolis.Hydra.Discovery.SupportedProperties;
using FakeItEasy;
using FluentAssertions;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Argolis.Tests.ApiDocumentation
{
    public class DefaultSupportedOperationFactoryTests
    {
        private const string IssueType = "http://example.com/ontolgy#Issue";
        private static readonly Dictionary<Type, Uri> ClassIds;
        private readonly DefaultSupportedOperationFactory factory;
        private readonly ISupportedOperations operations;
        private readonly IPropertyRangeRetrievalPolicy propertyRanges;

        static DefaultSupportedOperationFactoryTests()
        {
            ClassIds = new Dictionary<Type, Uri>
            {
                { typeof(Issue), new Uri(IssueType) }
            };
        }

        public DefaultSupportedOperationFactoryTests()
        {
            this.operations = A.Fake<ISupportedOperations>();
            this.propertyRanges = A.Fake<IPropertyRangeRetrievalPolicy>();
            this.factory = new DefaultSupportedOperationFactory(new[] { this.operations }, this.propertyRanges);
        }

        [Theory]
        [InlineData(HttpMethod.Head)]
        [InlineData(HttpMethod.Get)]
        [InlineData(HttpMethod.Trace)]
        [InlineData(HttpMethod.Delete)]
        public void Operation_should_always_expect_nothing(string method)
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(method, expects: (IriRef)Rdfs.Resource)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Expects.Should().Be((IriRef)Owl.Nothing);
        }

        [Fact]
        public void PUT_operation_should_always_return_nothing()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Put, expects: (IriRef)Rdfs.Resource)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Returns.Should().Be((IriRef)Owl.Nothing);
        }

        [Fact]
        public void GET_operation_should_always_return_model_type()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Get, returns: (IriRef)Schema.Person)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Returns.Should().Be((IriRef)IssueType);
        }

        [Fact]
        public void DELETE_operation_should_return_nothing_by_default()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Delete)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Returns.Should().Be((IriRef)Owl.Nothing);
        }

        [Fact]
        public void Operation_should_have_correct_method()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta("SELECT", returns: (IriRef)Schema.Person)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Method.Should().Be("SELECT");
        }

        [Fact]
        public void Operation_should_have_return_as_configured_value()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Post, returns: (IriRef)Schema.Person)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Returns.Should().Be((IriRef)Schema.Person);
        }

        [Fact]
        public void PUT_operation_should_always_expect_model_type()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Put, expects: (IriRef)Foaf.Person)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Expects.Should().Be((IriRef)IssueType);
        }

        [Fact]
        public void Operation_should_have_title_as_configured()
        {
            // given
            const string asExpected = "The title";
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Post, title: asExpected)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Title.Should().Be(asExpected);
        }

        [Fact]
        public void Operation_should_have_description_as_configured()
        {
            // given
            const string asExpected = "The description";
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Post, description: asExpected)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Description.Should().Be(asExpected);
        }

        [Fact]
        public void Operation_should_have_types_as_configured_along_with_hydra_type()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Post)
                {
                    Types = new[]
                    {
                        (IriRef)Schema.CreateAction,
                        (IriRef)Schema.UpdateAction,
                    }
                }
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Types.Should().Contain(Vocab.Hydra.Operation);
            operation.Types.Should().Contain(Schema.UpdateAction);
            operation.Types.Should().Contain(Schema.UpdateAction);
        }

        [Fact]
        public void Operation_should_have_hydra_types_by_default()
        {
            // given
            A.CallTo(() => this.operations.Type).Returns(typeof(Issue));
            A.CallTo(() => this.operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta(HttpMethod.Post)
            });

            // when
            var operation = this.factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Types.Should().HaveCount(1);
            operation.Types.Should().Contain(Vocab.Hydra.Operation);
        }
    }
}
