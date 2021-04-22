using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public interface IArtistsService
    {
        public Task<IEnumerable<ArtistModel>> GetArtistsAsync(string orderBy = "id");
        public Task<ArtistModel> GetArtistAsync(long artistId);
        public Task<ArtistModel> CreateArtistAsync(ArtistModel newArtist);
        public Task<bool> DeleteArtistAsync(long artistId);
        public Task<ArtistModel> UpdateArtistAsync(long artistId, ArtistModel updatedArtist);
        public Task<ArtistModel> UpdateArtistFollowersAsync(long artistId, Models.ActionForModels action);
        public Task<string> GetMeanOfFollowersByYearsOfCareerAsync(int years = 0);
        public Task<List<ArtistForDecadeModel>> GetArtistForYearOfBorningAsync();
    }
}
