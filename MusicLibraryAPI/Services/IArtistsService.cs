using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public interface IArtistsService
    {
        public IEnumerable<ArtistModel> GetArtists();
        public ArtistModel GetArtist(long artistId);
        public ArtistModel CreateArtist(ArtistModel newArtist);
        public bool DeleteArtist(long artistId);
        public ArtistModel UpdateArtist(long artistId, ArtistModel updatedArtist);
    }
}
