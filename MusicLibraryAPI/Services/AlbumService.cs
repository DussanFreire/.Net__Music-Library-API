
using MusicLibraryAPI.Exceptions;
using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public class AlbumService : IAlbumsService
    {
        private ICollection<AlbumModel> _albums;
        private IArtistsService _artistsService;
        public AlbumService(IArtistsService artistsService)
        {
            _artistsService = artistsService;

            _albums = new List<AlbumModel>();
            _albums.Add(new AlbumModel()
            {
                Id = 1,
                Name = "The Times They Are A-Changin'",
                RecordIndustry= "Columbia Records",
                Likes=154544545,
                PublicationDate= new DateTime(1964, 1, 13),
                ArtistId =1
            });
            _albums.Add(new AlbumModel()
            {
                Id = 2,
                Name = "The Definitive Rod Stewart",
                RecordIndustry = "Rhino Records",
                Likes = 25454121,
                PublicationDate = new DateTime(2008, 11, 14),
                ArtistId = 2
            });
            _albums.Add(new AlbumModel()
            {
                Id = 3,
                Name = "Band of Gypsys",
                RecordIndustry = "Fillmore East",
                Likes = 2311445,
                PublicationDate = new DateTime(1970, 1, 1),
                ArtistId = 3
            });

        }
        public AlbumModel CreateAlbum(long artistId, AlbumModel newAlbum)
        {
            ValidateArtist(artistId);
            newAlbum.ArtistId = artistId;
            var nextId = _albums.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
            newAlbum.Id = nextId;
            _albums.Add(newAlbum);
            return newAlbum;
        }

        public bool DeleteAlbum(long artistId, long albumId)
        {
            var albumToDelete = GetAlbum(artistId, albumId);
            _albums.Remove(albumToDelete);
            return true;
        }

        public AlbumModel GetAlbum(long artistId, long albumId)
        {
            ValidateArtist(artistId);
            var album = _albums.FirstOrDefault(p => p.ArtistId == artistId && p.Id == albumId);
            if (album == null)
            {
                throw new NotFoundItemException($"The album with id: {albumId} does not exist in artist with id:{artistId}.");
            }
            return album;
        }

        public IEnumerable<AlbumModel> GetAlbums(long artistId)
        {
            ValidateArtist(artistId);
            return _albums.Where(p => p.ArtistId == artistId);
        }

        public IEnumerable<AlbumModel> GetAllAlbums()
        {
            return _albums;
        }

        public AlbumModel UpdateAlbum(long artistId, long albumId, AlbumModel updatedAlbum)
        {
            var albumToUpdate = GetAlbum(artistId, albumId);
            albumToUpdate.Name = updatedAlbum.Name ?? albumToUpdate.Name;
            albumToUpdate.Likes = updatedAlbum.Likes ?? albumToUpdate.Likes;
            albumToUpdate.RecordIndustry = updatedAlbum.RecordIndustry ?? albumToUpdate.RecordIndustry;
            albumToUpdate.PublicationDate = updatedAlbum.PublicationDate ?? albumToUpdate.PublicationDate;
            return albumToUpdate;
        }
        private void ValidateArtist(long artistId)
        {
            _artistsService.GetArtist(artistId);
        }
    }
}
