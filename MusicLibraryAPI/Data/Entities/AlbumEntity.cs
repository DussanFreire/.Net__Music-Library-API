using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Data.Entities
{
    public class AlbumEntity
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public string RecordIndustry { get; set; }
        public long? Likes { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public double? Popularity { get; set; }
        public ArtistEntity Artist { get; set; }
        public ICollection<SongEntity> Songs { get; set; }

    }
}
