using System;
using JetBrains.Annotations;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;

namespace Hydra.Core
{
    /// <summary>
    /// A Hydra operation
    /// </summary>
    public class Operation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        public Operation(string method)
        {
            Method = method;
        }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        [JsonProperty(Hydra.method)]
        public string Method { get; private set; }

        /// <summary>
        /// Gets or sets the returned type.
        /// </summary>
        [AllowNull]
        [JsonProperty(Hydra.returns)]
        public IriRef Returns { get; set; }

        /// <summary>
        /// Gets or sets the expected type.
        /// </summary>
        [AllowNull]
        [JsonProperty(Hydra.expects)]
        public IriRef Expects { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Hydra.Operation; }
        }
    }
}
