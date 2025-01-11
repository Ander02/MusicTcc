using Business.Features.Albums;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Grpc;

namespace GrpcService.Services
{
    public class AlbumService : Grpc.AlbumService.AlbumServiceBase
    {
        private readonly ILogger<AlbumService> logger;
        private readonly IMediator mediator;

        public AlbumService(ILogger<AlbumService> logger, IMediator mediator)
        {
            this.logger = logger;
            this.mediator = mediator;
        }

        public override async Task<AlbumResponse> CreateAlbum(CreateAlbumRequest request, ServerCallContext context)
        {
            var command = new Create.Command
            {
                Name = request.Name,
                ReleasedAt = request.ReleasedAt.ToDateTime(),
                Base64Image = request.Base64Image,
            };

            var result = await mediator.Send(command);

            return new AlbumResponse
            {
                Id = result.Id.ToString(),
                Name = result.Name,
                Base64Image = result.Base64Image,
                ReleasedAt = result.ReleasedAt.ToTimestamp(),
            };
        }
    }
}
