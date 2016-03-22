using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Base class for setting up operations supported by a class
    /// </summary>
    public abstract class SupportedOperations : ISupportedOperations
    {
        private readonly IList<OperationMeta> _typeOperations = new List<OperationMeta>();
        private readonly IDictionary<PropertyInfo, IList<OperationMeta>> _propertyOperations = new Dictionary<PropertyInfo, IList<OperationMeta>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperations"/> class.
        /// </summary>
        /// <param name="type">The supported class type.</param>
        protected SupportedOperations(Type type)
        {
            Type = type;
        }

        /// <inheritdoc />
        public Type Type { get; private set; }
        
        /// <inheritdoc />
        protected IDictionary<PropertyInfo, IList<OperationMeta>> PropertyOperations
        {
            get { return _propertyOperations; }
        }
        
        /// <inheritdoc />
        public IEnumerable<OperationMeta> GetSupportedClassOperations()
        {
            return _typeOperations;
        }
        
        /// <inheritdoc />
        public IEnumerable<OperationMeta> GetSupportedPropertyOperations(PropertyInfo property)
        {
            if (_propertyOperations.ContainsKey(property) == false)
            {
                return Enumerable.Empty<OperationMeta>();
            }

            return _propertyOperations[property];
        }

        /// <summary>
        /// Includes the GET operation in the supported class' supported operations
        /// </summary>
        protected void SupportsGet()
        {
            _typeOperations.Add(new OperationMeta
            {
                Method = "GET"
            });
        }
    }
}
