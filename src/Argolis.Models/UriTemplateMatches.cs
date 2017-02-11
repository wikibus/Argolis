using System;
using System.Collections;
using System.Collections.Generic;
using NullGuard;

namespace Argolis.Models
{
    /// <summary>
    /// Wraps values extracted from a URI against matching URI Template
    /// </summary>
    public class UriTemplateMatches : IReadOnlyDictionary<string, object>
    {
        private readonly IDictionary<string, object> matchDict;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriTemplateMatches"/> class.
        /// </summary>
        /// <param name="matchDict">The match dictionary.</param>
        public UriTemplateMatches(IDictionary<string, object> matchDict)
            : this(true)
        {
            this.matchDict = matchDict;
        }

        private UriTemplateMatches(bool success)
        {
            this.Success = success;
            this.matchDict = new Dictionary<string, object>();
        }

        /// <summary>
        /// Gets a value indicating whether the URI matched the template
        /// </summary>
        public bool Success { get; private set; }

#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public int Count => this.matchDict.Count;

        public IEnumerable<string> Keys => this.matchDict.Keys;

        public IEnumerable<object> Values => this.matchDict.Values;

        public object this[string key] => this.matchDict[key];
#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

        /// <summary>
        /// Creates a instance which represents an unsuccessful match
        /// </summary>
        public static UriTemplateMatches Failure()
        {
            return new UriTemplateMatches(false);
        }

        /// <summary>
        /// Gets value of variable named as the <paramref name="key"/> parameter, converted to given type.
        /// </summary>
        /// <typeparam name="T">type to convert the variable value to</typeparam>
        public T Get<T>(string key)
        {
            var conversionType = typeof(T);
            if (typeof(T).IsConstructedGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                conversionType = Nullable.GetUnderlyingType(typeof(T));
            }

            return (T)Convert.ChangeType(this[key], conversionType);
        }

#pragma warning disable SA1600 // Elements must be documented
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public bool ContainsKey(string key)
        {
            return this.matchDict.ContainsKey(key);
        }

        public bool TryGetValue(string key, [AllowNull] out object value)
        {
            return this.matchDict.TryGetValue(key, out value);
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.matchDict.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this.matchDict).GetEnumerator();
        }
#pragma warning restore SA1600 // Elements must be documented
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}