using System;
using Hydra.Discovery.SupportedProperties;
using Newtonsoft.Json.Linq;

namespace Hydra.Serialization
{
    /// <summary>
    /// Automatic @context based on properties and class identifier
    /// </summary>
    /// <typeparam name="T">Model type</typeparam>
    /// <seealso cref="Newtonsoft.Json.Linq.JObject" />
    public class AutoContext<T> : AutoContextBase<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoContext{T}"/> class.
        /// </summary>
        /// <param name="classId">The class identifier.</param>
        public AutoContext(Uri classId)
            : base(new ClassNameStrategy(classId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoContext{T}"/> class
        /// by extending definitions from <paramref name="context"/>
        /// </summary>
        /// <param name="context">The current @context.</param>
        /// <param name="classId">The class identifier.</param>
        public AutoContext(JObject context, Uri classId)
            : base(context, new ClassNameStrategy(classId))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoContext{T}"/> class.
        /// </summary>
        /// <param name="classId">The class identifier.</param>
        /// <param name="selectionPolicy">The property selection policy.</param>
        public AutoContext(Uri classId, ISupportedPropertySelectionPolicy selectionPolicy)
            : base(new ClassNameStrategy(classId, selectionPolicy))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoContext{T}"/> class.
        /// </summary>
        /// <param name="context">The current @context.</param>
        /// <param name="classId">The class identifier.</param>
        /// <param name="selectionPolicy">The property selection policy.</param>
        public AutoContext(JObject context, Uri classId, ISupportedPropertySelectionPolicy selectionPolicy)
            : base(context, new ClassNameStrategy(classId, selectionPolicy))
        {
        }

        private class ClassNameStrategy : AutoContextStrategy
        {
            private const string SlashClassIdAppendFormat = "{0}#{1}";
            private const string HashClassIdAppendFormat = "{0}/{1}";
            private readonly Uri _classId;

            public ClassNameStrategy(Uri classId, ISupportedPropertySelectionPolicy selectionPolicy) : base(selectionPolicy)
            {
                _classId = classId;
            }

            public ClassNameStrategy(Uri classId) : this(classId, new DefaultPropertiesSelectionPolicy())
            {
            }

            protected override string GetPropertyId(string propertyName)
            {
                var format = HashClassIdAppendFormat;

                if (string.IsNullOrWhiteSpace(_classId.Fragment))
                {
                    format = SlashClassIdAppendFormat;
                }

                return string.Format(format, _classId, propertyName);
            }
        }
    }
}
