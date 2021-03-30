using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public interface IAlbumsService
    {
        public IEnumerable<AlbumModel> GetAlbums(long artistId);
        public AlbumModel GetAlbum(long artistId, long albumId);
        public AlbumModel CreateAlbum(long artistId, AlbumModel newAlbum);
        public bool DeleteAlbum(long artistId, long albumId);
        public AlbumModel UpdateAlbum(long artistId, long albumId, AlbumModel updatedAlbum);
        public IEnumerable<AlbumModel> GetAllAlbums();
    }
}
