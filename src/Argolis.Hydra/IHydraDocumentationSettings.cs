using JsonLD.Entities;

namespace Argolis.Hydra
{
    /// <summary>
    /// Settings for serving hydra API Documentation
    /// </summary>
    public interface IHydraDocumentationSettings
    {
        /// <summary>
        /// Gets the API Documentation path.
        /// </summary>
        string DocumentationPath { get; }

        /// <summary>
        /// Gets the entry point URI.
        /// </summary>
        IriRef EntryPoint { get; }
    }
}
