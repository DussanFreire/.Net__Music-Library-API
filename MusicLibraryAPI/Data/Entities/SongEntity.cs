using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Data.Entities
{
    public class SongEntity
    {
        [Key]
        [Required]
        public long Id { get; set; }
        public string Name { get; set; }
        public long? Reproductions { get; set; }
        public string Duration { get; set; }
        public string Genres { get; set; }
        public AlbumEntity Album { get; set; }
    }
}
