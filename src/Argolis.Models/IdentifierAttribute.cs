using System;

namespace Argolis.Models
{
    /// <summary>
    /// Lets API authors define template for identifiers identifying decorated resource type
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class IdentifierAttribute : TemplateAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierAttribute"/> class.
        /// </summary>
        /// <param name="template">The URI template string.</param>
        public IdentifierAttribute(string template)
            : base(template)
        {
        }
    }
}