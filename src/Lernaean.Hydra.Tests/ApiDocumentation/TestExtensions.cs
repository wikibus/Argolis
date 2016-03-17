using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Lernaean.Hydra.Tests.ApiDocumentation
{
    internal static class TestExtensions
    {
        internal static PropertyInfo GetProp<T>(this object testClass, Expression<Func<T, object>> propertySelector)
        {
            return (PropertyInfo) ((MemberExpression) propertySelector.Body).Member;
        } 
    }
}