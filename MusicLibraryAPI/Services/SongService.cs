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
        public SongModel CreateSong(long albumId, SongModel newSong, long artistId)
        {
            ValidateAlbum(albumId, artistId);
            var newS = _mapper.Map<SongModel>(_repository.CreateSong(albumId, _mapper.Map<SongEntity>(newSong), artistId));
            ///newSong.AlbumId = albumId;
            ///var nextId = _songs.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
            ///newSong.Id = nextId;
            ///_songs.Add(newSong);
            return newS;
        }

        public bool DeleteSong(long albumId, long songId, long artistId)
        {
            var songToDelete = GetSong(albumId, songId, artistId);
            ///_songs.Remove(songToDelete);
            return _repository.DeleteSong(albumId,songId,artistId);
        }

        public SongModel GetSong(long albumId, long songId, long artistId)
        {
            ValidateAlbum(albumId, artistId);
            var song = _repository.GetSong(albumId, songId, artistId);
            if (song == null)
            {
                throw new NotFoundItemException($"The song with id: {songId} does not exist in album with id:{albumId}.");
            }
            return _mapper.Map<SongModel>(song);
        }
        

        private IEnumerable<SongModel> OrderSongs(string orderBy, IEnumerable<SongModel> songsFiltered)
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

        private IEnumerable<SongModel> FilterSongs(string filter, IEnumerable<SongModel> albumSongs)
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

        public SongModel UpdateSong(long albumId, long songId, SongModel updatedSong, long artistId)
        {
            var songToUpdate = _repository.UpdateSong(albumId, songId, _mapper.Map<SongEntity>(updatedSong), artistId);
            return _mapper.Map<SongModel>(songToUpdate);
        }

        private void ValidateAlbum(long albumId, long artistId)
        {
            var album = _repository.GetAlbum(albumId, artistId);
            if (album == null)
            {
                throw new NotFoundItemException($"The album with id: {albumId} does not exist in artist with id:{artistId}.");
            }
        }

        public SongModel UpdateReproductions(long albumId, long songId, Models.ActionForModels action, long artistId)
        {
            if (!_allowedUpdatesToReproductions.Contains(action.Action.ToLower()))
                throw new InvalidOperationItemException($"The update: {action.Action} is invalid");
            var song = _mapper.Map<SongModel>(_repository.UpdateReproductions(albumId, songId, action, artistId));
            return song;
        }
        public IEnumerable<SongModel> GetSongs(long albumId, long artistId, string orderBy = "id", string filter = "allSongs")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            if (!_allowedFilters.Contains(filter.ToLower()))
                throw new InvalidOperationItemException($"The filter value: {filter} is invalid, please use one of {String.Join(',', _allowedFilters.ToArray())}");

            ValidateAlbum(albumId, artistId);
            IEnumerable<SongModel> albumSongs = _mapper.Map<IEnumerable<SongModel>>(_repository.GetSongs(albumId, artistId, orderBy, filter));
            var songsFiltered = FilterSongs(filter, albumSongs);
            var songsOrdered = OrderSongs(orderBy, songsFiltered);
            return songsOrdered;
        }
        public IEnumerable<SongModel> GetMostPlayedSongs(string orderBy = "reproductions", string filter = "top10")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            if (!_allowedFilters.Contains(filter.ToLower()))
                throw new InvalidOperationItemException($"The filter value: {filter} is invalid, please use one of {String.Join(',', _allowedFilters.ToArray())}");
            var songsFiltered = FilterSongs(filter, GetAllSongs());
            var songsOrdered = OrderSongs(orderBy, songsFiltered);
            return songsOrdered;
        }
        public IEnumerable<SongModel> GetAllSongs()
        {
            return _mapper.Map<IEnumerable<SongModel>>(_repository.GetAllSongs());
        }
    }
}
