using Microsoft.Owin;
using Owin;
using TestNancyApp;

[assembly:OwinStartup(typeof(Startup))]

namespace TestNancyApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}