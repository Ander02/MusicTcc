using Business.Features.Albums;
using Core;
using MediatR;

namespace GraphQLApi.Configuration.Mutations
{
    public class AlbumMutations
    {
        public Task<Album> CreateAlbumAsync(Create.Command command, [Service] IMediator mediator)
        {          
            return mediator.Send(command);
        }
    }
}
