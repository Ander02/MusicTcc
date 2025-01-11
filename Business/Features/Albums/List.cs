using Business.Models.Pagination;
using Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Features.Albums
{
    public class List
    {
        public class Query : PagedRequest<Result>
        {
            public string Search { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

        public class Result
        {
            public Guid Id { get; set; }
            public string Name { get; set; }

            public DateTime ReleasedAt { get; set; }
            public string Base64Image { get; set; }
        }


        public class Handler : IRequestHandler<Query, PagedResult<Result>>
        {
            private readonly MusicDbContext dbContext;

            public Handler(MusicDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<PagedResult<Result>> Handle(Query request, CancellationToken cancellationToken)
            {
                var query = dbContext.Albums.AsQueryable();


                if (!string.IsNullOrWhiteSpace(request.Search))
                {
                    query = query.Where(d => d.Name.Contains(request.Search));
                }

                if (request.StartDate.HasValue)
                {
                    query = query.Where(d => request.StartDate >= d.ReleasedAt);
                }

                if (request.EndDate.HasValue)
                {
                    query = query.Where(d => request.EndDate <= d.ReleasedAt);
                }

                var total = await query.CountAsync(cancellationToken);

                var results = await query.PaginateBy(request, d => d.Name)
                                         .Select(d => new Result
                                         {
                                             Id = d.Id,
                                             Name = d.Name,
                                             ReleasedAt = d.ReleasedAt,
                                             Base64Image = d.Base64Image,
                                         })
                                         .ToListAsync(cancellationToken);

                return request.GetResult(total, results);

            }
        }
    }
}
