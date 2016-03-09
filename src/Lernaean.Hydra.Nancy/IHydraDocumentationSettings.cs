namespace Hydra.Nancy
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
    }
}