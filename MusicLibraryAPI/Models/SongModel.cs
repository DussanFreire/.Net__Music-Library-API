using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Models
{
    public class SongModel
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public long? Reproductions{ get; set; }
        public string Duration { get; set; }
        public string Genres { get; set; }
        public long AlbumId { get; set; }

    }
}
