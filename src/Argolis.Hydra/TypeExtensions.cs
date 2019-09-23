using System;
using System.Reflection;
using Argolis.Hydra.Resources;
using JsonLD.Entities;
using NullGuard;

namespace Argolis.Hydra
{
    /// <summary>
    /// Extensions of <see cref="Type"/>
    /// </summary>
    public static class TypeExtensions
    {
        private static readonly Type CollectionType = typeof(Collection<>);

        /// <summary>
        /// Gets RDF type of collection member
        /// </summary>
        /// <typeparam name="TMember">Collection member type</typeparam>
        /// <returns>null if type has no clear annotation</returns>
        [return: AllowNull]
        public static Uri GetMemberRdfType<TMember>(this Collection<TMember> collection)
        {
            var type = typeof(TMember);

            if (type.GetProperty("Type", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static) != null)
            {
                return type.GetTypeIdentifier();
            }

            return null;
        }
    }
}