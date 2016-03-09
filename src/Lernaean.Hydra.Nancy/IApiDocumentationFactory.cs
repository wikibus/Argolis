using Hydra.Core;

namespace Hydra.Nancy
{
    /// <summary>
    /// Implementing class should return
    /// </summary>
    public interface IApiDocumentationFactory
    {
        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        ApiDocumentation CreateApiDocumentation();
    }
}