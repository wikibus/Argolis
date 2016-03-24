using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace TestNancyApp.Bootstrap
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            StaticConfiguration.DisableErrorTraces = false;
            // don't call base to disable automatic registration
        }

        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);

            container.Register(new NancyContextWrapper(context));
        }
    }
}