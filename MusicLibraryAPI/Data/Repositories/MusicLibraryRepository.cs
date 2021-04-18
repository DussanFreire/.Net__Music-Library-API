using MusicLibraryAPI.Data.Entities;
using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MusicLibraryAPI.Data.Repositories
{
    public class MusicLibraryRepository : IMusicLibraryRepository
    {
        private IList<ArtistEntity> _artists;
        private List<AlbumEntity> _albums;
        private List<SongEntity> _songs;
        private HashSet<string> _allowedOrderByValues = new HashSet<string>()
        {
            "id",
            "name",
            "followers",
            "Nacionality"
        };

        private HashSet<string> _allowedUpdatesToFollowers = new HashSet<string>()
        {
            "artist followed by an user",
            "artist unfollowed by an user",
        };
        private HashSet<string> _allowedTopValues = new HashSet<string>()
        {
            "likes",
            "price",
            "popularity"
        };

        private HashSet<string> _allowedUpdatesToReproductions = new HashSet<string>()
        {
            "the song was played",
        };

        private HashSet<string> _allowedOrderByValuesSongs = new HashSet<string>()
        {
            "id",
            "name",
            "reproductions",
        };

        private HashSet<string> _allowedFilters = new HashSet<string>()
        {
            "allsongs",
            "top3",
            "top10",
            "top50",
            "blues",
            "cumbia",
            "rock",
            "mostplayedsong",
        };


        public MusicLibraryRepository()
        {
            _artists = new List<ArtistEntity>();
            _albums = new List<AlbumEntity>();
            _songs = new List<SongEntity>();

            _artists.Add(new ArtistEntity()
            {
                Id = 1,
                ArtisticName = "Bob Dylan",
                Name = "Robert Allen Zimmerman",
                Followers = 121546,
                Nacionality = "USA",
                ArtistDescription = "Bob Dylan (born Robert Allen Zimmerman; May 24, 1941) is an American singer-songwriter, author and visual artist. Often regarded as one of the greatest songwriters of all time, Dylan has been a major figure in popular culture for more than 50 years. ",
                DateOfBirth = new DateTime(1941, 5, 24),

            });
            _artists.Add(new ArtistEntity()
            {
                Id = 2,
                ArtisticName = "Rod Stewart",
                Name = "Roderick David Stewart",
                Followers = 213215,
                Nacionality = "England",
                ArtistDescription = "Modern fans of Rod Stewart's smooth songbook recordings might not recognize the tough rock that defined his early work. His solo career began when he was still a member of the Faces, and – at least initially – mirrored his main band's ragged roots rock. But he found far more chart success and soon struck out on his own.",
                DateOfBirth = new DateTime(1945, 1, 10),
            });

            _artists.Add(new ArtistEntity()
            {
                Id = 3,
                ArtisticName = "Jimi Hendrix",
                Name = "James Marshall Hendrix",
                Followers = 4534546,
                Nacionality = "USA",
                ArtistDescription = "James Marshall Hendrix, better known as Jimi Hendrix, was an American guitarist, singer, and songwriter. Despite the fact that his professional career only lasted four years, he is considered one of the most influential guitarists in rock history.",
                DateOfBirth = new DateTime(1942, 10, 27),
            });
            _artists.Add(new ArtistEntity()
            {
                Id = 4,
                ArtisticName = "Ice Cube",
                Name = "O'Shea Jackson",
                Followers = 500000,
                Nacionality = "USA",
                ArtistDescription = "Ice Cube is an American rapper, actor, and filmmaker. They is knowed for his political rap.",
                DateOfBirth = new DateTime(1978, 1, 1),
            });
            _artists.Add(new ArtistEntity()
            {
                Id = 5,
                ArtisticName = "Asca",
                Name = "Asuka Ōkura",
                Followers = 200000,
                Nacionality = "Japanese",
                ArtistDescription = "Asuka made her debut in 2013 after becoming a finalist at the 5th Animax All-Japan Anisong Grand Prix. After focusing on her studies, she resumed her music career in late 2016.",
                DateOfBirth = new DateTime(1996, 9, 5),
            });
            _artists.Add(new ArtistEntity()
            {
                Id = 6,
                ArtisticName = "Eminen",
                Name = "Marshall Bruce Mathers III",
                Followers = 2000000,
                Nacionality = "USA",
                ArtistDescription = "Eminen is an American rapper, songwriter, and record producer. Eminem is among the best-selling music artists of all time, with estimated worldwide sales of more than 220 million records.",
                DateOfBirth = new DateTime(1972, 10, 17),
            });
            _artists.Add(new ArtistEntity()
            {
                Id = 7,
                ArtisticName = "Laura Pausini",
                Name = "Laura Pausini",
                Followers = 473000,
                Nacionality = "Italian",
                ArtistDescription = "Laura is an Italian singer-songwriter and television personality.  After competing in local singing contests, Pausini signed her first recording contract. She rose to fame in 1993.",
                DateOfBirth = new DateTime(1974, 5, 16),
            });

            _albums.Add(new AlbumEntity()
            {
                Id = 1,
                Name = "The Times They Are A-Changin'",
                RecordIndustry = "Columbia Records",
                Likes = 154544545,
                PublicationDate = new DateTime(1964, 1, 13),
                Description = "Dylan wrote the song as a deliberate attempt to create an anthem of change for the time, influenced by Irish and Scottish ballads.",
                Price = 30,
                Popularity = 85.0,
                ArtistId = 1
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 2,
                Name = "The Definitive Rod Stewart",
                RecordIndustry = "Rhino Records",
                Likes = 25454121,
                PublicationDate = new DateTime(2008, 11, 14),
                Description = "Stewart's album and single sales total have been variously estimated as more than 100 million, or at 200 million.",
                Price = 25,
                Popularity = 87.3,
                ArtistId = 2
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 3,
                Name = "Band of Gypsys",
                RecordIndustry = "Fillmore East",
                Likes = 2311445,
                PublicationDate = new DateTime(1970, 1, 1),
                Description = "It contains previously unreleased songs and was the last full-length Hendrix album released before his death.",
                Price = 20,
                Popularity = 86.22,
                ArtistId = 3
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 4,
                Name = "The Predator",
                RecordIndustry = "Echo Sound",
                Likes = 504065,
                PublicationDate = new DateTime(1992, 3, 11),
                Description = "It is the third solo album by American rapper Ice Cube. Released in 1992 Los Angeles Riots month.",
                Price = 20,
                Popularity = 75.65,
                ArtistId = 4,
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 5,
                Name = "I Am the West",
                RecordIndustry = "Lench Mob Records & EMI",
                Likes = 584065,
                PublicationDate = new DateTime(2010, 7, 28),
                Description = "I Am the West is the ninth studio album by American rapper Ice Cube",
                Price = 25,
                Popularity = 78.65,
                ArtistId = 4,
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 6,
                Name = "Lethal Injection",
                RecordIndustry = "Echo Sound",
                Likes = 1384065,
                PublicationDate = new DateTime(1993, 12, 7),
                Description = "Lethal Injection is the fourth studio album by American rapper Ice Cube.",
                Price = 25,
                Popularity = 74.65,
                ArtistId = 4,
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 7,
                Name = "CHAIN",
                RecordIndustry = "Sony Music Labels Inc",
                Likes = 684065,
                PublicationDate = new DateTime(2020, 2, 26),
                Description = "Hyakkiyakou - lectura: Hyakkiyakou",
                Price = 25,
                Popularity = 74.65,
                ArtistId = 5,
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 8,
                Name = "The Eminen Show",
                RecordIndustry = "Encore",
                Likes = 2684065,
                PublicationDate = new DateTime(2002, 5, 26),
                Description = "The Eminem Show is the fourth studio album by American rapper Eminem. Originally scheduled for release on June 4, 2002, the album.",
                Price = 35,
                Popularity = 80.65,
                ArtistId = 6,
            });
            _albums.Add(new AlbumEntity()
            {
                Id = 9,
                Name = "Yo Canto",
                RecordIndustry = "Warner Music",
                Likes = 3685462,
                PublicationDate = new DateTime(2002, 5, 26),
                Description = "The album is a recopilation of 16 songs composed by the most destaced Italians autos of the XX centuary.",
                Price = 40,
                Popularity = 86.65,
                ArtistId = 7,
            });

            _songs.Add(new SongEntity()
            {
                Id = 1,
                Name = "Like a Rolling Stone",
                Reproductions = 15454444545,
                Genres = "Blues",
                Duration = "4:31",
                AlbumId = 1
            });
            _songs.Add(new SongEntity()
            {
                Id = 2,
                Name = "Tangled Up In Blue",
                Reproductions = 35254542465465,
                Genres = "Rock",
                Duration = "4:32",
                AlbumId = 1
            });
            _songs.Add(new SongEntity()
            {
                Id = 3,
                Name = "Young turks",
                Reproductions = 45454545465465,
                Duration = "5:02",
                Genres = "Rock",
                AlbumId = 2
            });
            _songs.Add(new SongEntity()
            {
                Id = 4,
                Name = "Have You Ever Seen The Rain",
                Reproductions = 5454545465465,
                Duration = "3:10",
                Genres = "Rock",
                AlbumId = 2
            });
            _songs.Add(new SongEntity()
            {
                Id = 5,
                Name = "Machine Gun",
                Reproductions = 12233131313,
                Genres = "Rock",
                Duration = "12:38",
                AlbumId = 3
            });

            _songs.Add(new SongEntity()
            {
                Id = 6,
                Name = "Changes",
                Reproductions = 16532123,
                Duration = "7:21",
                Genres = "Rock",
                AlbumId = 3
            });
            _songs.Add(new SongEntity()
            {
                Id = 7,
                Name = "Song 3",
                Reproductions = 34567643,
                Duration = "7:21",
                Genres = "Blues",
                AlbumId = 1
            });
            _songs.Add(new SongEntity()
            {
                Id = 8,
                Name = "Song 4",
                Reproductions = 43456864,
                Duration = "7:21",
                Genres = "Cumbia",
                AlbumId = 1
            });
            _songs.Add(new SongEntity()
            {
                Id = 9,
                Name = "Song 5",
                Reproductions = 7875647,
                Duration = "7:21",
                Genres = "Rock",
                AlbumId = 1
            });
            _songs.Add(new SongEntity()
            {
                Id = 10,
                Name = "Song 6",
                Reproductions = 8458937,
                Genres = "Blues",
                Duration = "7:21",
                AlbumId = 1
            });
            _songs.Add(new SongEntity()
            {
                Id = 11,
                Name = "Song 7",
                Reproductions = 65778978,
                Genres = "Cumbia",
                Duration = "7:21",
                AlbumId = 1
            });
            _songs.Add(new SongEntity()
            {
                Id = 12,
                Name = "It Was A Good Day",
                Reproductions = 13619985,
                Genres = "Rap",
                Duration = "4:20",
                AlbumId = 4
            });
            _songs.Add(new SongEntity()
            {
                Id = 13,
                Name = "Howling",
                Reproductions = 1817752,
                Genres = "J-Pop",
                Duration = "4:32",
                AlbumId = 7
            });
            _songs.Add(new SongEntity()
            {
                Id = 14,
                Name = "Business",
                Reproductions = 7100828,
                Genres = "Hip Hop",
                Duration = "4:11",
                AlbumId = 8
            });
            _songs.Add(new SongEntity()
            {
                Id = 15,
                Name = "Yo Canto",
                Reproductions = 1307493,
                Genres = "Pop",
                Duration = "4:21",
                AlbumId = 9
            });
        }
        public IEnumerable<ArtistEntity> GetArtists(string orderBy = "id")
        {
            switch (orderBy.ToLower())
            {
                case "name":
                    return _artists.OrderBy(t => t.Name);
                case "followers":
                    return _artists.OrderByDescending(t => t.Followers);
                case "Nacionality":
                    return _artists.OrderBy(t => t.Nacionality);
                default:
                    return _artists.OrderBy(t => t.Id);
            }
        }
        public ArtistEntity GetArtist(long artistId)
        {
            var artist = _artists.FirstOrDefault(t => t.Id == artistId);
            return artist;
        }
        public ArtistEntity CreateArtist(ArtistEntity newArtist)
        {
            var nextId = _artists.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
            newArtist.Id = nextId;
            _artists.Add(newArtist);
            return newArtist;
        }
        public bool DeleteArtist(long artistId)
        {
            var artistDelete = GetArtist(artistId);
            var albums = _albums.Where(a => a.ArtistId == artistDelete.Id);
            foreach (var album in albums)
            {
                _songs.RemoveAll(s=>s.AlbumId==album.Id);
            }
            _albums.RemoveAll(a => a.ArtistId == artistDelete.Id);
            _artists.Remove(artistDelete);
            return true;
        }
        public ArtistEntity UpdateArtist(long artistId, ArtistEntity updatedArtist)
        {
            updatedArtist.Id = artistId;
            var artist = GetArtist(artistId);
            artist.ArtisticName = updatedArtist.ArtisticName ?? artist.ArtisticName;
            artist.DateOfBirth = updatedArtist.DateOfBirth ?? artist.DateOfBirth;
            artist.Name = updatedArtist.Name ?? artist.Name;
            artist.ArtistDescription = updatedArtist.ArtistDescription ?? artist.ArtistDescription;
            artist.Followers = updatedArtist.Followers ?? artist.Followers;
            artist.Nacionality = updatedArtist.Nacionality ?? artist.Nacionality;
            return artist;
        }
        public ArtistEntity UpdateArtistFollowers(long artistId, Models.ActionForModels action)
        {
            var artist = GetArtist(artistId);
            switch (action.Action.ToLower())
            {
                case "artist followed by an user":
                    artist.Followers++;
                    break;
                case "artist unfollowed by an user":
                    artist.Followers--;
                    break;
                default:
                    break;
            }
            return artist;
        }

        public IEnumerable<AlbumEntity> GetAlbums(long artistId)
        {
            return _albums.Where(p => p.ArtistId == artistId);
        }
        public IEnumerable<AlbumEntity> GetAllAlbums()
        {
            return _albums;
        }
        public AlbumEntity GetAlbum(long artistId, long albumId)
        {
            return _albums.FirstOrDefault(p => p.ArtistId == artistId && p.Id == albumId);
        }
        public AlbumEntity CreateAlbum(long artistId, AlbumEntity newAlbum)
        {
            newAlbum.ArtistId = artistId;
            var nextId = _albums.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
            newAlbum.Id = nextId;
            _albums.Add(newAlbum);
            return newAlbum;
        }
        public bool DeleteAlbum(long artistId, long albumId)
        {
            var album = GetAlbum(artistId, albumId);
            _songs.ToList().RemoveAll(s => s.AlbumId == album.Id);
            _albums.Remove(album);
            return true;
        }
        public AlbumEntity UpdateAlbum(long artistId, long albumId, AlbumEntity updatedAlbum)
        {
            var albumToUpdate = GetAlbum(artistId, albumId);
            albumToUpdate.Name = updatedAlbum.Name ?? albumToUpdate.Name;
            albumToUpdate.Likes = updatedAlbum.Likes ?? albumToUpdate.Likes;
            albumToUpdate.RecordIndustry = updatedAlbum.RecordIndustry ?? albumToUpdate.RecordIndustry;
            albumToUpdate.PublicationDate = updatedAlbum.PublicationDate ?? albumToUpdate.PublicationDate;
            albumToUpdate.Description = updatedAlbum.Description ?? albumToUpdate.Description;
            albumToUpdate.Popularity = updatedAlbum.Popularity ?? albumToUpdate.Popularity;
            albumToUpdate.Price = updatedAlbum.Price ?? albumToUpdate.Price;
            return albumToUpdate;
        }
        public IEnumerable<AlbumEntity> GetTop(string value = "", int n = 5, bool isDescending = false)
        {
            var albums = _albums;
            switch (value.ToLower())
            {
                case "popularity":
                    return ((isDescending) ? albums.OrderBy(a => a.Popularity) : albums.OrderByDescending(a => a.Popularity)).Take(n);
                case "likes":
                    return ((isDescending) ? albums.OrderBy(a => a.Likes) : albums.OrderByDescending(a => a.Likes)).Take(n);
                default:
                    return ((isDescending) ? albums.OrderBy(a => a.Price) : albums.OrderByDescending(a => a.Price)).Take(n);
            }
        }


        public SongEntity GetSong(long albumId, long songId, long artistId)
        {
            var song = _songs.FirstOrDefault(p => p.AlbumId == albumId && p.Id == songId);
            return song;
        }
        public IEnumerable<SongEntity> GetSongs(long albumId, long artistId, string orderBy = "id", string filter = "allSongs")
        {
            var songs =_songs.Where(p => p.AlbumId == albumId);
            return songs;
        }
        public SongEntity CreateSong(long albumId, SongEntity newSong, long artistId)
        {
            newSong.AlbumId = albumId;
            var nextId = _songs.OrderByDescending(p => p.Id).FirstOrDefault().Id + 1;
            newSong.Id = nextId;
            _songs.Add(newSong);
            return newSong;
        }
        public bool DeleteSong(long albumId, long songId, long artistId)
        {
            var songToDelete = GetSong(albumId, songId, artistId);
            _songs.Remove(songToDelete);
            return true;
        }
        public SongEntity UpdateSong(long albumId, long songId, SongEntity updatedSong, long artistId)
        {
            var songToUpdate = GetSong(albumId, songId, artistId);
            songToUpdate.Name = updatedSong.Name ?? songToUpdate.Name;
            songToUpdate.Duration = updatedSong.Duration ?? songToUpdate.Duration;
            songToUpdate.Genres = updatedSong.Genres ?? songToUpdate.Genres;
            songToUpdate.Reproductions = updatedSong.Reproductions ?? songToUpdate.Reproductions;
            return songToUpdate;
        }
        public IEnumerable<SongEntity> GetAllSongs()
        {
            return _songs;
        }
        public SongEntity UpdateReproductions(long albumId, long songId, Models.ActionForModels action, long artistId)
        {
            var song = GetSong(albumId, songId, artistId);
            switch (action.Action.ToLower())
            {
                case "the song was played":
                    song.Reproductions++;
                    break;
                default:
                    break;
            }
            return song;
        }
    }
}
