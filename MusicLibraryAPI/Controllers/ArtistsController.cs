using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicLibraryAPI.Exceptions;
using MusicLibraryAPI.Models;
using MusicLibraryAPI.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Controllers
{
    [Route("api/[controller]")]
    public class ArtistsController : Controller
    {
        private IArtistsService _artistsService;
        public ArtistsController(IArtistsService artistsService)
        {
            _artistsService = artistsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArtistModel>> GetArtists(string orderBy = "id")
        {
            try
            {
                var artists = _artistsService.GetArtists(orderBy);
                return Ok(artists);
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

    

        [HttpGet("{artistId:long}")]
        public ActionResult<ArtistModel> GetArtist(long artistId)
        {
            try
            {
                var artist = _artistsService.GetArtist(artistId);
                return Ok(artist);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPost]
        public ActionResult<ArtistModel> CreateArtist([FromBody] ArtistModel newArtist)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var artist = _artistsService.CreateArtist(newArtist);
                return Created($"/api/artists/{artist.Id}", artist);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpDelete("{artistId:long}")]
        public ActionResult<bool> DeleteArtist(long artistId)
        {
            try
            {
                var result = _artistsService.DeleteArtist(artistId);
                return Ok(result);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPut("{artistId:long}")]
        public ActionResult<ArtistModel> UpdateArtist(long artistId, [FromBody] ArtistModel updatedArtist)
        {
            try
            {
                var artist = _artistsService.UpdateArtist(artistId, updatedArtist);
                return Ok(artist);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpPut("{artistId:long}$followers")]
        public ActionResult<ArtistModel> UpdateArtistFollowers(long artistId, [FromBody] Models.ActionForModels action)
        {
            try
            {
                var artist = _artistsService.UpdateArtistFollowers(artistId, action);
                return Ok(artist);
            }
            catch (InvalidOperationItemException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
        [Route("/api/artists/report/")]
        [HttpGet]
        public ActionResult<string> getMean(int years = 3)
        {
            try
            {
                if (years < 2)
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
                return Ok(_artistsService.GetMeanOfFollowersByYearsOfCareer(years));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Years must be greater than 2 years.");
            }
        }
        [Route("/api/artists/year/")]
        [HttpGet]
        public ActionResult<List<ArtistForDecadeModel>> GetArtistForYear()
        {
            try
            {
                var artistForYear = _artistsService.GetArtistForYearOfBorning();
                return Ok(artistForYear);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Years of experience must be greater than 2 years.");
            }
        }
    }
}
