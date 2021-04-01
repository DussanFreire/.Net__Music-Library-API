using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Models
{
    public class AlbumModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string RecordIndustry { get; set; }
        [Required]
        public long? Likes { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Description { get; set; }
        public int? Price { get; set; }
        public double? Popularity { get; set; }
        public long ArtistId { get; set; }
    }
}
