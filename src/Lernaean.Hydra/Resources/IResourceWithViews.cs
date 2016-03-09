namespace Hydra.Resources
{
    /// <summary>
    /// Represents a resource, which 
    /// </summary>
    public interface IResourceWithViews
    {
        /// <summary>
        /// Gets or sets the views
        /// </summary>
        IView[] Views { get; set; }
    }
}
