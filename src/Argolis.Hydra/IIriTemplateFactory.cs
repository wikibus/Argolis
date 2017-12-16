using Argolis.Hydra.Core;
using Argolis.Models;

namespace Argolis.Hydra
{
    /// <summary>
    /// Creates a <see cref="IriTemplate"/> for a given model allowing databinding from the URI
    /// </summary>
    public interface IIriTemplateFactory
    {
        /// <summary>
        /// Creates an <see cref="IriTemplate"/> by extending template for <typeparamref name="TModel"/>
        /// </summary>
        /// <typeparam name="TExtension">the model to be bound from the request</typeparam>
        /// <typeparam name="TModel">model which receives additional paramters</typeparam>
        IriTemplate CreateIriTemplate<TExtension, TModel>()
            where TExtension : ITemplateParameters<TModel>;
    }
}
