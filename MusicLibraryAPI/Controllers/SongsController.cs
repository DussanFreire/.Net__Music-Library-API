using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MusicLibraryAPI.Exceptions;
using MusicLibraryAPI.Models;
using MusicLibraryAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicLibraryAPI.Controllers
{
    [Route("api/artists/{artistId:long}/albums/{albumId:long}/[controller]")]
    public class SongsController : Controller
    {
        private ISongsService _songService;
        
        public SongsController(ISongsService songService)
        {
            _songService = songService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongModel>>> GetSongsAsync(long artistId, long albumId, string orderBy="id", string filter="allSongs")
        {
            try
            {
                var songs = await _songService.GetSongsAsync(albumId,artistId, orderBy, filter);
                return Ok(songs);
            }
            catch (InvalidOperationItemException ex)
            {
                return NotFound(ex.Message);
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

        [HttpGet("{songId:long}")]
        public async Task<IActionResult> GetSongAsync(long artistId, long songId, long albumId)
        {
            try
            {
                var song = await _songService.GetSongAsync(albumId, songId, artistId);
                return Ok(song);
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
        public async Task<ActionResult<SongModel>> CreateSongAsync(long artistId, [FromBody] SongModel newSong, long albumId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var artist = await _songService.CreateSongAsync(albumId, newSong, artistId);
                return Created($"/api/song/{artist.Id}", artist);
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

        [HttpDelete("{songId:long}")]
        public async Task<ActionResult<bool>> DeleteSongAsync(long artistId, long songId, long albumId)
        {
            try
            {
                var result = await _songService.DeleteSongAsync(albumId, songId, artistId);
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

        [HttpPut("{songId:long}")]
        public async Task<ActionResult<SongModel>> UpdateSongAsync(long artistId, long songId, [FromBody] SongModel updatedSong, long albumId)
        {
            try
            {
                var artist = await _songService.UpdateSongAsync(albumId, songId, updatedSong, artistId);
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

        [HttpPut("{songId:long}$played")]
        public async Task<ActionResult<SongModel>> UpdateSongFollowersAsync(long artistId, long albumId, [FromBody] Models.ActionForModels action, long songId)
        {
            try
            {
                var artist = await _songService.UpdateReproductionsAsync(albumId, songId, action, artistId);
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


        //[HttpPatch("{songId:long}")]
        //public ActionResult<SongModel> PatchReproductions(long artistId, long albumId, long songId, [FromBody] JsonPatchDocument<SongModel> patchDoc)
        //{
        //    try
        //    {
        //        if (patchDoc != null)
        //        {
        //            var song = _songService.GetSong( albumId,  songId,  artistId);
        //            if (song == null)
        //            {
        //                return NotFound();
        //            }
        //            patchDoc.ApplyTo(song,ModelState);
        //            return Ok(song);
        //        }
        //        else
        //            return BadRequest(ModelState);
        //    }
        //    catch (NotFoundItemException ex)
        //    {
        //        return NotFound(ex.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, $"Something unexpected happened.{e}");
        //    }
        //}
    }
}
