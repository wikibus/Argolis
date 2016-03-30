using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JsonLD.Entities.Context;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Base class for setting up operations supported by class <typeparamref name="T" />
    /// </summary>
    /// <typeparam name="T">the supported class type</typeparam>
    public abstract class SupportedOperations<T> : SupportedOperations
    {
        private readonly IDictionary<PropertyInfo, IList<OperationMeta>> _propertyOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperations{T}"/> class.
        /// </summary>
        protected SupportedOperations() : base(typeof(T))
        {
            _propertyOperations = new Dictionary<PropertyInfo, IList<OperationMeta>>();
        }

        /// <summary>
        /// Gets the supported operations for a supported property .
        /// </summary>
        /// <param name="property">The supported property.</param>
        public override IEnumerable<OperationMeta> GetSupportedPropertyOperations(PropertyInfo property)
        {
            if (_propertyOperations.ContainsKey(property) == false)
            {
                return Enumerable.Empty<OperationMeta>();
            }

            return _propertyOperations[property];
        }

        /// <summary>
        /// Allows the setup of an operation supported by a property
        /// </summary>
        /// <typeparam name="TReturn">Property return type.</typeparam>
        protected SupportedOperationBuilder Property<TReturn>(Expression<Func<T, TReturn>> propertyExpression)
        {
            var propertyInfo = propertyExpression.GetProperty();

            if (_propertyOperations.ContainsKey(propertyInfo) == false)
            {
                _propertyOperations[propertyInfo] = new List<OperationMeta>();
            }

            return new SupportedOperationBuilder(_propertyOperations[propertyInfo]);
        }
    }
}
