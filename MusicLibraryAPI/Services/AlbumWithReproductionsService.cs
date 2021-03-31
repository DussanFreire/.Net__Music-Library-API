using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public class AlbumWithReproductionsService: IAlbumsWithReproductionsService
    {
        private IAlbumsService _albumService;
        private ISongsService _songsService;
        public AlbumWithReproductionsService(IAlbumsService albumService, ISongsService songsService)
        {
            _albumService = albumService;
            _songsService = songsService;
        }
        public IEnumerable<AlbumWithReproductionsModel> ChooseMostHearedAlbums()
        {
            var albums = _albumService.GetAllAlbums();
            var songs = _songsService.GetAllSongs();
            long? reproductions = 0;
            List<AlbumWithReproductionsModel> AlbumsWithReproduction = new List<AlbumWithReproductionsModel>();
            foreach (AlbumModel album in albums)
            {
                reproductions = songs.Where(s => s.AlbumId == album.Id).Select(s => s.Reproductions).Sum();
                AlbumWithReproductionsModel albumWithReproductions = new AlbumWithReproductionsModel()
                {
                    Name = album.Name,
                    RecordIndustry = album.RecordIndustry,
                    ArtistId = album.ArtistId,
                    Likes = album.Likes,
                    PublicationDate = album.PublicationDate,
                    Id = album.Id,
                    Reproductions = reproductions
                };
                AlbumsWithReproduction.Add(albumWithReproductions);
            }
            return AlbumsWithReproduction.ToList().Count > 0 ? AlbumsWithReproduction.Take(10) : AlbumsWithReproduction;
        }
    }
}
