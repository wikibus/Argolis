using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Hydra.Discovery.SupportedOperations
{
    public abstract class SupportedOperations : ISupportedOperations
    {
        private readonly IList<OperationMeta> _typeOperations = new List<OperationMeta>();
        private readonly IDictionary<PropertyInfo, IList<OperationMeta>> _propertyOperations = new Dictionary<PropertyInfo, IList<OperationMeta>>();

        public Type Type { get; private set; }

        public IEnumerable<OperationMeta> GetTypeOperations()
        {
            return _typeOperations;
        }

        protected IDictionary<PropertyInfo, IList<OperationMeta>> PropertyOperations
        {
            get { return _propertyOperations; }
        }

        public IEnumerable<OperationMeta> GetPropertyOperations(PropertyInfo property)
        {
            if (_propertyOperations.ContainsKey(property) == false)
            {
                return Enumerable.Empty<OperationMeta>();
            }

            return _propertyOperations[property];
        }

        protected SupportedOperations(Type type)
        {
            Type = type;
        }

        protected void CanGet()
        {
            _typeOperations.Add(new OperationMeta
            {
                Method = "GET"
            });
        }
    }

    public abstract class SupportedOperations<T> : SupportedOperations
    {
        protected SupportedOperations() : base(typeof(T))
        {
        }

        protected SupportedOperationBuilder Property(Expression<Func<T, object>> propertyExpression)
        {
            PropertyInfo propertyInfo = (PropertyInfo)((MemberExpression)propertyExpression.Body).Member;

            if (PropertyOperations.ContainsKey(propertyInfo) == false)
            {
                PropertyOperations[propertyInfo] = new List<OperationMeta>();
            }

            return new SupportedOperationBuilder(propertyInfo, PropertyOperations[propertyInfo]);
        }
    }
}