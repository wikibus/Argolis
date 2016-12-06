using System;
using System.Reflection;

namespace Argolis.Models
{
    /// <summary>
    /// Retrieves resource idenftifier templates from attributes
    /// </summary>
    /// <seealso cref="Argolis.Models.IModelTemplateProvider" />
    public class AttributeModelTemplateProvider : IModelTemplateProvider
    {
        private readonly IBaseUriProvider baseUriProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeModelTemplateProvider"/> class.
        /// </summary>
        public AttributeModelTemplateProvider(IBaseUriProvider baseUriProvider)
        {
            this.baseUriProvider = baseUriProvider;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttributeModelTemplateProvider"/> class.
        /// </summary>
        public AttributeModelTemplateProvider()
        {
        }

        /// <summary>
        /// Gets the template just as declared by the <see cref="IdentifierTemplateAttribute"/>.
        /// </summary>
        /// <exception cref="MissingTemplateException">when the attribute is not found</exception>
        public string GetTemplate(Type type)
        {
            return GetIdentifierTemplateAttribute(type).Template;
        }

        /// <summary>
        /// Gets the template as declared by the <see cref="IdentifierTemplateAttribute"/>, prefixed with a base URI.
        /// </summary>
        /// <exception cref="MissingTemplateException">when the attribute is not found</exception>
        public string GetAbsoluteTemplate(Type type)
        {
            return this.baseUriProvider.BaseResourceUri + GetIdentifierTemplateAttribute(type).Template;
        }

        private static IdentifierTemplateAttribute GetIdentifierTemplateAttribute(Type type)
        {
            var templateAttribute = type.GetCustomAttribute<IdentifierTemplateAttribute>();
            if (templateAttribute == null)
            {
                throw new MissingTemplateException(type);
            }

            return templateAttribute;
        }
    }
}
