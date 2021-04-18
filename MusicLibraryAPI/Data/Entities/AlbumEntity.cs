using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Data.Entities
{
    public class AlbumEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string RecordIndustry { get; set; }
        public long? Likes { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public double? Popularity { get; set; }
        public long ArtistId { get; set; }
    }
}
