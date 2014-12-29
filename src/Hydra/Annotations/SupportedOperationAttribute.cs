using System;
using NullGuard;

namespace Hydra.Annotations
{
    /// <summary>
    /// Marks a property available for invoking GET
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class AllowGetAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        public string Range { [return: AllowNull] get; set; }
    }
}
