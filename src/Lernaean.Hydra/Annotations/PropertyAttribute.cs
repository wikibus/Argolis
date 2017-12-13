using System;

namespace Hydra.Annotations
{
    /// <summary>
    /// Annotates property identifier
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyAttribute"/> class.
        /// </summary>
        public PropertyAttribute(string id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Gets the property identifier
        /// </summary>
        public string Id { get; private set; }
    }
}
