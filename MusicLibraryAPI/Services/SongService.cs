using MusicLibraryAPI.Controllers;
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
    public class SongService : ISongsService
    {
        private IMusicLibraryRepository _repository;
        private IMapper _mapper;
        private HashSet<string> _allowedUpdatesToReproductions = new HashSet<string>()
        {
            "the song was played",
        };

        private HashSet<string> _allowedOrderByValues = new HashSet<string>()
        {
            "id",
            "name",
            "reproductions",
        };

        private HashSet<string> _allowedFilters = new HashSet<string>()
        {
            "allsongs",
            "top3",
            "top10",
            "top50",
            "blues",
            "cumbia",
            "rock",
            "mostplayedsong",
        };

        public SongService(IMusicLibraryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<SongModel> CreateSongAsync(long albumId, SongModel newSong, long artistId)
        {    
            await ValidateAlbumAsync(albumId, artistId);
            newSong.AlbumId = albumId;
            var songEntity = _mapper.Map<SongEntity>(newSong);

            _repository.CreateSong(albumId, songEntity, artistId);

            var result = await _repository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
            return _mapper.Map<SongModel>(songEntity);
        }

        public async Task<bool> DeleteSongAsync(long albumId, long songId, long artistId)
        {

            await ValidateSongAsync(artistId, albumId, songId);

            await _repository.DeleteSongAsync(albumId,songId,artistId);

            var result = await _repository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
            return true;
        }

        public async Task<SongModel> GetSongAsync(long albumId, long songId, long artistId)
        {
            await ValidateAlbumAsync(albumId, artistId);
            var songEntity = await _repository.GetSongAsync(albumId,songId,artistId);
            if (songEntity == null)
            {
                throw new NotFoundItemException($"The song with id: {songId} does not exist in album with id:{albumId}.");
            }
            var songModel = _mapper.Map<SongModel>(songEntity);

            songModel.AlbumId = albumId;
            return songModel;
        }
        

        private IEnumerable<SongModel> OrderSongsAsync(string orderBy, IEnumerable<SongModel> songsFiltered)
        {
            switch (orderBy.ToLower())
            {
                case "name":
                    return songsFiltered.OrderBy(t => t.Name);
                case "reproductions":
                    return songsFiltered.OrderByDescending(t => t.Reproductions);
                default:
                    return songsFiltered.OrderBy(t => t.Id);
            }
        }

        private IEnumerable<SongModel> FilterSongsAsync(string filter, IEnumerable<SongModel> albumSongs)
        {
            int numberOfElements = albumSongs.ToList().Count;
            switch (filter.ToLower())
            {
                case "top3":
                    return numberOfElements >= 3 ? albumSongs.OrderBy(t => t.Reproductions).Take(3) : albumSongs;
                case "top10":
                    return numberOfElements >= 10 ? albumSongs.OrderBy(t => t.Reproductions).Take(10) : albumSongs;
                case "top50":
                    return numberOfElements >= 50 ? albumSongs.OrderBy(t => t.Reproductions).Take(50) : albumSongs;
                case "rock":
                    return albumSongs.Where(t => t.Genres == "Rock");
                case "cumbia":
                    return albumSongs.Where(t => t.Genres == "Cumbia");
                case "blues":
                    return albumSongs.Where(t => t.Genres == "Blues");
                case "mostplayedsong":
                    return numberOfElements >= 1 ? albumSongs.OrderBy(t => t.Reproductions).Take(1) : albumSongs;
                default:
                    return albumSongs;
            }
        }

        public async Task<SongModel> UpdateSongAsync(long albumId, long songId, SongModel updatedSong, long artistId)
        {
            await ValidateAlbumAsync(albumId, artistId);
            var songUp = await _repository.UpdateSongAsync(albumId, songId, _mapper.Map<SongEntity>(updatedSong),artistId);
            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            var song = _mapper.Map<SongModel>(songUp);
            song.AlbumId = albumId;
            return song;
        }

        private async Task ValidateAlbumAsync(long albumId, long artistId)
        {
            var album = await _repository.GetAlbumAsync(artistId,albumId);
            if (album == null)
            {
                throw new NotFoundItemException($"The album with id: {albumId} does not exist in artist with id:{artistId}.");
            }
        }

        public async Task<SongModel> UpdateReproductionsAsync(long albumId, long songId, Models.ActionForModels action, long artistId)
        {
            if (!_allowedUpdatesToReproductions.Contains(action.Action.ToLower()))
                throw new InvalidOperationItemException($"The update: {action.Action} is invalid");
            var songR = await _repository.UpdateReproductionsAsync(albumId, songId, action, artistId);
            var result = await _repository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
            var song = _mapper.Map<SongModel>(songR);
            return song;
        }
        public async Task<IEnumerable<SongModel>> GetSongsAsync(long albumId, long artistId, string orderBy = "id", string filter = "allSongs")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            if (!_allowedFilters.Contains(filter.ToLower()))
                throw new InvalidOperationItemException($"The filter value: {filter} is invalid, please use one of {String.Join(',', _allowedFilters.ToArray())}");

            await ValidateAlbumAsync(albumId,artistId);
            var songsEntity = await _repository.GetSongsAsync(albumId, artistId, orderBy, filter);
            IEnumerable<SongModel> albumSongs = _mapper.Map<IEnumerable<SongModel>>(songsEntity);
            var songsFiltered = FilterSongsAsync(filter, albumSongs);
            var songsOrdered = OrderSongsAsync(orderBy, songsFiltered);
            return songsOrdered;
        }
        public async Task<IEnumerable<SongModel>> GetMostPlayedSongsAsync(string orderBy = "reproductions", string filter = "top10")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            if (!_allowedFilters.Contains(filter.ToLower()))
                throw new InvalidOperationItemException($"The filter value: {filter} is invalid, please use one of {String.Join(',', _allowedFilters.ToArray())}");
            var songs = await GetAllSongsAsync();
            var songsFiltered = FilterSongsAsync(filter, songs);
            var songsOrdered = OrderSongsAsync(orderBy, songsFiltered);
            return songsOrdered;
        }
        public async Task<IEnumerable<SongModel>> GetAllSongsAsync()
        { 
            var songsEntity = await _repository.GetAllSongsAsync();
     
            var songsModel = _mapper.Map<IEnumerable<SongModel>>(songsEntity);

            return songsModel;
        }

        public async Task ValidateSongAsync(long artistId, long albumId,long songId)
        {
            var song = await GetSongAsync(albumId, songId, artistId);
        }
    }
}
