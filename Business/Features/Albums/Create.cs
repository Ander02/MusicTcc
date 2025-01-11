using Core;
using Data;
using FluentValidation;
using MediatR;

namespace Business.Features.Albums
{
    public class Create
    {
        public class Command : IRequest<Album>
        {
            public string Name { get; set; }

            public DateTime ReleasedAt { get; set; }
            public string Base64Image { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(d => d.Name).NotEmpty();
                RuleFor(d => d.ReleasedAt).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Album>
        {
            private readonly MusicDbContext dbContext;

            public Handler(MusicDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public async Task<Album> Handle(Command request, CancellationToken cancellationToken)
            {
                var album = new Album
                {
                    Name = request.Name,
                    ReleasedAt = request.ReleasedAt,
                    Base64Image = request.Base64Image
                };

                dbContext.Albums.Add(album);

                await dbContext.SaveChangesAsync(cancellationToken);

                return album;
            }
        }
    }
}
