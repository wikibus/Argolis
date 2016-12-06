using System;

namespace Argolis.Models
{
    /// <summary>
    /// Retrieves identifier templates associated with resource types
    /// </summary>
    public interface IModelTemplateProvider
    {
        /// <summary>
        /// Gets the template as is.
        /// </summary>
        string GetTemplate(Type type);

        /// <summary>
        /// Gets the template concatenated which should be an absolute URI.
        /// </summary>
        /// <remarks>implementers can use the <see cref="IBaseUriProvider"/> to construct absolute templates</remarks>
        string GetAbsoluteTemplate(Type type);
    }
}