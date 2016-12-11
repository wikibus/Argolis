using System;
using System.Collections.Generic;

namespace Argolis.Models
{
    /// <summary>
    /// Used to expand a template by substituting variables with given parameters
    /// </summary>
    public interface IUriTemplateExpander
    {
        /// <summary>
        /// Expands template associated with type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">type of resource model</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A relative or absolute URI depending on the URI template</returns>
        Uri Expand<T>(IDictionary<string, object> parameters);

        /// <summary>
        /// Expands template associated with type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">type of resource model</typeparam>
        /// <param name="parameters">The parameters anonymous object.</param>
        /// <returns>A relative or absolute URI depending on the URI template</returns>
        Uri Expand<T>(object parameters);

        /// <summary>
        /// Expands template associated with type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">type of resource model</typeparam>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A absolute URI identifier</returns>
        Uri ExpandAbsolute<T>(IDictionary<string, object> parameters);

        /// <summary>
        /// Expands template associated with type <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">type of resource model</typeparam>
        /// <param name="parameters">The parameters anonymous object.</param>
        /// <returns>An absolute URI identifier</returns>
        Uri ExpandAbsolute<T>(object parameters);
    }
}