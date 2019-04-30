using Nancy;
using Nancy.Bootstrapper;

namespace Argolis.Hydra.Nancy
{
    /// <summary>
    /// Registers a <see cref="NancyContext"/> wrapper with the container.
    /// </summary>
    /// <remarks>
    /// See https://github.com/NancyFx/Nancy/issues/2346 for more details
    /// </remarks>
    public class NancyContextWrapperRegistrations : Registrations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NancyContextWrapperRegistrations"/> class.
        /// </summary>
        public NancyContextWrapperRegistrations(ITypeCatalog catalog)
            : base(catalog)
        {
            this.Register<NancyContextWrapper>(typeof(NancyContextWrapper), Lifetime.PerRequest);
        }
    }
}
