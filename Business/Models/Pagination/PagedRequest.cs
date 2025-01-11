using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models.Pagination
{
    public class PagedRequest<TResponse> : IRequest<PagedResult<TResponse>> where TResponse : class
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 30;

        public string Field { get; set; }
        public SortDirection? SortDirection { get; set; }

        internal bool HasSortingData => Field != null && SortDirection.HasValue;

        public PagedResult<TResponse> GetResult(int total, ICollection<TResponse> results) => new PagedResult<TResponse>(this, total, results);
    }

    public class PagedRequestValidator<TQuery, TResult> : AbstractValidator<TQuery> where TQuery : PagedRequest<TResult>
                                                                                    where TResult : class
    {
        public PagedRequestValidator()
        {
            RuleFor(d => d.Page).GreaterThanOrEqualTo(0);
            RuleFor(d => d.PageSize).GreaterThanOrEqualTo(0);
        }
    }
}
