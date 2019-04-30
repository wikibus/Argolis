using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Configuration;
using Nancy.Routing.UriTemplates;
using Nancy.TinyIoc;

namespace TestHydraApp.Bootstrap
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override Func<ITypeCatalog, NancyInternalConfiguration> InternalConfiguration
        {
            get
            {
                return NancyInternalConfiguration.WithOverrides(c =>
                {
                    c.RouteResolver = typeof(UriTemplateRouteResolver);
                });
            }
        }

        public override void Configure(INancyEnvironment environment)
        {
            base.Configure(environment);
            environment.Tracing(true, true);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            // don't call base to disable automatic registration
        }
    }
}