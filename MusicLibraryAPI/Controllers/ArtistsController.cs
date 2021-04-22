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
        public async Task<ActionResult<IEnumerable<ArtistModel>>> GetArtistsAsync(string orderBy = "id")
        {
            try
            {
                var artists = await _artistsService.GetArtistsAsync(orderBy);
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
        public async Task<ActionResult<ArtistModel>> GetArtistAsync(long artistId)
        {
            try
            {
                var artist = await _artistsService.GetArtistAsync(artistId);
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
        public async Task<ActionResult<ArtistModel>> CreateArtistAsync([FromBody] ArtistModel newArtist)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var artist = await _artistsService.CreateArtistAsync(newArtist);
                return Created($"/api/artists/{artist.Id}", artist);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }

        [HttpDelete("{artistId:long}")]
        public async Task<ActionResult<bool>> DeleteArtistAsync(long artistId)
        {
            try
            {
                var result = await _artistsService.DeleteArtistAsync(artistId);
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
        public async Task<ActionResult<ArtistModel>> UpdateArtistAsync(long artistId, [FromBody] ArtistModel updatedArtist)
        {
            try
            {
                var artist = await _artistsService.UpdateArtistAsync(artistId, updatedArtist);
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
        public async Task<ActionResult<ArtistModel>> UpdateArtistFollowersAsync(long artistId, [FromBody] Models.ActionForModels action)
        {
            try
            {
                var artist = await _artistsService.UpdateArtistFollowersAsync(artistId, action);
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
        public async Task<ActionResult<string>> getMeanAsync(int years = 3)
        {
            try
            {
                if (years < 2) 
                    return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
                var res = await _artistsService.GetMeanOfFollowersByYearsOfCareerAsync(years);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Years must be greater than 2 years.");
            }
        }
        [Route("/api/artists/year/")]
        [HttpGet]
        public async Task<ActionResult<List<ArtistForDecadeModel>>> GetArtistForYearAsync()
        {
            try
            {
                var artistForYear = await _artistsService.GetArtistForYearOfBorningAsync();
                return Ok(artistForYear);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status204NoContent, "Years of experience must be greater than 2 years.");
            }
        }
    }
}
