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
        public async Task<ActionResult<IEnumerable<SongModel>>> GetMostPlayedSongsAsync(string orderBy = "reproductions", string filter = "top10")
        {
            try
            {
                var songs = await _songService.GetMostPlayedSongsAsync(orderBy, filter);
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
