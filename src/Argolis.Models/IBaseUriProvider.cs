namespace Argolis.Models
{
    /// <summary>
    /// Provider of base URI used when constructing absolute resource identifiers
    /// </summary>
    public interface IBaseUriProvider
    {
        /// <summary>
        /// Gets the base resource URI.
        /// </summary>
        string BaseResourceUri { get; }
    }
}