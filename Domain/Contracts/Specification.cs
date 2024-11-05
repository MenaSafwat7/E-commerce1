using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public abstract class Specification<T> where T : class
    {
        protected Specification(Expression<Func<T,bool>>? criteria)
        {
            Criteria = criteria;
        }
        public Expression<Func<T,bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();
        public Expression<Func<T,object>>? orderBy { get; private set; }
        public Expression<Func<T,object>>? orderByDescending { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> expression)
            => IncludeExpressions.Add(expression);
        
        protected void SetOrderBy(Expression<Func<T, object>> expression)
            => orderBy = expression;
        protected void SetOrderByDescending(Expression<Func<T, object>> expression)
            => orderByDescending = expression;

        protected void ApplyPagination(int PageIndex, int PageSize)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
    }
}
