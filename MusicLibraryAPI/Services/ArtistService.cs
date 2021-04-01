using MusicLibraryAPI.Exceptions;
using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Services
{
    public class ArtistService : IArtistsService
    {
        private IList<ArtistModel> _artists;

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
        public ArtistService()
        {
            _artists = new List<ArtistModel>();

            _artists.Add(new ArtistModel()
            {
                Id = 1,
                ArtisticName = "Bob Dylan",
                Name = "Robert Allen Zimmerman",
                Followers = 121546,
                Nacionality = "USA",
                ArtistDescription = "Bob Dylan (born Robert Allen Zimmerman; May 24, 1941) is an American singer-songwriter, author and visual artist. Often regarded as one of the greatest songwriters of all time, Dylan has been a major figure in popular culture for more than 50 years. ",
                DateOfBirth = new DateTime(1941, 5, 24),

            });
            _artists.Add(new ArtistModel()
            {
                Id = 2,
                ArtisticName = "Rod Stewart",
                Name = "Roderick David Stewart",
                Followers = 213215,
                Nacionality = "England",
                ArtistDescription = "Modern fans of Rod Stewart's smooth songbook recordings might not recognize the tough rock that defined his early work. His solo career began when he was still a member of the Faces, and – at least initially – mirrored his main band's ragged roots rock. But he found far more chart success and soon struck out on his own.",
                DateOfBirth = new DateTime(1945, 1, 10),
            });

            _artists.Add(new ArtistModel()
            {
                Id = 3,
                ArtisticName = "Jimi Hendrix",
                Name = "James Marshall Hendrix",
                Followers = 4534546,
                Nacionality = "USA",
                ArtistDescription = "James Marshall Hendrix, better known as Jimi Hendrix, was an American guitarist, singer, and songwriter. Despite the fact that his professional career only lasted four years, he is considered one of the most influential guitarists in rock history.",
                DateOfBirth = new DateTime(1942, 10, 27),
            });
            _artists.Add(new ArtistModel()
            {
                Id = 4,
                ArtisticName = "Ice Cube",
                Name = "O'Shea Jackson",
                Followers = 500000,
                Nacionality = "USA",
                ArtistDescription = "Ice Cube is an American rapper, actor, and filmmaker. They is knowed for his political rap.",
                DateOfBirth = new DateTime(1978, 1, 1),
            });
            _artists.Add(new ArtistModel()
            {
                Id = 5,
                ArtisticName= "Asca",
                Name = "Asuka Ōkura",
                Followers = 200000,
                Nacionality="Japanese",
                ArtistDescription = "Asuka made her debut in 2013 after becoming a finalist at the 5th Animax All-Japan Anisong Grand Prix. After focusing on her studies, she resumed her music career in late 2016.",
                DateOfBirth = new DateTime(1996, 9, 5),
            });
            _artists.Add(new ArtistModel()
            {
                Id = 6,
                ArtisticName = "Eminen",
                Name = "Marshall Bruce Mathers III",
                Followers = 2000000,
                Nacionality = "USA",
                ArtistDescription = "Eminen is an American rapper, songwriter, and record producer. Eminem is among the best-selling music artists of all time, with estimated worldwide sales of more than 220 million records.",
                DateOfBirth = new DateTime(1972, 10, 17),
            });
            _artists.Add(new ArtistModel()
            {
                Id = 7,
                ArtisticName="Laura Pausini",
                Name = "Laura Pausini",
                Followers = 473000,
                Nacionality = "Italian",
                ArtistDescription = "Laura is an Italian singer-songwriter and television personality.  After competing in local singing contests, Pausini signed her first recording contract. She rose to fame in 1993.",
                DateOfBirth = new DateTime(1974, 5, 16),
            });
        }

        public ArtistModel CreateArtist(ArtistModel newArtist)
        {
            var nextId = _artists.OrderByDescending(t => t.Id).FirstOrDefault().Id + 1;
            newArtist.Id = nextId;
            _artists.Add(newArtist);
            return newArtist;
        }

        public bool DeleteArtist(long artistId)
        {
            var artistDelete = GetArtist(artistId);
            _artists.Remove(artistDelete);
            return true;
        }

        public ArtistModel GetArtist(long artistId)
        {
            var artist = _artists.FirstOrDefault(t => t.Id == artistId);
            if (artist == null)
            {
                throw new NotFoundItemException($"The artist with id: {artistId} doesn't exists.");
            }
            return artist;
        }

        public IEnumerable<ArtistModel> GetArtists(string orderBy = "id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
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

        public ArtistModel UpdateArtist(long artistId, ArtistModel updatedArtist)
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

        public ArtistModel UpdateArtistFollowers(long artistId, ActionModel action)
        {
            if (!_allowedUpdatesToFollowers.Contains(action.Action.ToLower()))
                throw new InvalidOperationItemException($"The update: {action.Action} is invalid");
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
        public string GetMeanOfFollowersByYearsOfCareer(int years = 0)
        {
            var artists = _artists.Where(a => years <= a.DateOfBirth.Value.Year);
            return $"If the artist has at least {years} years , he can have {artists.Average(a => a.Followers)} followers.";
        }
        public List<ArtistForDecadeModel> GetArtistForYearOfBorning()
        {
            var artistGroupedByYear = _artists.GroupBy(a => a.DateOfBirth.Value.Year - a.DateOfBirth.Value.Year % 10);
            var list = new List<ArtistForDecadeModel>();
            foreach (var artists in artistGroupedByYear)
            {
                var artistM = new ArtistForDecadeModel();
                artistM.Year = artists.Key;
                foreach (var artist in artists)
                {
                    artistM.Names.Add(artist.Name);
                }
                list.Add(artistM);
            }
            return list;
        }
    }
}
