using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Hydra.Core;
using Hydra.Discovery.SupportedClasses;
using Hydra.Discovery.SupportedProperties;
using JsonLD.Entities;
using TestHydraApi;

namespace TestNancyApp
{
    public class SupportedPropertyFactoryEx : ISupportedPropertyFactory
    {
        private readonly DefaultSupportedPropertyFactory _inner;
        private readonly IEnumerable<ISupportedOperations> _operations;

        public SupportedPropertyFactoryEx(DefaultSupportedPropertyFactory inner, IEnumerable<ISupportedOperations> operations)
        {
            _inner = inner;
            _operations = operations;
        }

        public SupportedProperty Create(PropertyInfo prop, IReadOnlyDictionary<Type, Uri> classIds)
        {
            var supportedProperty = _inner.Create(prop, classIds);

            var operations = from operation in _operations
                             where operation.Type == prop.ReflectedType
                             from meta in operation.GetPropertyOperations(prop)
                             select new Operation(meta.Method)
                             {
                                 Returns = supportedProperty.Property.Range
                             };
            supportedProperty.SupportedOperations = operations.ToList();

            return supportedProperty;
        }
    }

    public class SupportedClassFactoryEx : ISupportedClassFactory
    {
        private readonly DefaultSupportedClassFactory _inner;
        private readonly IEnumerable<ISupportedOperations> _operations;

        public SupportedClassFactoryEx(
            DefaultSupportedClassFactory inner,
            IEnumerable<ISupportedOperations> operations)
        {
            _inner = inner;
            _operations = operations;
        }

        public Class Create(Type supportedClassType, IReadOnlyDictionary<Type, Uri> classIds)
        {
            var supportedClass = _inner.Create(supportedClassType, classIds);

            var operations = from source in _operations
                             where source.Type == supportedClassType
                             from op in source.TypeOperations
                             select new Operation(op.Method)
                             {
                                 Returns = (IriRef)supportedClass.Id
                             };
            supportedClass.SupportedOperations = operations.ToList();

            return supportedClass;
        }
    }

    public class IssueSupportedOperations : SupportedOperations<Issue>
    {
        public IssueSupportedOperations()
        {
            CanGet();

            Property(issue => issue.ProjectId).CanGet();
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

    public abstract class SupportedOperations : ISupportedOperations
    {
        private readonly IList<OperationMeta> _typeOperations = new List<OperationMeta>();
        private readonly IDictionary<PropertyInfo, IList<OperationMeta>> _propertyOperations = new Dictionary<PropertyInfo, IList<OperationMeta>>();

        public Type Type { get; private set; }

        public IEnumerable<OperationMeta> TypeOperations
        {
            get { return _typeOperations; }
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

    public interface ISupportedOperations
    {
        Type Type { get; }

        IEnumerable<OperationMeta> TypeOperations { get; }

        IEnumerable<OperationMeta> GetPropertyOperations(PropertyInfo property);
    }

    public class OperationMeta
    {
        public string Method { get; set; }
    }

    public class SupportedOperationBuilder
    {
        private PropertyInfo _property;
        private readonly IList<OperationMeta> _propertyOperations;

        public SupportedOperationBuilder(PropertyInfo property, IList<OperationMeta> propertyOperations)
        {
            this._property = property;
            _propertyOperations = propertyOperations;
        }

        public SupportedOperationBuilder CanGet()
        {
            _propertyOperations.Add(new OperationMeta
            {
                Method = "GET"
            });

            return this;
        }
    }
}