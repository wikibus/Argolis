using System.Collections.Generic;
using System.Reflection;

namespace Hydra.Discovery.SupportedOperations
{
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