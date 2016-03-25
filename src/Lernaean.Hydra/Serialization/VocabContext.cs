using Hydra.Discovery.SupportedProperties;
using Newtonsoft.Json.Linq;

namespace Hydra.Serialization
{
    /// <summary>
    /// Automatic @context used which constructs missing property mappings
    /// by appending property names to a base vocabulary URI
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    public class VocabContext<T> : AutoContextBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VocabContext{T}"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        public VocabContext(string baseUri) : base(new VocabularyStrategy(baseUri))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VocabContext{T}"/> class.
        /// </summary>
        /// <param name="context">The preexisting context.</param>
        /// <param name="baseUri">The base URI.</param>
        public VocabContext(JObject context, string baseUri)
            : base(context, new VocabularyStrategy(baseUri))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VocabContext{T}"/> class.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="selectionPolicy">The property selection policy.</param>
        public VocabContext(string baseUri, ISupportedPropertySelectionPolicy selectionPolicy)
            : base(new VocabularyStrategy(baseUri, selectionPolicy))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VocabContext{T}"/> class.
        /// </summary>
        /// <param name="context">The preexisting context.</param>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="selectionPolicy">The property selection policy.</param>
        public VocabContext(JObject context, string baseUri, ISupportedPropertySelectionPolicy selectionPolicy)
            : base(context, new VocabularyStrategy(baseUri, selectionPolicy))
        {
        }

        private class VocabularyStrategy : AutoContextStrategy
        {
            private readonly string _baseUri;

            public VocabularyStrategy(string baseUri)
                : this(baseUri, new DefaultPropertiesSelectionPolicy())
            {
            }

            public VocabularyStrategy(string baseUri, ISupportedPropertySelectionPolicy selectionPolicy)
                : base(selectionPolicy)
            {
                _baseUri = baseUri;
            }

            protected override string GetPropertyId(string propertyName)
            {
                return _baseUri + propertyName;
            }
        }
    }
}
