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
        public ActionResult<IEnumerable<SongModel>> GetSongs(long artistId, long albumId, string orderBy="id", string filter="allSongs")
        {
            try
            {
                var songs = _songService.GetSongs(albumId,artistId, orderBy, filter);
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
        public IActionResult GetSong(long artistId, long songId, long albumId)
        {
            try
            {
                var song = _songService.GetSong(albumId, songId, artistId);
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
        public ActionResult<SongModel> CreateSong(long artistId, [FromBody] SongModel newSong, long albumId)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var artist = _songService.CreateSong(albumId, newSong, artistId);
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
        public ActionResult<bool> DeleteSong(long artistId, long songId, long albumId)
        {
            try
            {
                var result = _songService.DeleteSong(albumId, songId, artistId);
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
        public ActionResult<SongModel> UpdateSong(long artistId, long songId, [FromBody] SongModel updatedSong, long albumId)
        {
            try
            {
                var artist = _songService.UpdateSong(albumId, songId, updatedSong, artistId);
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
        public ActionResult<SongModel> UpdateSongFollowers(long artistId, long albumId, [FromBody] Models.ActionForModels action, long songId)
        {
            try
            {
                var artist = _songService.UpdateReproductions(albumId, songId, action, artistId);
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
