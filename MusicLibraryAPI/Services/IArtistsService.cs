using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public interface IArtistsService
    {
        public IEnumerable<ArtistModel> GetArtists(string orderBy = "id");
        public ArtistModel GetArtist(long artistId);
        public ArtistModel CreateArtist(ArtistModel newArtist);
        public bool DeleteArtist(long artistId);
        public ArtistModel UpdateArtist(long artistId, ArtistModel updatedArtist);
        public ArtistModel UpdateArtistFollowers(long artistId, Models.ActionForModels action);
        public string GetMeanOfFollowersByYearsOfCareer(int years = 0);
        public List<ArtistForDecadeModel> GetArtistForYearOfBorning();
    }
}
