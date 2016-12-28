using System.Linq;
using System.Threading.Tasks;
using Argolis.Models;
using Argolis.UriTemplates.Nancy;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace Argolis.Tests.UriTemplates
{
    public class ArgolisModuleTests
    {
        private const string TestTemplate = "/i/can/haz/template";
        private readonly IModelTemplateProvider templateProvider;

        public ArgolisModuleTests()
        {
            this.templateProvider = A.Fake<IModelTemplateProvider>();

            A.CallTo(() => this.templateProvider.GetTemplate(typeof(object))).Returns(TestTemplate);
        }

        [Fact]
        public void Get_sync_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Get<object>(_ => new object());

            // then
            module.Routes.Single().Description.Method.Should().Be("GET");
        }

        [Fact]
        public void Get_async_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Get<object>(_ => Task.FromResult(new object()));

            // then
            module.Routes.Single().Description.Method.Should().Be("GET");
        }

        [Fact]
        public void Get_async_with_token_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Get<object>((_, c) => null);

            // then
            module.Routes.Single().Description.Method.Should().Be("GET");
        }

        [Fact]
        public void Get_route_should_add_correct_route_entry()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Get<object>(_ => new object(), context => true, "TestRoute");
            module.Get<object>((_, c) => Task.FromResult(new object()), context => true, "TestRoute");
            module.Get<object>(_ => Task.FromResult(new object()), context => true, "TestRoute");

            // then
            foreach (var route in module.Routes)
            {
                var description = route.Description;

                description.Condition.Should().NotBeNull();
                description.Name.Should().Be("TestRoute");
                description.Path.Should().Be(TestTemplate);
            }
        }

        [Fact]
        public void Put_sync_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Put<object>(_ => new object());

            // then
            module.Routes.Single().Description.Method.Should().Be("PUT");
        }

        [Fact]
        public void Put_async_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Put<object>(_ => Task.FromResult(new object()));

            // then
            module.Routes.Single().Description.Method.Should().Be("PUT");
        }

        [Fact]
        public void Put_async_with_token_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Put<object>((_, c) => null);

            // then
            module.Routes.Single().Description.Method.Should().Be("PUT");
        }

        [Fact]
        public void Put_route_should_add_correct_route_entry()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Put<object>(_ => new object(), context => true, "TestRoute");
            module.Put<object>((_, c) => Task.FromResult(new object()), context => true, "TestRoute");
            module.Put<object>(_ => Task.FromResult(new object()), context => true, "TestRoute");

            // then
            foreach (var route in module.Routes)
            {
                var description = route.Description;

                description.Condition.Should().NotBeNull();
                description.Name.Should().Be("TestRoute");
                description.Path.Should().Be(TestTemplate);
            }
        }

        [Fact]
        public void Post_sync_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Post<object>(_ => new object());

            // then
            module.Routes.Single().Description.Method.Should().Be("POST");
        }

        [Fact]
        public void Post_async_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Post<object>(_ => Task.FromResult(new object()));

            // then
            module.Routes.Single().Description.Method.Should().Be("POST");
        }

        [Fact]
        public void Post_async_with_token_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Post<object>((_, c) => null);

            // then
            module.Routes.Single().Description.Method.Should().Be("POST");
        }

        [Fact]
        public void Post_route_should_add_correct_route_entry()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Post<object>(_ => new object(), context => true, "TestRoute");
            module.Post<object>((_, c) => Task.FromResult(new object()), context => true, "TestRoute");
            module.Post<object>(_ => Task.FromResult(new object()), context => true, "TestRoute");

            // then
            foreach (var route in module.Routes)
            {
                var description = route.Description;

                description.Condition.Should().NotBeNull();
                description.Name.Should().Be("TestRoute");
                description.Path.Should().Be(TestTemplate);
            }
        }

        [Fact]
        public void Delete_sync_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Delete<object>(_ => new object());

            // then
            module.Routes.Single().Description.Method.Should().Be("DELETE");
        }

        [Fact]
        public void Delete_async_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Delete<object>(_ => Task.FromResult(new object()));

            // then
            module.Routes.Single().Description.Method.Should().Be("DELETE");
        }

        [Fact]
        public void Delete_async_with_token_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Delete<object>((_, c) => null);

            // then
            module.Routes.Single().Description.Method.Should().Be("DELETE");
        }

        [Fact]
        public void Delete_route_should_add_correct_route_entry()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Delete<object>(_ => new object(), context => true, "TestRoute");
            module.Delete<object>((_, c) => Task.FromResult(new object()), context => true, "TestRoute");
            module.Delete<object>(_ => Task.FromResult(new object()), context => true, "TestRoute");

            // then
            foreach (var route in module.Routes)
            {
                var description = route.Description;

                description.Condition.Should().NotBeNull();
                description.Name.Should().Be("TestRoute");
                description.Path.Should().Be(TestTemplate);
            }
        }

        [Fact]
        public void Patch_sync_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Patch<object>(_ => new object());

            // then
            module.Routes.Single().Description.Method.Should().Be("PATCH");
        }

        [Fact]
        public void Patch_async_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Patch<object>(_ => Task.FromResult(new object()));

            // then
            module.Routes.Single().Description.Method.Should().Be("PATCH");
        }

        [Fact]
        public void Patch_async_with_token_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Patch<object>((_, c) => null);

            // then
            module.Routes.Single().Description.Method.Should().Be("PATCH");
        }

        [Fact]
        public void Patch_route_should_add_correct_route_entry()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Patch<object>(_ => new object(), context => true, "TestRoute");
            module.Patch<object>((_, c) => Task.FromResult(new object()), context => true, "TestRoute");
            module.Patch<object>(_ => Task.FromResult(new object()), context => true, "TestRoute");

            // then
            foreach (var route in module.Routes)
            {
                var description = route.Description;

                description.Condition.Should().NotBeNull();
                description.Name.Should().Be("TestRoute");
                description.Path.Should().Be(TestTemplate);
            }
        }

        [Fact]
        public void Options_sync_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Options<object>(_ => new object());

            // then
            module.Routes.Single().Description.Method.Should().Be("OPTIONS");
        }

        [Fact]
        public void Options_async_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Options<object>(_ => Task.FromResult(new object()));

            // then
            module.Routes.Single().Description.Method.Should().Be("OPTIONS");
        }

        [Fact]
        public void Options_async_with_token_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Options<object>((_, c) => null);

            // then
            module.Routes.Single().Description.Method.Should().Be("OPTIONS");
        }

        [Fact]
        public void Options_route_should_add_correct_route_entry()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Options<object>(_ => new object(), context => true, "TestRoute");
            module.Options<object>((_, c) => Task.FromResult(new object()), context => true, "TestRoute");
            module.Options<object>(_ => Task.FromResult(new object()), context => true, "TestRoute");

            // then
            foreach (var route in module.Routes)
            {
                var description = route.Description;

                description.Condition.Should().NotBeNull();
                description.Name.Should().Be("TestRoute");
                description.Path.Should().Be(TestTemplate);
            }
        }

        [Fact]
        public void Head_sync_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Head<object>(_ => new object());

            // then
            module.Routes.Single().Description.Method.Should().Be("HEAD");
        }

        [Fact]
        public void Head_async_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Head<object>(_ => Task.FromResult(new object()));

            // then
            module.Routes.Single().Description.Method.Should().Be("HEAD");
        }

        [Fact]
        public void Head_async_with_token_should_add_route()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Head<object>((_, c) => null);

            // then
            module.Routes.Single().Description.Method.Should().Be("HEAD");
        }

        [Fact]
        public void Head_route_should_add_correct_route_entry()
        {
            // given
            var module = new ArgolisModuleTestable(this.templateProvider);

            // when
            module.Head<object>(_ => new object(), context => true, "TestRoute");
            module.Head<object>((_, c) => Task.FromResult(new object()), context => true, "TestRoute");
            module.Head<object>(_ => Task.FromResult(new object()), context => true, "TestRoute");

            // then
            foreach (var route in module.Routes)
            {
                var description = route.Description;

                description.Condition.Should().NotBeNull();
                description.Name.Should().Be("TestRoute");
                description.Path.Should().Be(TestTemplate);
            }
        }

        private class ArgolisModuleTestable : ArgolisModule
        {
            public ArgolisModuleTestable(IModelTemplateProvider provider)
                : base(provider)
            {
            }
        }
    }
}