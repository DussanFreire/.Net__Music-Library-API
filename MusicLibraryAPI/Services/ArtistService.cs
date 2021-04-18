using MusicLibraryAPI.Exceptions;
using MusicLibraryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MusicLibraryAPI.Data.Repositories;
using MusicLibraryAPI.Data.Entities;
namespace MusicLibraryAPI.Services
{
    public class ArtistService : IArtistsService
    {
        private IMusicLibraryRepository _repository;
        private IMapper _mapper;
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
        public ArtistService(IMusicLibraryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ArtistModel CreateArtist(ArtistModel newArtist)
        {

            var newA = _mapper.Map<ArtistModel>(_repository.CreateArtist(_mapper.Map<ArtistEntity>(newArtist)));
            return newA;
        }

        public bool DeleteArtist(long artistId)
        {
            ValidateArtist(artistId);
            return _repository.DeleteArtist(artistId);
        }

        public ArtistModel GetArtist(long artistId)
        {
            var artist = _repository.GetArtist(artistId);
            if (artist == null)
            {
                throw new NotFoundItemException($"The artist with id: {artistId} doesn't exists.");
            }
            return _mapper.Map<ArtistModel>(artist);
        }

        public IEnumerable<ArtistModel> GetArtists(string orderBy = "id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            return _mapper.Map<IEnumerable<ArtistModel>>(_repository.GetArtists(orderBy));

        }
        public ArtistModel UpdateArtist(long artistId, ArtistModel updatedArtist)
        {
            ValidateArtist(artistId);
            var artist = _mapper.Map<ArtistModel>(_repository.UpdateArtist( artistId, _mapper.Map < ArtistEntity >( updatedArtist)));
            return artist;
        }

        public ArtistModel UpdateArtistFollowers(long artistId, Models.ActionForModels action)
        {
            if (!_allowedUpdatesToFollowers.Contains(action.Action.ToLower()))
                throw new InvalidOperationItemException($"The update: {action.Action} is invalid");
            ValidateArtist(artistId);
            return _mapper.Map<ArtistModel>(_repository.UpdateArtistFollowers(artistId,action));
        }
        public string GetMeanOfFollowersByYearsOfCareer(int years = 0)
        {
            var artists = _repository.GetArtists().Where(a => years <= a.DateOfBirth.Value.Year);
            return $"If the artist has at least {years} years , he can have {artists.Average(a => a.Followers)} followers.";
        }
        public List<ArtistForDecadeModel> GetArtistForYearOfBorning()
        {
            var artistGroupedByYear = _repository.GetArtists().GroupBy(a => a.DateOfBirth.Value.Year - a.DateOfBirth.Value.Year % 10);
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
            return list.OrderBy(a => a.Year).ToList();
        }
        public void ValidateArtist(long artistId)
        {
            var artist = GetArtist(artistId);
        }
    }
}
