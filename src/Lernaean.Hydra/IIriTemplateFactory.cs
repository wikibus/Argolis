using Hydra.Core;

namespace Hydra
{
    /// <summary>
    /// Creates a <see cref="IriTemplate"/> for a given model allowing databinding from the URI
    /// </summary>
    public interface IIriTemplateFactory
    {
        /// <summary>
        /// Creates an <see cref="IriTemplate"/>
        /// </summary>
        /// <typeparam name="T">the model to be bound from the request</typeparam>
        IriTemplate CreateIriTemplate<T>(string path);
    }
}
