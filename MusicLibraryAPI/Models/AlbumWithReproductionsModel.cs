using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Models
{
    public class AlbumWithReproductionsModel:AlbumModel
    {
        public long? Reproductions { get; set; }
    }
}
