using System;
using Hydra.Core;

namespace Hydra.Annotations
{
    /// <summary>
    /// Defines the variable to be used with an <see cref="IriTemplateMapping"/>
    /// </summary>
    public class VariableAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariableAttribute"/> class.
        /// </summary>
        public VariableAttribute(string variable)
        {
            this.Variable = variable;
        }

        /// <summary>
        /// Gets the variable name
        /// </summary>
        public string Variable { get; }
    }
}