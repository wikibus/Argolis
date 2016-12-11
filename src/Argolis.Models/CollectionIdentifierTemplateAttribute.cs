namespace Argolis.Models
{
    /// <summary>
    /// Provides identifer template for collection resource
    /// </summary>
    /// <seealso cref="Argolis.Models.TemplateAttributeBase" />
    public class CollectionIdentifierTemplateAttribute : TemplateAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionIdentifierTemplateAttribute"/> class.
        /// </summary>
        /// <param name="template">The template string.</param>
        public CollectionIdentifierTemplateAttribute(string template)
            : base(template)
        {
        }
    }
}