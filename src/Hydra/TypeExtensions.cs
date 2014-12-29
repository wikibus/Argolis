using System;
using System.Linq;
using System.Reflection;
using Hydra.Annotations;

namespace Hydra
{
    /// <summary>
    /// Helpers to build API Documentation
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Creates a <see cref="Class"/> for a <see cref="Type"/>
        /// </summary>
        public static Class ToClass(this Type type)
        {
            var result = new Class(type.GetIdentifier());

            result.SupportedProperties = (from property in type.GetProperties()
                                          where property.GetAttribute<SupportedPropertyAttribute>() != null
                                          select CreateProperty(property)).ToList();

            return result;
        }

        private static string GetBaseUri(string ns)
        {
            switch (ns)
            {
                case "wikibus.nancy.Hydra":
                    return "api:";
                case "wikibus.nancy":
                    return "http://wikibus.org/api#";
                case "wikibus.sources":
                    return "http://wikibus.org/ontology#";
            }

            throw new ArgumentException(string.Format("Unknown namespace {0}", ns));
        }

        private static Property CreateProperty(PropertyInfo property)
        {
            var attribute = property.GetAttribute<SupportedPropertyAttribute>();
            var getOperation = property.GetAttribute<AllowGetAttribute>();

            var hydraProperty = new Property();

            hydraProperty.Predicate = attribute.GetPredicate();
            hydraProperty.Range = attribute.Range;
            if (getOperation != null)
            {
                hydraProperty.SupportedOperations.Add(new Operation
                  {
                      Method = "GET",
                      Returns = attribute.Range ?? getOperation.Range
                  });
            }

            return hydraProperty;
        }

        private static T GetAttribute<T>(this PropertyInfo property)
        {
            return (T)property.GetCustomAttributes(typeof(T), true).SingleOrDefault();
        }

        private static string GetIdentifier(this Type type)
        {
            return GetBaseUri(type.Namespace) + type.Name;
        }
    }
}
