using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NetSort.Sorting
{
    internal abstract class BaseQueryableSorter<TIn, TOut, TType> : BaseSorter<TIn, TOut, TType>
        where TIn : IQueryable<TType>
        where TOut : IOrderedQueryable<TType>
        where TType : class
    {
        protected TOut SortByExpression(TIn items, IEnumerable<SortOperationMetadata> metadata, string methodName)
        {
            var parameter = Expression.Parameter(typeof(TType), "t");

            Expression body = parameter;
            foreach (var part in metadata)
            {
                body = Expression.PropertyOrField(body, part.ToSortBy.Name);
            }
                    
            var orderExpression = Expression.Lambda(body, parameter);
            var propType = metadata.Last().ToSortBy.PropertyType;

            var expression = Expression.Call(
                typeof(Queryable), 
                methodName, 
                new Type[] { typeof(TType), propType }, 
                items.Expression, 
                Expression.Quote(orderExpression));

            return (TOut)items.Provider.CreateQuery<TType>(expression);
        }
    }
}
