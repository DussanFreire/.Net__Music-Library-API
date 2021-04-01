using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Models
{
    public class ArtistForDecadeModel
    {
        public long Year { get; set; }
        public List<string> Names { get; set; }
        public ArtistForDecadeModel()
        {
            Names = new List<string>();
        }
    }
}
