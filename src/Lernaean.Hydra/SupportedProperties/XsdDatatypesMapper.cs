using System;
using System.Collections.Generic;
using System.Reflection;
using Hydra.DocumentationDiscovery;
using NullGuard;
using Vocab;

namespace Hydra.SupportedProperties
{
    /// <summary>
    /// Maps a <see cref="PropertyInfo.PropertyType" /> to predefined XSD data types.
    /// </summary>
    public class XsdDatatypesMapper : IPropertyRangeMapper
    {
        private static readonly IDictionary<Type, Uri> Types;

        static XsdDatatypesMapper()
        {
            Types = new Dictionary<Type, Uri>
            {
                { typeof(int), new Uri(Xsd.@int) },
                { typeof(DateTime), new Uri(Xsd.dateTime) },
                { typeof(bool), new Uri(Xsd.boolean) },
                { typeof(string), new Uri(Xsd.@string) },
                { typeof(decimal), new Uri(Xsd.@decimal) },
                { typeof(double), new Uri(Xsd.@double) },
                { typeof(float), new Uri(Xsd.@float) },
                { typeof(TimeSpan), new Uri(Xsd.duration) },
                { typeof(byte), new Uri(Xsd.unsignedByte) },
                { typeof(sbyte), new Uri(Xsd.@byte) },
                { typeof(long), new Uri(Xsd.@long) },
                { typeof(ulong), new Uri(Xsd.unsignedLong) },
                { typeof(short), new Uri(Xsd.@short) },
                { typeof(ushort), new Uri(Xsd.unsignedShort) },
            };
        }

        /// <summary>
        /// Maps a <see cref="PropertyInfo.PropertyType" /> to predefined XSD data types.
        /// </summary>
        [return: AllowNull]
        public Uri MapType(PropertyInfo property, IReadOnlyDictionary<Type, Uri> classIds)
        {
            return GetMappedXsdTypeUri(property.PropertyType);
        }

        /// <summary>
        /// Gets the mapped class URI or null.
        /// </summary>
        protected Uri GetMappedXsdTypeUri(Type propertyType)
        {
            if (Types.ContainsKey(propertyType) == false)
            {
                return null;
            }

            return Types[propertyType];
        }
    }
}