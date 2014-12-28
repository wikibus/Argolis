using System;
using NullGuard;

namespace Hydra.Annotations
{
    /// <summary>
    /// Marks a property available for invoking GET
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [NullGuard(ValidationFlags.ReturnValues)]
    public class AllowGetAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        public string Range { get; set; }
    }
}
