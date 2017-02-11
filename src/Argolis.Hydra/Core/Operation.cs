using JetBrains.Annotations;
using JsonLD.Entities;
using Newtonsoft.Json;
using NullGuard;
using Vocab;

namespace Argolis.Hydra.Core
{
    /// <summary>
    /// A Hydra operation
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    public class Operation : Resource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Operation"/> class.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        public Operation(string method)
        {
            this.Method = method;
            this.Expects = (IriRef)Owl.Nothing;
            this.Returns = (IriRef)Owl.Nothing;
        }

        /// <summary>
        /// Gets or sets the Operation's identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets the HTTP method.
        /// </summary>
        [JsonProperty(Vocab.Hydra.method)]
        public string Method { get; private set; }

        /// <summary>
        /// Gets or sets the returned type.
        /// </summary>
        [AllowNull]
        [JsonProperty(Vocab.Hydra.returns)]
        public IriRef Returns { get; set; }

        /// <summary>
        /// Gets or sets the expected type.
        /// </summary>
        [AllowNull]
        [JsonProperty(Vocab.Hydra.expects)]
        public IriRef Expects { get; set; }

        [JsonProperty, UsedImplicitly]
        private string Type
        {
            get { return Vocab.Hydra.Operation; }
        }
    }
}
