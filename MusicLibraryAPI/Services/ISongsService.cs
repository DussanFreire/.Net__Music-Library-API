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
        public Task<IEnumerable<SongModel>> GetSongsAsync(long albumId, long artistId,string orderBy = "id", string filter = "allSongs");
        public Task<IEnumerable<SongModel>> GetAllSongsAsync();
        public Task<SongModel> GetSongAsync(long albumId, long songId, long artistId);
        public Task<IEnumerable<SongModel>> GetMostPlayedSongsAsync(string orderBy = "reproductions", string filter = "top10");
        public Task<SongModel> CreateSongAsync(long albumId, SongModel newSong, long artistId);
        public Task<bool> DeleteSongAsync(long albumId, long songId, long artistId);
        public Task<SongModel> UpdateSongAsync(long albumId, long songId, SongModel updatedSong, long artistId);
        public Task<SongModel> UpdateReproductionsAsync(long albumId, long songId, Models.ActionForModels action, long artistId);
    }
}
