using System.Collections.Generic;
using Hydra;
using JsonLD.Entities;
using Microsoft.Owin;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Rdf;
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
                container.Register(typeof(IHydraDocumentationSettings), typeof(HydraDocumentationSettings)).AsSingleton();
            }
        }
    }
}