using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Argolis.Models;
using Argolis.Nancy;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests
{
    public class ArgolisModuleTests
    {
        private const string TestPath = "some/path{?s,p,q,r}";
        private readonly ArgolisModule module;

        public ArgolisModuleTests()
        {
            var templates = A.Fake<IModelTemplateProvider>();
            A.CallTo(() => templates.GetTemplate(typeof(object))).Returns(TestPath);

            this.module = new ArgolisModule(templates);
        }

        [Fact]
        public void Should_add_GET_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Get<object>(Action);

            // then
            var route = this.module.Routes.Single();
            route.Description.Method.Should().Be("GET");
            route.Description.Path.Should().Be(TestPath);
        }

        [Fact]
        public void Should_add_POST_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Post<object>(Action);

            // then
            var route = this.module.Routes.Single();
            route.Description.Method.Should().Be("POST");
            route.Description.Path.Should().Be(TestPath);
        }

        [Fact]
        public void Should_add_PATCH_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Patch<object>(Action);

            // then
            var route = this.module.Routes.Single();
            route.Description.Method.Should().Be("PATCH");
            route.Description.Path.Should().Be(TestPath);
        }

        [Fact]
        public void Should_add_HEAD_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Head<object>(Action);

            // then
            var route = this.module.Routes.Single();
            route.Description.Method.Should().Be("HEAD");
            route.Description.Path.Should().Be(TestPath);
        }

        [Fact]
        public void Should_add_OPTIONS_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Options<object>(Action);

            // then
            var route = this.module.Routes.Single();
            route.Description.Method.Should().Be("OPTIONS");
            route.Description.Path.Should().Be(TestPath);
        }

        [Fact]
        public void Should_add_PUT_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Put<object>(Action);

            // then
            var route = this.module.Routes.Single();
            route.Description.Method.Should().Be("PUT");
            route.Description.Path.Should().Be(TestPath);
        }

        [Fact]
        public void Should_add_DELETE_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Delete<object>(Action);

            // then
            var route = this.module.Routes.Single();
            route.Description.Method.Should().Be("DELETE");
            route.Description.Path.Should().Be(TestPath);
        }

        [Fact]
        public void Should_add_async_GET_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Get<object>(AsyncAction);

            // then
            this.module.Routes.Should().HaveCount(1);
        }

        [Fact]
        public void Should_add_async_POST_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Post<object>(AsyncAction);

            // then
            this.module.Routes.Should().HaveCount(1);
        }

        [Fact]
        public void Should_add_async_PATCH_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Patch<object>(AsyncAction);

            // then
            this.module.Routes.Should().HaveCount(1);
        }

        [Fact]
        public void Should_add_async_HEAD_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Head<object>(AsyncAction);

            // then
            this.module.Routes.Should().HaveCount(1);
        }

        [Fact]
        public void Should_add_async_OPTIONS_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Options<object>(AsyncAction);

            // then
            this.module.Routes.Should().HaveCount(1);
        }

        [Fact]
        public void Should_add_async_PUT_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Put<object>(AsyncAction);

            // then
            this.module.Routes.Should().HaveCount(1);
        }

        [Fact]
        public void Should_add_async_DELETE_route_with_path_from_identifier_provider()
        {
            // when
            this.module.Delete<object>(AsyncAction);

            // then
            this.module.Routes.Should().HaveCount(1);
        }

        private static object Action(object p)
        {
            return new object();
        }

        private static Task<object> AsyncAction(object p)
        {
            return Task.FromResult(new object());
        }
    }
}