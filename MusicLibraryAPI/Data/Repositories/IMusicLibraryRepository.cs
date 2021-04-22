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
        public Task<IEnumerable<ArtistEntity>> GetArtistsAsync(string orderBy = "id");
        public Task<ArtistEntity> GetArtistAsync(long artistId);
        public void CreateArtist(ArtistEntity newArtist);
        public Task DeleteArtistAsync(long artistId);
        public Task<ArtistEntity> UpdateArtistAsync(long artistId, ArtistEntity updatedArtist);
        public Task<ArtistEntity> UpdateArtistFollowersAsync(long artistId, Models.ActionForModels action);
        /// public string GetMeanOfFollowersByYearsOfCareer(int years = 0);
        ///public List<ArtistForDecadeModel> GetArtistForYearOfBorning();


        public Task<IEnumerable<AlbumEntity>> GetAlbumsAsync(long artistId);
        public Task<AlbumEntity> GetAlbumAsync(long artistId, long albumId);
        public void CreateAlbum(long artistId, AlbumEntity newAlbum);
        public Task DeleteAlbumAsync(long artistId, long albumId);
        public Task<AlbumEntity> UpdateAlbumAsync(long artistId, long albumId, AlbumEntity updatedAlbum);
        public Task<IEnumerable<AlbumEntity>> GetAllAlbumsAsync();
        /// public IEnumerable<AlbumEntity> BestAlbums();
        public Task<IEnumerable<AlbumEntity>> GetTopAsync(string value = "", int n = 5, bool isDescending = false);


        public Task<IEnumerable<SongEntity>> GetSongsAsync(long albumId, long artistId, string orderBy = "id", string filter = "allSongs");
        public Task<IEnumerable<SongEntity>> GetAllSongsAsync();
        public Task<SongEntity> GetSongAsync(long albumId, long songId, long artistId);
        public void CreateSong(long albumId, SongEntity newSong, long artistId);
        public Task DeleteSongAsync(long albumId, long songId, long artistId);
        public Task<SongEntity> UpdateSongAsync(long albumId, long songId, SongEntity updatedSong, long artistId);
        public Task<SongEntity> UpdateReproductionsAsync(long albumId, long songId, Models.ActionForModels action, long artistId);

        ///public IEnumerable<SongEntity> GetMostPlayedSongs(string orderBy = "reproductions", string filter = "top10");
        public Task<bool> SaveChangesAsync();
    }
}
