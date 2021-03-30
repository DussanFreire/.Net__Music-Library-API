using Microsoft.AspNetCore.Http;
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
    [Route("api/artists/{artistId:long}/[controller]")]
    public class AlbumsController : Controller
    {
        private IAlbumsService _albumService;

        public AlbumsController(IAlbumsService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumModel>> GetAlbums(long artistId)
        {
            try
            {
                var albums = _albumService.GetAlbums(artistId);
                return Ok(albums);
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

        [HttpGet("{albumId:long}")]
        public IActionResult GetAlbum(long artistId, long albumId)
        {
            try
            {
                var album = _albumService.GetAlbum(artistId, albumId);
                return Ok(album);
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
        public ActionResult<AlbumModel> CreateAlbum(long artistId, [FromBody] AlbumModel newAlbum)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var artist = _albumService.CreateAlbum(artistId, newAlbum);
                return Created($"/api/artists/{artist.Id}", artist);
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

        [HttpDelete("{albumId:long}")]
        public ActionResult<bool> DeleteAlbum(long artistId, long albumId)
        {
            try
            {
                var result = _albumService.DeleteAlbum(artistId, albumId);
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

        [HttpPut("{albumId:long}")]
        public ActionResult<AlbumModel> UpdateAlbum(long artistId, long albumId, [FromBody] AlbumModel updatedAlbum)
        {
            try
            {
                var artist = _albumService.UpdateAlbum(artistId, albumId, updatedAlbum);
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
