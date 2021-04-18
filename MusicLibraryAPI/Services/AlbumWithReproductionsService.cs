using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicLibraryAPI.Data.Repositories;
using MusicLibraryAPI.Data.Entities;
namespace MusicLibraryAPI.Services
{
    public class AlbumWithReproductionsService: IAlbumsWithReproductionsService
    {
        private IAlbumsService _albumService;
        private ISongsService _songsService;
        private IMusicLibraryRepository _repository;
        private IMapper _mapper;
        public AlbumWithReproductionsService(IAlbumsService albumService, ISongsService songsService, IMusicLibraryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _albumService = albumService;
            _songsService = songsService;
        }
        public IEnumerable<AlbumWithReproductionsModel> ChooseMostHearedAlbums()
        {
            var albums = _mapper.Map< IEnumerable < AlbumModel > >( _repository.GetAllAlbums());
            var songs = _mapper.Map<IEnumerable<SongModel>>(_repository.GetAllSongs());
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
