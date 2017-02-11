using Argolis.Hydra.Core;

namespace Argolis.Hydra.Discovery
{
    /// <summary>
    /// Discovers Types and converts them into Supported Classes to
    /// be included in the <see cref="ApiDocumentation"/>
    /// </summary>
    public interface IApiDocumentationFactory
    {
        /// <summary>
        /// Creates the API documentation.
        /// </summary>
        ApiDocumentation Create();
    }
}
