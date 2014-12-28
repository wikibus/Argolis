using System;
using System.Collections.Generic;

namespace Hydra
{
    /// <summary>
    /// Base class for Hydra API documentation
    /// </summary>
    public abstract class ApiDocumentation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiDocumentation"/> class.
        /// </summary>
        /// <param name="entrypoint">The entrypoint Uri.</param>
        protected ApiDocumentation(Uri entrypoint)
        {
            Entrypoint = entrypoint;
        }

        /// <summary>
        /// Gets the entrypoint Uri.
        /// </summary>
        public Uri Entrypoint { get; private set; }

        /// <summary>
        /// Gets the supported classes.
        /// </summary>
        [JsonProperty("supportedClass")]
        public virtual IEnumerable<Class> SupportedClasses
        {
            get { yield break; }
        }
    }
}
