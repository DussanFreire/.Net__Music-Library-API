using MusicLibraryAPI.Controllers;
using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public interface ISongsService
    {
        public IEnumerable<SongModel> GetSongs(long albumId, long artistId,string orderBy = "id", string filter = "allSongs");
        public IEnumerable<SongModel> GetAllSongs();
        public SongModel GetSong(long albumId, long songId, long artistId);
        public IEnumerable<SongModel> GetMostPlayedSongs(string orderBy = "reproductions", string filter = "top10");
        public SongModel CreateSong(long albumId, SongModel newSong, long artistId);
        public bool DeleteSong(long albumId, long songId, long artistId);
        public SongModel UpdateSong(long albumId, long songId, SongModel updatedSong, long artistId);
        public SongModel UpdateReproductions(long albumId, long songId, Models.ActionForModels action, long artistId);
    }
}
