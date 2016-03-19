using System;

namespace Hydra.Discovery.SupportedClasses
{
    /// <summary>
    /// Default implementation of <see cref="ISupportedClassMetaProvider"/>
    /// </summary>
    public class DefaultSupportedClassMetaProvider : ISupportedClassMetaProvider
    {
        private const string DefaultDescription = "The {0} class";

        /// <summary>
        /// Gets the basic information about a supported class from it's reflected data and attributes.
        /// </summary>
        public virtual SupportedClassMeta GetMeta(Type type)
        {
            var title = type.IsGenericType ? GetCleanGenericName(type) : type.Name;
            var description = type.GetDescription() ?? string.Format(DefaultDescription, title);

            return new SupportedClassMeta
            {
                Title = title,
                Description = description
            };
        }

        private static string GetCleanGenericName(Type type)
        {
            return type.Name.Substring(0, type.Name.IndexOf('`'));
        }
    }
}
