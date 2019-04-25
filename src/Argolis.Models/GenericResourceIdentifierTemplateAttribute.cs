using System;
using System.Reflection;

namespace Argolis.Models
{
    /// <summary>
    /// Provides identifer template for generic resources (useful for collections)
    /// </summary>
    /// <seealso cref="Argolis.Models.TemplateAttributeBase" />
    public abstract class GenericResourceIdentifierTemplateAttribute : TemplateAttributeBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericResourceIdentifierTemplateAttribute"/> class.
        /// </summary>
        /// <param name="template">The template string.</param>
        /// <param name="containerType">Generic container type (must have exactly one generic type parameter)</param>
        protected GenericResourceIdentifierTemplateAttribute(string template, Type containerType)
            : base(template)
        {
            if (containerType.GetTypeInfo().IsGenericTypeDefinition == false)
            {
                throw new ArgumentException("Container type must be a generic type", nameof(containerType));
            }

            if (containerType.GetTypeInfo().GenericTypeParameters.Length != 1)
            {
                throw new ArgumentException("Container type must have exactly one generic argument", nameof(containerType));
            }

            this.ContainerType = containerType;
        }

        /// <summary>
        /// Gets the container type.
        /// </summary>
        public Type ContainerType { get; }
    }
}