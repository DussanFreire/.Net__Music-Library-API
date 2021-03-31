using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public interface IAlbumsWithReproductionsService
    {
        public IEnumerable<AlbumWithReproductionsModel> ChooseMostHearedAlbums();
    }
}
