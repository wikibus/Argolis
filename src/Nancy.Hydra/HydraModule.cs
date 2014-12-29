using Hydra;

namespace Nancy.Hydra
{
    /// <summary>
    /// Server Hydra API documentation
    /// </summary>
    public abstract class HydraModule : NancyModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HydraModule"/> class.
        /// </summary>
        protected HydraModule()
        {
            Get[Path] = route => CreateApiDocumentation();
        }

        /// <summary>
        /// Gets the API Documentation path.
        /// </summary>
        protected virtual string Path
        {
            get { return "doc"; }
        }

        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        protected abstract ApiDocumentation CreateApiDocumentation();
    }
}
