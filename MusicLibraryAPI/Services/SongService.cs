using MusicLibraryAPI.Controllers;
using MusicLibraryAPI.Exceptions;
using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public class SongService : ISongsService
    {
        private ICollection<SongModel> _songs;
        private IAlbumsService _albumService;
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

        public SongService(IAlbumsService songService)
        {
            _albumService = songService;
            _songs = new List<SongModel>();
            _songs.Add(new SongModel()
            {
                Id = 1,
                Name = "Like a Rolling Stone",
                Reproductions = 15454444545,
                Genres = "Blues",
                Duration = "4:31",
                AlbumId = 1
            });
            _songs.Add(new SongModel()
            {
                Id = 2,
                Name = "Tangled Up In Blue",
                Reproductions = 35254542465465,
                Genres = "Rock",
                Duration = "4:32",
                AlbumId = 1
            });
            _songs.Add(new SongModel()
            {
                Id = 3,
                Name = "Young turks",
                Reproductions = 45454545465465,
                Duration = "5:02",
                Genres = "Rock",
                AlbumId = 2
            });
            _songs.Add(new SongModel()
            {
                Id = 4,
                Name = "Have You Ever Seen The Rain",
                Reproductions = 5454545465465,
                Duration = "3:10",
                Genres = "Rock",
                AlbumId = 2
            });
            _songs.Add(new SongModel()
            {
                Id = 5,
                Name = "Machine Gun",
                Reproductions = 12233131313,
                Genres = "Rock",
                Duration = "12:38",
                AlbumId = 3
            });

            _songs.Add(new SongModel()
            {
                Id = 6,
                Name = "Changes",
                Reproductions = 16532123,
                Duration = "7:21",
                Genres = "Rock",
                AlbumId = 3
            });
            _songs.Add(new SongModel()
            {
                Id = 7,
                Name = "Song 3",
                Reproductions = 34567643,
                Duration = "7:21",
                Genres = "Blues",
                AlbumId = 1
            });
            _songs.Add(new SongModel()
            {
                Id = 8,
                Name = "Song 4",
                Reproductions = 43456864,
                Duration = "7:21",
                Genres = "Cumbia",
                AlbumId = 1
            });
            _songs.Add(new SongModel()
            {
                Id = 9,
                Name = "Song 5",
                Reproductions = 7875647,
                Duration = "7:21",
                Genres = "Rock",
                AlbumId = 1
            });
            _songs.Add(new SongModel()
            {
                Id = 10,
                Name = "Song 6",
                Reproductions = 8458937,
                Genres = "Blues",
                Duration = "7:21",
                AlbumId = 1
            });
            _songs.Add(new SongModel()
            {
                Id = 11,
                Name = "Song 7",
                Reproductions = 65778978,
                Genres = "Cumbia",
                Duration = "7:21",
                AlbumId = 1
            });
            _songs.Add(new SongModel()
            {
                Id = 12,
                Name = "It Was A Good Day",
                Reproductions = 13619985,
                Genres = "Rap",
                Duration = "4:20",
                AlbumId = 4
            });
            _songs.Add(new SongModel()
            {
                Id = 13,
                Name = "Howling",
                Reproductions = 1817752,
                Genres = "J-Pop",
                Duration = "4:32",
                AlbumId = 7
            });
            _songs.Add(new SongModel()
            {
                Id = 14,
                Name = "Business",
                Reproductions = 7100828,
                Genres = "Hip Hop",
                Duration = "4:11",
                AlbumId = 8
            });
            _songs.Add(new SongModel()
            {
                Id = 15,
                Name = "Yo Canto",
                Reproductions = 1307493,
                Genres = "Pop",
                Duration = "4:21",
                AlbumId = 9
            });
        }
        public SongModel CreateSong(long albumId, SongModel newSong, long artistId)
        {
            ValidateAlbum(albumId, artistId);
            newSong.AlbumId = albumId;
            var nextId = _songs.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
            newSong.Id = nextId;
            _songs.Add(newSong);
            return newSong;
        }

        public bool DeleteSong(long albumId, long songId, long artistId)
        {
            var songToDelete = GetSong(albumId, songId, artistId);
            _songs.Remove(songToDelete);
            return true;
        }

        public SongModel GetSong(long albumId, long songId, long artistId)
        {
            ValidateAlbum(albumId, artistId);
            var song = _songs.FirstOrDefault(p => p.AlbumId == albumId && p.Id == songId);
            if (song == null)
            {
                throw new NotFoundItemException($"The song with id: {songId} does not exist in album with id:{albumId}.");
            }
            return song;
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
            var songToUpdate = GetSong(albumId, songId, artistId);
            songToUpdate.Name = updatedSong.Name ?? songToUpdate.Name;
            songToUpdate.Duration = updatedSong.Duration ?? songToUpdate.Duration;
            songToUpdate.Genres = updatedSong.Genres ?? songToUpdate.Genres;
            songToUpdate.Reproductions = updatedSong.Reproductions ?? songToUpdate.Reproductions;
            return songToUpdate;
        }

        private void ValidateAlbum(long albumId, long artistId)
        {
            _albumService.GetAlbum(albumId, artistId);
        }

        public SongModel UpdateReproductions(long albumId, long songId, ActionModel action, long artistId)
        {
            if (!_allowedUpdatesToReproductions.Contains(action.Action.ToLower()))
                throw new InvalidOperationItemException($"The update: {action.Action} is invalid");
            var song = GetSong(albumId, songId, artistId);
            switch (action.Action.ToLower())
            {
                case "the song was played":
                    song.Reproductions++;
                    break;
                default:
                    break;
            }
            return song;
        }
        public IEnumerable<SongModel> GetSongs(long albumId, long artistId, string orderBy = "id", string filter = "allSongs")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            if (!_allowedFilters.Contains(filter.ToLower()))
                throw new InvalidOperationItemException($"The filter value: {filter} is invalid, please use one of {String.Join(',', _allowedFilters.ToArray())}");

            ValidateAlbum(albumId, artistId);
            IEnumerable<SongModel> albumSongs = _songs.Where(p => p.AlbumId == albumId);
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
            var songsFiltered = FilterSongs(filter, _songs);
            var songsOrdered = OrderSongs(orderBy, songsFiltered);
            return songsOrdered;
        }

        public IEnumerable<SongModel> GetAllSongs()
        {
            return _songs;
        }
    }
}
