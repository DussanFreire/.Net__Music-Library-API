using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public interface IAlbumsService
    {
        public Task<IEnumerable<AlbumModel>> GetAlbumsAsync(long artistId);
        public Task<AlbumModel> GetAlbumAsync(long artistId, long albumId);
        public Task<AlbumModel> CreateAlbumAsync(long artistId, AlbumModel newAlbum);
        public Task<bool> DeleteAlbumAsync(long artistId, long albumId);
        public Task<AlbumModel> UpdateAlbumAsync(long artistId, long albumId, AlbumModel updatedAlbum);
        public Task<IEnumerable<AlbumModel>> GetAllAlbumsAsync();
        public Task<IEnumerable<AlbumModel>> BestAlbumsAsync();
        public Task<IEnumerable<AlbumModel>> GetTopAsync(string value = "", int n = 5, bool isDescending = false);
    }
}
