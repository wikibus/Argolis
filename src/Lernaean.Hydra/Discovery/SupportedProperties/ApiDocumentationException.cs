using System;
using Hydra.Core;

namespace Hydra.Discovery.SupportedProperties
{
    /// <summary>
    /// Represents an error, which occurs while creating <see cref="ApiDocumentation"/>
    /// </summary>
    public class ApiDocumentationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentationException"/> class.
        /// </summary>
        public ApiDocumentationException(string message)
            : base(message)
        {
        }
    }
}
