using System;
using System.Collections.Generic;
using System.Reflection;

namespace Hydra.Discovery.SupportedOperations
{
    public interface ISupportedOperations
    {
        Type Type { get; }

        IEnumerable<OperationMeta> GetTypeOperations();

        IEnumerable<OperationMeta> GetPropertyOperations(PropertyInfo property);
    }
}