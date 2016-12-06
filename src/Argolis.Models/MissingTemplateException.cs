using System;

namespace Argolis.Models
{
    /// <summary>
    /// Exception thrown when template for the given resource type is not found
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class MissingTemplateException : Exception
    {
        private readonly Type type;

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingTemplateException"/> class.
        /// </summary>
        /// <param name="type">The resource type.</param>
        public MissingTemplateException(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets a message that describes the current exception.
        /// </summary>
        public override string Message => $"Template not found for type {this.type.FullName}";
    }
}