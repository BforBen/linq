using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;

namespace GuildfordBoroughCouncil.Linq
{
    public static partial class ExpressionHelper
    {
        public static Expression<Func<TEntity, TResult>> GetMember<TEntity, TResult>(String memberName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity), "p");
            MemberExpression member = Expression.MakeMemberAccess(parameter, typeof(TEntity).GetMember(memberName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Single());
            Expression<Func<TEntity, TResult>> expression = Expression.Lambda<Func<TEntity, TResult>>(member, parameter);
            return (expression);

        }
    }

    public static partial class ObjectExtensions
    {
        public static IDictionary<string, object> ToDictionary(this object data)
        {
            if (data == null) return null; // Or throw an ArgumentNullException if you want

            BindingFlags publicAttributes = BindingFlags.Public | BindingFlags.Instance;
            Dictionary<string, object> dictionary = new Dictionary<string, object>();

            foreach (PropertyInfo property in data.GetType().GetProperties(publicAttributes))
            {
                if (property.CanRead)
                {
                    dictionary.Add(property.Name, property.GetValue(data, null));
                }
            }
            return dictionary;
        }
    }
}
