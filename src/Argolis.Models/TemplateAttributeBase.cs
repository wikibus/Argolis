using System;

namespace Argolis.Models
{
    /// <summary>
    /// Derive to annotate with resource model classes with identifier templates
    /// </summary>
    public abstract class TemplateAttributeBase : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TemplateAttributeBase"/> class.
        /// </summary>
        /// <param name="template">The template string.</param>
        protected TemplateAttributeBase(string template)
        {
            this.Template = template;
        }

        /// <summary>
        /// Gets the URI Template.
        /// </summary>
        public string Template { get; private set; }
    }
}