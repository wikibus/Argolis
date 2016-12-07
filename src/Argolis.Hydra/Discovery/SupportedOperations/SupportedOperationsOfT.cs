using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JsonLD.Entities.Context;

namespace Argolis.Hydra.Discovery.SupportedOperations
{
#pragma warning disable SA1649 // File name must match first type name
                              /// <summary>
                              /// Base class for setting up operations supported by class <typeparamref name="T" />
                              /// </summary>
                              /// <typeparam name="T">the supported class type</typeparam>
    public abstract class SupportedOperations<T> : SupportedOperations
#pragma warning restore SA1649 // File name must match first type name
    {
        private readonly IDictionary<PropertyInfo, IList<OperationMeta>> propertyOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperations{T}"/> class.
        /// </summary>
        protected SupportedOperations()
            : base(typeof(T))
        {
            this.propertyOperations = new Dictionary<PropertyInfo, IList<OperationMeta>>();
        }

        /// <summary>
        /// Gets the supported operations for a supported property .
        /// </summary>
        /// <param name="property">The supported property.</param>
        public override IEnumerable<OperationMeta> GetSupportedPropertyOperations(PropertyInfo property)
        {
            if (this.propertyOperations.ContainsKey(property) == false)
            {
                return Enumerable.Empty<OperationMeta>();
            }

            return this.propertyOperations[property];
        }

        /// <summary>
        /// Allows the setup of an operation supported by a property
        /// </summary>
        /// <typeparam name="TReturn">Property return type.</typeparam>
        protected SupportedOperationBuilder Property<TReturn>(Expression<Func<T, TReturn>> propertyExpression)
        {
            var propertyInfo = propertyExpression.GetProperty();

            if (this.propertyOperations.ContainsKey(propertyInfo) == false)
            {
                this.propertyOperations[propertyInfo] = new List<OperationMeta>();
            }

            return new SupportedOperationBuilder(this.propertyOperations[propertyInfo]);
        }
    }
}
