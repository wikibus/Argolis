using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Base class for setting up operations supported by class <typeparamref name="T" />
    /// </summary>
    /// <typeparam name="T">the supported class type</typeparam>
    public abstract class SupportedOperations<T> : SupportedOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperations{T}"/> class.
        /// </summary>
        protected SupportedOperations() : base(typeof(T))
        {
        }

        /// <summary>
        /// Allows the setup of an operation supported by a property
        /// </summary>
        /// <typeparam name="TReturn">Property return type.</typeparam>
        protected SupportedOperationBuilder Property<TReturn>(Expression<Func<T, TReturn>> propertyExpression)
        {
            if (!(propertyExpression.Body is MemberExpression))
            {
                throw new ArgumentException("Parameter must be a property access expression", nameof(propertyExpression));
            }

            var memberExpression = (MemberExpression)propertyExpression.Body;
            var propertyInfo = (PropertyInfo)memberExpression.Member;

            if (PropertyOperations.ContainsKey(propertyInfo) == false)
            {
                PropertyOperations[propertyInfo] = new List<OperationMeta>();
            }

            return new SupportedOperationBuilder(PropertyOperations[propertyInfo]);
        }
    }
}
