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
        private IMusicLibraryRepository _repository;
        private IMapper _mapper;
        public AlbumWithReproductionsService(IAlbumsService albumService, ISongsService songsService, IMusicLibraryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AlbumWithReproductionsModel>> ChooseMostHearedAlbumsAsync()
        {
            var albumsEntities = await _repository.GetAllAlbumsAsync();
            var songsEntities = await _repository.GetAllSongsAsync();
            var albums = _mapper.Map< IEnumerable < AlbumModel > >( albumsEntities);
            var songs = _mapper.Map<IEnumerable<SongModel>>(songsEntities);
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
                    Reproductions = reproductions,
                    Description = album.Description,
                    Price = album.Price,
                    Popularity = album.Popularity
                };
                AlbumsWithReproduction.Add(albumWithReproductions);
            }
            return AlbumsWithReproduction.ToList().Count > 0 ? AlbumsWithReproduction.Take(10) : AlbumsWithReproduction;
        }
    }
}
