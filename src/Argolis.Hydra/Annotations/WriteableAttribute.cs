using System;
using Argolis.Hydra.Core;

namespace Argolis.Hydra.Annotations
{
    /// <summary>
    /// Use to mark a <see cref="SupportedProperty"/>, which should
    /// not be <see cref="Vocab.Hydra.writeable"/>
    /// </summary>
    public class WriteableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WriteableAttribute"/> class.
        /// </summary>
        public WriteableAttribute(bool writeable = true) => this.Writeable = writeable;

        /// <summary>
        /// Gets a value indicating whether the property is writeable
        /// </summary>
        public bool Writeable { get; }
    }
}
