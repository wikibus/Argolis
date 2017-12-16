using System;
using Argolis.Hydra.Resources;
using Argolis.Models;

namespace Argolis.Hydra.Models
{
    /// <summary>
    /// Allows defining an identifier template for Hydra:Collection resources
    /// </summary>
    /// <seealso cref="GenericResourceIdentifierTemplateAttribute" />
    public class CollectionIdentifierAttribute : GenericResourceIdentifierTemplateAttribute
    {
        private static readonly Type HydraCollectionType = typeof(Collection<>);

        /// <summary>
        /// Initializes a new instance of the <see cref="CollectionIdentifierAttribute"/> class.
        /// </summary>
        /// <param name="template">The template string.</param>
        public CollectionIdentifierAttribute(string template)
            : base(template, HydraCollectionType)
        {
        }
    }
}