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

        public async Task<ArtistModel> CreateArtistAsync(ArtistModel newArtist)
        {
            var artistEntity = _mapper.Map<ArtistEntity>(newArtist);
            _repository.CreateArtist(artistEntity);
            var result = await _repository.SaveChangesAsync();
            if (result)
            {
                return _mapper.Map<ArtistModel>(artistEntity);
            }
            throw new Exception("Database Error");
        }

        public async Task<bool> DeleteArtistAsync(long artistId)
        {
            await ValidateArtistAsync(artistId);
            await _repository.DeleteArtistAsync(artistId);
            var result = await _repository.SaveChangesAsync();

            if (!result)
            {
                throw new Exception("Database Error");
            }
            return true;
        }

        public async Task<ArtistModel> GetArtistAsync(long artistId)
        {
            var artist = await _repository.GetArtistAsync(artistId);
            if (artist == null)
            {
                throw new NotFoundItemException($"The artist with id: {artistId} doesn't exists.");
            }
            return _mapper.Map<ArtistModel>(artist);
        }

        public async Task<IEnumerable<ArtistModel>> GetArtistsAsync(string orderBy = "id")
        {
            if (!_allowedOrderByValues.Contains(orderBy.ToLower()))
                throw new InvalidOperationItemException($"The Orderby value: {orderBy} is invalid, please use one of {String.Join(',', _allowedOrderByValues.ToArray())}");
            var entityList = await _repository.GetArtistsAsync(orderBy.ToLower());
            var modelList = _mapper.Map<IEnumerable<ArtistModel>>(entityList);
            return modelList;
        }
        public async Task<ArtistModel> UpdateArtistAsync(long artistId, ArtistModel updatedArtist)
        {
            await GetArtistAsync(artistId);
            updatedArtist.Id = artistId;
            var upArt = await _repository.UpdateArtistAsync(artistId, _mapper.Map<ArtistEntity>(updatedArtist));
            var result = await _repository.SaveChangesAsync();
            if (!result)
            {
                throw new Exception("Database Error");
            }
            return _mapper.Map<ArtistModel>(upArt);
        }

        public async Task<ArtistModel> UpdateArtistFollowersAsync(long artistId, Models.ActionForModels action)
        {
            if (!_allowedUpdatesToFollowers.Contains(action.Action.ToLower()))
                throw new InvalidOperationItemException($"The update: {action.Action} is invalid");
            await ValidateArtistAsync(artistId);
            var upArt = await _repository.UpdateArtistFollowersAsync(artistId, action);
            var result = await _repository.SaveChangesAsync();
            if (result)
            {
                return _mapper.Map<ArtistModel>(upArt);
            }
            throw new Exception("Database Error");
        }
        public async Task<string> GetMeanOfFollowersByYearsOfCareerAsync(int years = 0)
        {
            var artists = await _repository.GetArtistsAsync();
            artists = artists.Where(a => years <= a.DateOfBirth.Value.Year);
            return $"If the artist has at least {years} years , he can have {(int)(artists.Average(a => a.Followers))} followers.";
        }
        public async Task<List<ArtistForDecadeModel>> GetArtistForYearOfBorningAsync()
        {
            var artistsR = await _repository.GetArtistsAsync();
            var artistGroupedByYear = artistsR.GroupBy(a => a.DateOfBirth.Value.Year - a.DateOfBirth.Value.Year % 10);
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
        public async Task ValidateArtistAsync(long artistId)
        {
            var artist = await GetArtistAsync(artistId);
        }
    }
}
