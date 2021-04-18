using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicLibraryAPI.Models;
using MusicLibraryAPI.Data.Entities;
namespace MusicLibraryAPI.Data.Repositories
{
    public interface IMusicLibraryRepository
    {
        public IEnumerable<ArtistEntity> GetArtists(string orderBy = "id");
        public ArtistEntity GetArtist(long artistId);
        public ArtistEntity CreateArtist(ArtistEntity newArtist);
        public bool DeleteArtist(long artistId);
        public ArtistEntity UpdateArtist(long artistId, ArtistEntity updatedArtist);
        public ArtistEntity UpdateArtistFollowers(long artistId, Models.ActionForModels action);
        /// public string GetMeanOfFollowersByYearsOfCareer(int years = 0);
        ///public List<ArtistForDecadeModel> GetArtistForYearOfBorning();


        public IEnumerable<AlbumEntity> GetAlbums(long artistId);
        public AlbumEntity GetAlbum(long artistId, long albumId);
        public AlbumEntity CreateAlbum(long artistId, AlbumEntity newAlbum);
        public bool DeleteAlbum(long artistId, long albumId);
        public AlbumEntity UpdateAlbum(long artistId, long albumId, AlbumEntity updatedAlbum);
        public IEnumerable<AlbumEntity> GetAllAlbums();
        /// public IEnumerable<AlbumEntity> BestAlbums();
        public IEnumerable<AlbumEntity> GetTop(string value = "", int n = 5, bool isDescending = false);


        public IEnumerable<SongEntity> GetSongs(long albumId, long artistId, string orderBy = "id", string filter = "allSongs");
        public IEnumerable<SongEntity> GetAllSongs();
        public SongEntity GetSong(long albumId, long songId, long artistId);
        public SongEntity CreateSong(long albumId, SongEntity newSong, long artistId);
        public bool DeleteSong(long albumId, long songId, long artistId);
        public SongEntity UpdateSong(long albumId, long songId, SongEntity updatedSong, long artistId);
        public SongEntity UpdateReproductions(long albumId, long songId, Models.ActionForModels action, long artistId);

        ///public IEnumerable<SongEntity> GetMostPlayedSongs(string orderBy = "reproductions", string filter = "top10");
    }
}
