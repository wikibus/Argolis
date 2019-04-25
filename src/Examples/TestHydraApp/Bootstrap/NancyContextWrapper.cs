using Nancy;

namespace TestHydraApp.Bootstrap
{
    public class NancyContextWrapper
    {
        public NancyContextWrapper(NancyContext context)
        {
            this.Context = context;
        }

        public NancyContext Context { get; }
    }
}
