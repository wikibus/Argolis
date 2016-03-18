using System.Reflection;

namespace Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Contract for selecting properties to be included as Supported Properties
    /// </summary>
    public interface ISupportedPropertySelectionPolicy
    {
        /// <summary>
        /// Determines whether the the given <paramref name="property"/> should
        /// be included as hydra:SupportedClass.
        /// </summary>
        bool ShouldIncludeProperty(PropertyInfo property);
    }
}