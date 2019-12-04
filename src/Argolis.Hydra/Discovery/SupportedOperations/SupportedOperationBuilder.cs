using JsonLD.Entities;

namespace Argolis.Hydra.Discovery.SupportedOperations
{
    /// <summary>
    /// Builder to set up a supported operations
    /// </summary>
    public class SupportedOperationBuilder
    {
        private readonly OperationMeta meta;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedOperationBuilder"/> class.
        /// </summary>
        public SupportedOperationBuilder(OperationMeta meta)
        {
            this.meta = meta;
        }

        /// <summary>
        /// Sets operation's hydra:title
        /// </summary>
        public SupportedOperationBuilder Title(string title)
        {
            this.meta.Title = title;
            return this;
        }

        /// <summary>
        /// Sets operation hydra:description
        /// </summary>
        public SupportedOperationBuilder Description(string description)
        {
            this.meta.Description = description;
            return this;
        }

        /// <summary>
        /// Sets operation's hydra:expects
        /// </summary>
        public SupportedOperationBuilder Expects(IriRef payloadType)
        {
            this.meta.Expects = payloadType;
            return this;
        }

        /// <summary>
        /// Sets operation's hydra:returns
        /// </summary>
        public SupportedOperationBuilder Returns(IriRef returned)
        {
            this.meta.Returns = returned;
            return this;
        }
    }
}
