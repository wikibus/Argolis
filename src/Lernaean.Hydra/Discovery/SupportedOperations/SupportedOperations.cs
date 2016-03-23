using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Base class for setting up operations supported by a class
    /// </summary>
    public abstract class SupportedOperations : ISupportedOperations
    {
        private readonly Type _type;
        private readonly SupportedOperationBuilder _builder;
        private readonly List<OperationMeta> _typeOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperations"/> class.
        /// </summary>
        /// <param name="type">The supported class type.</param>
        protected SupportedOperations(Type type)
        {
            _type = type;
            _typeOperations = new List<OperationMeta>();
            _builder = new SupportedOperationBuilder(_typeOperations);
        }

        /// <summary>
        /// Gets the type, which these operations apply to
        /// </summary>
        public Type Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets a builder, which sets up operations supported by the supported class
        /// </summary>
        public SupportedOperationBuilder Class
        {
            get
            {
                return _builder;
            }
        }

        /// <summary>
        /// Gets the supported operations for a supported class.
        /// </summary>
        public IEnumerable<OperationMeta> GetSupportedClassOperations()
        {
            return _typeOperations;
        }

        /// <summary>
        /// Gets the supported operations for a supported property .
        /// </summary>
        /// <param name="property">The supported property.</param>
        public abstract IEnumerable<OperationMeta> GetSupportedPropertyOperations(PropertyInfo property);
    }
}
