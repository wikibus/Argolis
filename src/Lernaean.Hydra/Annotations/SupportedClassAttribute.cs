using System;
using Hydra.Core;

namespace Hydra.Annotations
{
    /// <summary>
    /// Marks <see cref="Type"/> to be included as hydra:Class in <see cref="ApiDocumentation"/>
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct)]
    public class SupportedClassAttribute : Attribute
    {
        private Uri _rdfClassId;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupportedClassAttribute" /> class.
        /// </summary>
        /// <param name="rdfClassId">The RDF class identifier.</param>
        public SupportedClassAttribute(string rdfClassId)
        {
            _rdfClassId = new Uri(rdfClassId, UriKind.Absolute);
        }
    }
}
