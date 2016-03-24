using Nancy;

namespace TestNancyApp.Bootstrap
{
    public class NancyContextWrapper
    {
        public NancyContext Context { get; }

        public NancyContextWrapper(NancyContext context)
        {
            Context = context;
        }
    }
}
