using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using Hydra.Discovery.SupportedOperations;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using TestHydraApi;
using Vocab;
using Xunit;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    public class DefaultSupportedOperationFactoryTests
    {
        private const string IssueType = "http://example.com/ontolgy#Issue";
        private static readonly Dictionary<Type, Uri> ClassIds;
        private readonly DefaultSupportedOperationFactory _factory;
        private readonly ISupportedOperations _operations;
        private readonly IPropertyRangeRetrievalPolicy _propertyRanges;

        public DefaultSupportedOperationFactoryTests()
        {
            _operations = A.Fake<ISupportedOperations>();
            _propertyRanges = A.Fake<IPropertyRangeRetrievalPolicy>();
            _factory = new DefaultSupportedOperationFactory(new[] { _operations }, _propertyRanges);
        }

        static DefaultSupportedOperationFactoryTests()
        {
            ClassIds = new Dictionary<Type, Uri>
            {
                { typeof(Issue), new Uri(IssueType) }
            };
        }

        [Theory]
        [InlineData("HEAD")]
        [InlineData("GET")]
        [InlineData("TRACE")]
        [InlineData("DELETE")]
        public void Operation_should_always_expect_nothing(string method)
        {
            // given
            A.CallTo(() => _operations.Type).Returns(typeof(Issue));
            A.CallTo(() => _operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta { Method = method, Expects = (IriRef)Rdfs.Resource }
            });

            // when
            var operation = _factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Expects.Should().Be((IriRef)Owl.Nothing);
        }

        [Fact]
        public void PUT_operation_should_always_return_nothing()
        {
            // given
            A.CallTo(() => _operations.Type).Returns(typeof(Issue));
            A.CallTo(() => _operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta { Method = HttpMethod.Put, Expects = (IriRef)Rdfs.Resource }
            });

            // when
            var operation = _factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Returns.Should().Be((IriRef)Owl.Nothing);
        }

        [Fact]
        public void GET_operation_should_always_return_model_type()
        {
            // given
            A.CallTo(() => _operations.Type).Returns(typeof(Issue));
            A.CallTo(() => _operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta { Method = "GET", Returns = (IriRef)Schema.Person }
            });

            // when
            var operation = _factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Returns.Should().Be((IriRef)IssueType);
        }

        [Fact]
        public void Operation_should_have_correct_method()
        {
            // given
            A.CallTo(() => _operations.Type).Returns(typeof(Issue));
            A.CallTo(() => _operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta { Method = "SELECT", Returns = (IriRef)Schema.Person }
            });

            // when
            var operation = _factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Method.Should().Be("SELECT");
        }

        [Fact]
        public void Operation_should_have_return_as_configured_value()
        {
            // given
            A.CallTo(() => _operations.Type).Returns(typeof(Issue));
            A.CallTo(() => _operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta { Method = "POST", Returns = (IriRef)Schema.Person }
            });

            // when
            var operation = _factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Returns.Should().Be((IriRef)Schema.Person);
        }

        [Fact]
        public void PUT_operation_should_always_expect_model_type()
        {
            // given
            A.CallTo(() => _operations.Type).Returns(typeof(Issue));
            A.CallTo(() => _operations.GetSupportedClassOperations()).Returns(new[]
            {
                new OperationMeta { Method = "PUT", Expects = (IriRef)Foaf.Agent }
            });

            // when
            var operation = _factory.CreateOperations(typeof(Issue), ClassIds).Single();

            // then
            operation.Expects.Should().Be((IriRef)IssueType);
        }
    }
}
