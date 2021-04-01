using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Models
{
    public class ArtistModel
    {
        public long Id { get; set; }
        [Required]
        public string ArtisticName { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long? Followers { get; set; }
        [Required]
        public string Nacionality { get; set; }
        [Required]
        public string ArtistDescription { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
