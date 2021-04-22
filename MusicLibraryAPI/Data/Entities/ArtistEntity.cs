using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Data.Entities
{
    public class ArtistEntity
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string ArtisticName { get; set; }
        public string Name { get; set; }
        public long? Followers { get; set; }
        public string Nacionality { get; set; }
        public string ArtistDescription { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public ICollection<AlbumEntity> Albums { get; set; }
    }
}
