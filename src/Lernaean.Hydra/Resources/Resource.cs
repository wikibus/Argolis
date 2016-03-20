using System;
using System.ComponentModel;
using Hydra.Annotations;
using NullGuard;

namespace Hydra.Resources
{
    /// <summary>
    /// Any resource
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    [SupportedClass(Hydra.Resource)]
    [Description("The class of dereferenceable resources.")]
    public class Resource
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }
    }
}
