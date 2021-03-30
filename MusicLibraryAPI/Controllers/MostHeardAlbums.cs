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
    public class MostHeardAlbums : Controller
    {

        private IAlbumsService _albumService;
        private ISongsService _songsService;

        public MostHeardAlbums(IAlbumsService albumService, ISongsService songsService)
        {
            _albumService = albumService;
            _songsService = songsService;
        }

        [HttpGet]
        public ActionResult<List<AlbumWithReproductionsModel>> GetMostHeardAlbums()
        {
            try
            {
                var albums = _albumService.GetAllAlbums();
                var songs = _songsService.GetAllSongs();
                var albumsWithReproductions=ChooseMostHearedAlbums(albums, songs);
                return Ok(albumsWithReproductions);
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
        private IEnumerable<AlbumWithReproductionsModel>  ChooseMostHearedAlbums(IEnumerable<AlbumModel> albums, IEnumerable<SongModel> songs)
        {
            long? reproductions = 0;
            List<AlbumWithReproductionsModel> AlbumsWithReproduction = new List<AlbumWithReproductionsModel>();
            foreach (AlbumModel album in albums)
            {
                reproductions = songs.Where(s => s.AlbumId == album.Id).Select(s=>s.Reproductions).Sum();
                AlbumWithReproductionsModel albumWithReproductions = new AlbumWithReproductionsModel()
                {
                    Name = album.Name,
                    RecordIndustry = album.RecordIndustry,
                    ArtistId = album.ArtistId,
                    Likes = album.Likes,
                    PublicationDate = album.PublicationDate,
                    Id = album.Id,
                    Reproductions = reproductions
                };
                AlbumsWithReproduction.Add(albumWithReproductions);
            }
            return AlbumsWithReproduction.ToList().Count > 0 ? AlbumsWithReproduction.Take(10) : AlbumsWithReproduction;
        }
    }
}
