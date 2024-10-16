﻿using Microsoft.AspNetCore.Http;
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
    public class MostHeardAlbumsController : Controller
    {
        IAlbumsWithReproductionsService _albumWithReproductionsService;


        public MostHeardAlbumsController(IAlbumsService albumService, IAlbumsWithReproductionsService albumWithReproductionsService)
        {
            _albumWithReproductionsService = albumWithReproductionsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlbumWithReproductionsModel>>> GetMostHeardAlbumsAsync()
        {
            try
            {
                var albumsWithReproductions= await _albumWithReproductionsService.ChooseMostHearedAlbumsAsync();
                return Ok(albumsWithReproductions);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something unexpected happened.");
            }
            /*
            catch (NotFoundItemException ex)
            {
                return NotFound(ex.Message);
            }
            */
        }

    }
}
