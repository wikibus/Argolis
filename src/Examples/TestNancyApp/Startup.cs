using Microsoft.Owin;
using Nancy;
using Nancy.TinyIoc;
using Owin;
using TestNancyApp;

[assembly: OwinStartup(typeof(Startup))]

namespace TestNancyApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy(options => options.Bootstrapper = new TestBootstrapper());
        }

        public class TestBootstrapper : DefaultNancyBootstrapper
        {
            protected override void ConfigureApplicationContainer(TinyIoCContainer container)
            {
                // don't call base to disable automatic registration
            }
        }
    }
}