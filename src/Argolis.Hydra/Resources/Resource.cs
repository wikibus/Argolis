using System;
using System.ComponentModel;
using Argolis.Hydra.Annotations;
using NullGuard;

namespace Argolis.Hydra.Resources
{
    /// <summary>
    /// Any resource
    /// </summary>
    [NullGuard(ValidationFlags.AllPublic ^ ValidationFlags.Properties)]
    [SupportedClass(Vocab.Hydra.Resource)]
    [Description("The class of dereferenceable resources.")]
    public class Resource
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public Uri Id { get; set; }
    }
}
