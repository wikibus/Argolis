using System;
using Hydra.Core;

namespace Hydra.Annotations
{
    /// <summary>
    /// Marks <see cref="Type"/> to be included as hydra:Class in <see cref="ApiDocumentation"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class SupportedClassAttribute : Attribute
    {
    }
}