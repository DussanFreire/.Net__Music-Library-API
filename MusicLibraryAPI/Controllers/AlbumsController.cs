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
    }
}
