using Nancy;
using Nancy.Bootstrapper;

namespace Argolis.Hydra.Nancy
{
    /// <summary>
    /// Sets Nancy context to current request's wrapper
    /// </summary>
    public class NancyContextWrapperStartup : IRequestStartup
    {
        private readonly NancyContextWrapper contextWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NancyContextWrapperStartup"/> class.
        /// </summary>
        public NancyContextWrapperStartup(NancyContextWrapper contextWrapper)
        {
            this.contextWrapper = contextWrapper;
        }

        /// <inheritdoc />
        public void Initialize(IPipelines pipelines, NancyContext context)
        {
            this.contextWrapper.SetContext(context);
        }
    }
}
