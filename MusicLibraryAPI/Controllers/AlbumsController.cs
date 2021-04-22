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
        public async Task<ActionResult<IEnumerable<AlbumModel>>> GetAlbumsAsync(long artistId)
        {
            try
            {
                var albums = await _albumService.GetAlbumsAsync(artistId);
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
        public async Task<IActionResult> GetAlbumAsync(long artistId, long albumId)
        {
            try
            {
                var album = await _albumService.GetAlbumAsync(artistId, albumId);
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
        public async Task<ActionResult<AlbumModel>> CreateAlbumAsync(long artistId, [FromBody] AlbumModel newAlbum)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var artist = await _albumService.CreateAlbumAsync(artistId, newAlbum);
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
        public async Task<ActionResult<bool>> DeleteAlbumAsync(long artistId, long albumId)
        {
            try
            {
                var result = await _albumService.DeleteAlbumAsync(artistId, albumId);
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
        public async Task<ActionResult<AlbumModel>> UpdateAlbumAsync(long artistId, long albumId, [FromBody] AlbumModel updatedAlbum)
        {
            try
            {
                var artist = await _albumService.UpdateAlbumAsync(artistId, albumId, updatedAlbum);
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
        [Route("/api/bestalbums")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> GetBestAlbumsAsync()
        {
            try
            {
                var albums = await _albumService.BestAlbumsAsync();
                return Ok(albums);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
        }
        [Route("/api/topalbums")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlbumModel>>> GetTopAlbumsAsync(string value = "", int top = 5, bool descending = false)
        {
            try
            {
                var albums = await _albumService.GetTopAsync(value,top,descending);
                return Ok(albums);
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
    }
}
