using System;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Annotations
{
    /// <summary>
    /// Use to mark a <see cref="SupportedProperty"/>, which should
    /// not be <see cref="Vocab.Hydra.readable"/>
    /// </summary>
    public class ReadableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadableAttribute"/> class.
        /// </summary>
        public ReadableAttribute(bool readeble = true) => this.Readable = readeble;

        /// <summary>
        /// Gets a value indicating whether the property is writeable
        /// </summary>
        public bool Readable { get; }
    }
}
