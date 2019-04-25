using System;
using System.Linq;
using System.Reflection;
using NullGuard;

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
        /// Gets the template just as declared by the <see cref="IdentifierAttribute"/>.
        /// </summary>
        /// <exception cref="MissingTemplateException">when the attribute is not found</exception>
        public string GetTemplate(Type type)
        {
            return this.GetIdentifierTemplate(type);
        }

        /// <summary>
        /// Gets the template as declared by the <see cref="IdentifierAttribute"/>, prefixed with a base URI.
        /// </summary>
        /// <exception cref="MissingTemplateException">when the attribute is not found</exception>
        public string GetAbsoluteTemplate(Type type)
        {
            if (this.baseUriProvider != null)
            {
                return this.baseUriProvider.BaseResourceUri + this.GetIdentifierTemplate(type);
            }

            throw new MissingTemplateException(type, new Exception($"No {typeof(IBaseUriProvider).Name} was provided"));
        }

        /// <summary>
        /// Gets the template attribute for given <paramref name="type"/>.
        /// </summary>
        [return: AllowNull]
        protected virtual TemplateAttributeBase GetTemplateAttribute(Type type)
        {
            if (type.IsConstructedGenericType)
            {
                var genericTypeAttributes = type.GetTypeInfo().GetGenericArguments()[0].GetTypeInfo().GetCustomAttributes<GenericResourceIdentifierTemplateAttribute>();

                return genericTypeAttributes.First(attr => attr.ContainerType.TypeHandle.Equals(type.GetGenericTypeDefinition().TypeHandle));
            }

            return type.GetTypeInfo().GetCustomAttribute<IdentifierAttribute>();
        }

        private string GetIdentifierTemplate(Type type)
        {
            var templateAttribute = this.GetTemplateAttribute(type);
            if (templateAttribute == null)
            {
                throw new MissingTemplateException(type);
            }

            return templateAttribute.Template;
        }
    }
}
