
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
        private ICollection<AlbumModel> _albums;
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
            _albums = new List<AlbumModel>();
            _albums.Add(new AlbumModel()
            {
                Id = 1,
                Name = "The Times They Are A-Changin'",
                RecordIndustry= "Columbia Records",
                Likes=154544545,
                PublicationDate= new DateTime(1964, 1, 13),
                Description = "Dylan wrote the song as a deliberate attempt to create an anthem of change for the time, influenced by Irish and Scottish ballads.",
                Price = 30,
                Popularity= 85.0,
                ArtistId =1
            });
            _albums.Add(new AlbumModel()
            {
                Id = 2,
                Name = "The Definitive Rod Stewart",
                RecordIndustry = "Rhino Records",
                Likes = 25454121,
                PublicationDate = new DateTime(2008, 11, 14),
                Description = "Stewart's album and single sales total have been variously estimated as more than 100 million, or at 200 million.",
                Price = 25,
                Popularity = 87.3,
                ArtistId = 2
            });
            _albums.Add(new AlbumModel()
            {
                Id = 3,
                Name = "Band of Gypsys",
                RecordIndustry = "Fillmore East",
                Likes = 2311445,
                PublicationDate = new DateTime(1970, 1, 1),
                Description = "It contains previously unreleased songs and was the last full-length Hendrix album released before his death.",
                Price = 20,
                Popularity = 86.22,
                ArtistId = 3
            });
            _albums.Add(new AlbumModel()
            {
                Id = 4,
                Name = "The Predator",
                RecordIndustry = "Echo Sound",
                Likes = 504065,
                PublicationDate = new DateTime(1992, 3, 11),
                Description = "It is the third solo album by American rapper Ice Cube. Released in 1992 Los Angeles Riots month.",
                Price = 20,
                Popularity = 75.65,
                ArtistId = 4,
            });
            _albums.Add(new AlbumModel()
            {
                Id = 5,
                Name = "I Am the West",
                RecordIndustry = "Lench Mob Records & EMI",
                Likes = 584065,
                PublicationDate = new DateTime(2010, 7, 28),
                Description = "I Am the West is the ninth studio album by American rapper Ice Cube",
                Price = 25,
                Popularity = 78.65,
                ArtistId = 4,
            });
            _albums.Add(new AlbumModel()
            {
                Id = 6,
                Name = "Lethal Injection",
                RecordIndustry = "Echo Sound",
                Likes = 1384065,
                PublicationDate = new DateTime(1993, 12, 7),
                Description = "Lethal Injection is the fourth studio album by American rapper Ice Cube.",
                Price = 25,
                Popularity = 74.65,
                ArtistId = 4,
            });
            _albums.Add(new AlbumModel()
            {
                Id = 7,
                Name = "CHAIN",
                RecordIndustry = "Sony Music Labels Inc",
                Likes = 684065,
                PublicationDate = new DateTime(2020, 2, 26),
                Description = "Hyakkiyakou - lectura: Hyakkiyakou",
                Price = 25,
                Popularity = 74.65,
                ArtistId = 5,
            });
            _albums.Add(new AlbumModel()
            {
                Id = 8,
                Name = "The Eminen Show",
                RecordIndustry = "Encore",
                Likes = 2684065,
                PublicationDate = new DateTime(2002, 5, 26),
                Description = "The Eminem Show is the fourth studio album by American rapper Eminem. Originally scheduled for release on June 4, 2002, the album.",
                Price = 35,
                Popularity = 80.65,
                ArtistId = 6,
            });
            _albums.Add(new AlbumModel()
            {
                Id = 9,
                Name = "Yo Canto",
                RecordIndustry = "Warner Music",
                Likes = 3685462,
                PublicationDate = new DateTime(2002, 5, 26),
                Description = "The album is a recopilation of 16 songs composed by the most destaced Italians autos of the XX centuary.",
                Price = 40,
                Popularity = 86.65,
                ArtistId = 7,
            });
        }
        public AlbumModel CreateAlbum(long artistId, AlbumModel newAlbum)
        {
            ValidateArtist(artistId);
            var newAl = _mapper.Map<AlbumModel>(_repository.CreateAlbum(artistId, _mapper.Map<AlbumEntity>(newAlbum)));
            return newAl;
        }

        public bool DeleteAlbum(long artistId, long albumId)
        {
            ValidateAlbum(artistId, albumId);
            return _repository.DeleteAlbum(artistId,albumId);
        }

        public AlbumModel GetAlbum(long artistId, long albumId)
        {
            ValidateArtist(artistId);
            var album = _repository.GetAlbum(artistId,albumId);
            if (album == null)
            {
                throw new NotFoundItemException($"The album with id: {albumId} does not exist in artist with id:{artistId}.");
            }
            return _mapper.Map<AlbumModel>(album);
        }

        public IEnumerable<AlbumModel> GetAlbums(long artistId)
        {
            ValidateArtist(artistId);
            return _mapper.Map< IEnumerable<AlbumModel>>(_repository.GetAlbums(artistId));
        }

        public IEnumerable<AlbumModel> GetAllAlbums()
        {
            return _mapper.Map<IEnumerable<AlbumModel>>(_repository.GetAllAlbums());
        }

        public AlbumModel UpdateAlbum(long artistId, long albumId, AlbumModel updatedAlbum)
        {
            ValidateArtist(artistId);
            var albumToUpdate = GetAlbum(artistId, albumId);
            var upA = _mapper.Map<AlbumModel>(_repository.UpdateAlbum(artistId, albumId, _mapper.Map<AlbumEntity>(updatedAlbum)));
            return upA;
        }
        public IEnumerable<AlbumModel> BestAlbums()
        {
            var alb = GetAllAlbums();
            var albums = alb.OrderByDescending(a => a.Popularity);
            var albums2 = new List<AlbumModel>();
            var albumsGroups = albums.GroupBy(a => a.ArtistId);
            foreach (var albs in albumsGroups)
            {
                albums2.Add(albs.FirstOrDefault());
            }
            return albums2;
        }
        public IEnumerable<AlbumModel> GetTop(string value = "", int n = 5, bool isDescending = false)
        {
            if (!_allowedTopValues.Contains(value.ToLower()))
                throw new InvalidOperationItemException($"The value: {value} is invalid, please use one of {String.Join(',', _allowedTopValues.ToArray())}");
            var albums = _mapper.Map<IEnumerable<AlbumModel>>(_repository.GetTop(value, n, isDescending));
            return albums;
        }
        private void ValidateAlbum(long artistId,long albumId)
        {
            var album= GetAlbum(artistId,albumId);
        }
        private void ValidateArtist(long artistId)
        {
            var artist = _repository.GetArtist(artistId);
            if (artist == null)
            {
                throw new NotFoundItemException($"The artist with id: {artistId} doesn't exists.");
            }
        }
    }
}
