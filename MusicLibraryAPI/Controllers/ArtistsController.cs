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
        public ArtistsController(IArtistsService teamsService)
        {
            _artistsService = teamsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ArtistModel>> GetArtists()
        {
            try
            {
                var artists = _artistsService.GetArtists();
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
        public ActionResult<bool> DeleteTeam(long artistId)
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
    }
}
