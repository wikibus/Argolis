using System;
using Hydra.Core;

namespace Hydra.Annotations
{
    /// <summary>
    /// Use to mark a <see cref="SupportedProperty"/>, which should
    /// not be <see cref="Hydra.readable"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class WriteOnlyAttribute : Attribute
    {
    }
}
