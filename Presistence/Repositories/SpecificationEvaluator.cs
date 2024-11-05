using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence.Repositories
{
    internal static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(
            IQueryable<T> InputQuery,
            Specification<T> specification) where T : class

        {
            var query = InputQuery;
            if (specification.Criteria is not null) query = query.Where(specification.Criteria);
            query = specification.IncludeExpressions.Aggregate(
                query,
                (currentQuery, IncludeExpression) => currentQuery.Include(IncludeExpression));


            if(specification.orderBy is not null) 
                query = query.OrderBy(specification.orderBy);

            else if (specification.orderByDescending is not null)
                query = query.OrderByDescending(specification.orderByDescending);

            if (specification.IsPaginated)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;
        }

    }
}
