using System;

namespace Argolis.Models
{
    /// <summary>
    /// Used to extract variables and values from a URI
    /// </summary>
    public interface IUriTemplateMatcher
    {
        /// <summary>
        /// Matches the specified URI against type <typeparamref name="T"/> identifier template.
        /// </summary>
        /// <typeparam name="T">resource model type</typeparam>
        /// <param name="uri">The URI (absolute or relative).</param>
        /// <returns>an object containing matched variables</returns>
        UriTemplateMatches Match<T>(Uri uri);
    }
}