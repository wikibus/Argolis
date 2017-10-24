using System;

namespace Hydra.Annotations
{
    /// <summary>
    /// Marks a property as a hydra:Link
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class LinkAttribute : Attribute
    {
    }
}