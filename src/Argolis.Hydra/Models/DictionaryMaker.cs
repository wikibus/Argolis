using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace Argolis.Hydra.Models
{
    /// <summary>
    /// Makes a dictionary out of a given object.
    /// </summary>
    internal static class DictionaryMaker
    {
        /// <summary>
        /// Makes a dictionary out of the properties of the given input.
        /// </summary>
        /// <param name="input">Object to make a dictionary out of</param>
        /// <returns>
        /// A dictionary with the keys being the input object property names and the values their respective values
        /// </returns>
        public static IDictionary<string, object> Make(object input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input is IDictionary<string, object> objects)
            {
                return objects;
            }

            var properties = input.GetType().GetTypeInfo().GetProperties();
            var fields = input.GetType().GetTypeInfo().GetFields();
            var members = properties.Cast<MemberInfo>().Concat(fields);

            return members.ToDictionary(m => m.Name, m => GetValue(input, m));
        }

        /// <summary>
        /// Makes a dictionary out of the properties of the given input.
        /// </summary>
        /// <param name="input">Object to make a dictionary out of</param>
        /// <returns>
        /// A dictionary with the keys being the input object property names and the values their respective types and
        /// values
        /// </returns>
        public static IDictionary<string, Tuple<Type, object>> MakeWithType(object input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input is IDictionary<string, object> objects)
            {
                return objects.ToDictionary(o => o.Key, o => o.Value == null
                    ? new Tuple<Type, object>(typeof(object), o.Value)
                    : new Tuple<Type, object>(o.Value.GetType(), o.Value));
            }

            var dict = new Dictionary<string, Tuple<Type, object>>();

            foreach (var property in input.GetType().GetTypeInfo().GetProperties())
            {
                dict.Add(property.Name, new Tuple<Type, object>(property.PropertyType, property.GetValue(input, null)));
            }

            foreach (var field in input.GetType().GetTypeInfo().GetFields())
            {
                dict.Add(field.Name, new Tuple<Type, object>(field.FieldType, field.GetValue(input)));
            }

            return dict;
        }

        [SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "It's necessary.")]
        private static object GetValue(object obj, MemberInfo member)
        {
            if (member is PropertyInfo propertyInfo)
            {
                return propertyInfo.GetValue(obj, null);
            }

            if (member is FieldInfo info)
            {
                return info.GetValue(obj);
            }

            throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
        }
    }
}
