using Core;
using Data;

namespace GraphQLApi.Configuration;
public class Query
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Music> GetMusics([Service] MusicDbContext context) => context.Musics;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Album> GetAlbums([Service] MusicDbContext context) => context.Albums;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Artist> GetArtists([Service] MusicDbContext context) => context.Artists;

    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Genre> GetGenres([Service] MusicDbContext context) => context.Genres;
}