using System;
using NullGuard;

namespace Hydra.Annotations
{
    /// <summary>
    /// Marks a Hydra supported property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [NullGuard(ValidationFlags.ReturnValues)]
    public class SupportedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedPropertyAttribute"/> class.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        public SupportedPropertyAttribute(string predicate)
        {
            Predicate = predicate;
        }

        /// <summary>
        /// Gets the predicate.
        /// </summary>
        public string Predicate { get; private set; }

        /// <summary>
        /// Gets or sets the range.
        /// </summary>
        public string Range { get; set; }
    }
}
