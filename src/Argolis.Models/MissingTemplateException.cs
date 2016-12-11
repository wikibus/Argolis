using System;

namespace Argolis.Models
{
    /// <summary>
    /// Exception thrown when template for the given resource type is not found
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class MissingTemplateException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingTemplateException"/> class.
        /// </summary>
        /// <param name="type">The resource type.</param>
        public MissingTemplateException(Type type)
            : base(GetMessage(type))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MissingTemplateException"/> class.
        /// </summary>
        /// <param name="type">The resource type.</param>
        /// <param name="exception">A more detailed exception.</param>
        public MissingTemplateException(Type type, Exception exception)
            : base(GetMessage(type), exception)
        {
        }

        private static string GetMessage(Type type)
        {
            return $"Template not found for type {type.FullName}";
        }
    }
}