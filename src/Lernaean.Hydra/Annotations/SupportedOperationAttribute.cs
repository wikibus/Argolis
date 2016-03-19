using System;

namespace Hydra.Annotations
{
    /// <summary>
    /// Marks a property available for invoking GET
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RangeAttribute"/> class.
        /// </summary>
        /// <param name="range">The range predicate.</param>
        public RangeAttribute(string range)
        {
            Range = range;
        }

        /// <summary>
        /// Gets the range.
        /// </summary>
        public string Range { get; private set; }
    }
}
