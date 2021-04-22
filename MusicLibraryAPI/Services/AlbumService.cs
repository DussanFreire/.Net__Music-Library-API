
using MusicLibraryAPI.Exceptions;
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
    public class AlbumService : IAlbumsService
    {
        private IArtistsService _artistsService;
        private HashSet<string> _allowedTopValues = new HashSet<string>()
        {
            "likes",
            "price",
            "popularity"
        };
        private IMusicLibraryRepository _repository;
        private IMapper _mapper;
        public AlbumService(IArtistsService artistsService, IMusicLibraryRepository repository, IMapper mapper)
        {
            _artistsService = artistsService;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<AlbumModel> CreateAlbumAsync(long artistId, AlbumModel newAlbum)
        {
            await ValidateArtistAsync(artistId);
            newAlbum.ArtistId = artistId;
            var albumEntity = _mapper.Map<AlbumEntity>(newAlbum);

            _repository.CreateAlbum(artistId, albumEntity);

            var result = await _repository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
            return _mapper.Map<AlbumModel>(albumEntity);
        }

        public async Task<bool> DeleteAlbumAsync(long artistId, long albumId)
        {
            await ValidateAlbumAsync(artistId, albumId);

            await _repository.DeleteAlbumAsync(artistId, albumId);

            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            return true;
        }

        public async Task<AlbumModel> GetAlbumAsync(long artistId, long albumId)
        {
            await ValidateArtistAsync(artistId);
            var albumEntity = await _repository.GetAlbumAsync(artistId, albumId);
            if (albumEntity == null)
            {
                throw new NotFoundItemException($"The player with id: {albumId} does not exist in team with id:{artistId}.");
            }

            var albumModel = _mapper.Map<AlbumModel>(albumEntity);

            albumModel.ArtistId = artistId;
            return albumModel;
        }

        public async Task<IEnumerable<AlbumModel>> GetAlbumsAsync(long artistId)
        {
            await ValidateArtistAsync(artistId);
            var albums = await _repository.GetAlbumsAsync(artistId);
            return _mapper.Map<IEnumerable<AlbumModel>>(albums);
        }

        public async Task<IEnumerable<AlbumModel>> GetAllAlbumsAsync()
        {
            var albums = await _repository.GetAllAlbumsAsync();
            return _mapper.Map<IEnumerable<AlbumModel>>(albums);
        }

        public async Task<AlbumModel> UpdateAlbumAsync(long artistId, long albumId, AlbumModel updatedAlbum)
        {
            await ValidateArtistAsync(artistId);
            var albUp = await _repository.UpdateAlbumAsync(artistId, albumId, _mapper.Map<AlbumEntity>(updatedAlbum));
            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            return _mapper.Map<AlbumModel>(albUp);
        }
        public async Task<IEnumerable<AlbumModel>> BestAlbumsAsync()
        {
            var alb = await GetAllAlbumsAsync();
            var albums = alb.OrderByDescending(a => a.Popularity);
            var albums2 = new List<AlbumModel>();
            var albumsGroups = albums.GroupBy(a => a.ArtistId);
            ///var albumsGroups = albums.GroupBy(a => a.PublicationDate.Value.Year- a.PublicationDate.Value.Year%10);
            foreach (var albs in albumsGroups)
            {
                albums2.Add(albs.FirstOrDefault());
            }
            return albums2.OrderBy(a => a.ArtistId);///albums2.OrderBy(a=>a.PublicationDate.Value.Year);
        }
        public async Task<IEnumerable<AlbumModel>> GetTopAsync(string value = "", int n = 5, bool isDescending = false)
        {
            if (!_allowedTopValues.Contains(value.ToLower()))
                throw new InvalidOperationItemException($"The value: {value} is invalid, please use one of {String.Join(',', _allowedTopValues.ToArray())}");
            var albumsR = await _repository.GetTopAsync(value, n, isDescending);
            var albums = _mapper.Map<IEnumerable<AlbumModel>>(albumsR);
            return albums;
        }
        private async Task ValidateAlbumAsync(long artistId,long albumId)
        {
            var album= await GetAlbumAsync(artistId,albumId);
        }
        private async Task ValidateArtistAsync(long artistId)
        {
            var artist = await _repository.GetArtistAsync(artistId);
            if (artist == null)
            {
                throw new NotFoundItemException($"The artist with id: {artistId} doesn't exists.");
            }
        }
    }
}
