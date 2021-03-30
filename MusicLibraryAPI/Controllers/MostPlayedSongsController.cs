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
    [Route("api/[controller]")]
    public class MostPlayedSongsController : Controller
    {
        private ISongsService _songService;
        public MostPlayedSongsController(ISongsService songService)
        {
             _songService = songService;
        }



        [HttpGet]
        public ActionResult<IEnumerable<SongModel>> GetMostPlayedSongs(string orderBy = "reproductions", string filter = "top10")
        {
            try
            {
                var songs = _songService.GetMostPlayedSongs(orderBy, filter);
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
    }
}
