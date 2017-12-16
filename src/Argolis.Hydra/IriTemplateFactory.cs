using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Argolis.Hydra.Annotations;
using Argolis.Hydra.Core;
using Argolis.Hydra.Discovery.SupportedProperties;
using Argolis.Models;
using UriTemplateString.Spec;

namespace Argolis.Hydra
{
    /// <inheritdoc />
    public class IriTemplateFactory : IIriTemplateFactory
    {
        private readonly IPropertyRangeRetrievalPolicy propertyRange;
        private readonly IModelTemplateProvider modelTemplateProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="IriTemplateFactory"/> class.
        /// </summary>
        public IriTemplateFactory(
            IPropertyRangeRetrievalPolicy propertyRange,
            IModelTemplateProvider modelTemplateProvider)
        {
            this.propertyRange = propertyRange;
            this.modelTemplateProvider = modelTemplateProvider;
        }

        /// <inheritdoc />
        public IriTemplate CreateIriTemplate<T, T2>()
            where T : ITemplateParameters<T2>
        {
            var mappings = typeof(T).GetProperties().Select(this.CreateMapping).ToList();
            var path = this.modelTemplateProvider.GetAbsoluteTemplate(typeof(T2));
            var template = this.BuildTemplate(path, mappings);

            return new IriTemplate
            {
                Mappings = mappings,
                Template = template
            };
        }

        private string BuildTemplate(string path, IEnumerable<IriTemplateMapping> mappings)
        {
            var template = new UriTemplateString.UriTemplateString(path);
            var existingVariables = template.Parts
                .OfType<Expression>()
                .SelectMany(ex => ex.VariableList)
                .Select(vs => vs.Name)
                .Distinct()
                .ToArray();

            foreach (var mapping in mappings)
            {
                if (existingVariables.Contains(mapping.Variable))
                {
                    continue;
                }

                template = template.AppendQueryParam(mapping.Variable);
            }

            return template.ToString();
        }

        private IriTemplateMapping CreateMapping(PropertyInfo p)
        {
            var variable = p.GetCustomAttribute<VariableAttribute>()?.Variable;

            var iriTemplateMapping = new IriTemplateMapping
            {
                Required = p.GetCustomAttribute<RequiredAttribute>() != null,
                Variable = variable ?? p.Name
            };

            var property = this.CreateProperty(p);
            if (property != null)
            {
                iriTemplateMapping.Property = property;
            }

            return iriTemplateMapping;
        }

        private Property CreateProperty(PropertyInfo propertyInfo)
        {
            var property = new Lazy<Property>(() => new Property());
            var propertyAttribute = propertyInfo.GetCustomAttribute<PropertyAttribute>();

            if (propertyAttribute != null)
            {
                property.Value.Id = propertyAttribute.Id;
            }

            var range = this.propertyRange.GetRange(propertyInfo, new Dictionary<Type, Uri>());
            if (range != null)
            {
                property.Value.Range = range.Value;
            }

            if (property.IsValueCreated)
            {
                return property.Value;
            }

            return null;
        }
    }
}