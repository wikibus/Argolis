using System;

namespace Argolis.Models
{
    /// <summary>
    /// Lets API authors define template for identifiers identifying decorated resource type
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class IdentifierTemplateAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierTemplateAttribute"/> class.
        /// </summary>
        /// <param name="template">The URI template string.</param>
        public IdentifierTemplateAttribute(string template)
        {
            this.Template = template;
        }

        /// <summary>
        /// Gets the URI Template.
        /// </summary>
        public string Template { get; private set; }
    }
}