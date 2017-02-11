namespace Hydra.Resources
{
    /// <summary>
    /// Represents a resource, which can be "viewed" in different perspectives
    /// </summary>
    public interface IResourceWithViews
    {
        /// <summary>
        /// Gets or sets the views
        /// </summary>
        IView[] Views { get; set; }
    }
}
