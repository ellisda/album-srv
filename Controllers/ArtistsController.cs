using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlbumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        //TODO: Replace this singleton with a DI-based IConfiguration c'tor
        private static AlbumCollection _albums = AlbumCollection.Default();

        //GET /artists returns the names of Artists
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var artists = _albums.Values.GroupBy(a => a.Artist).Select(g => g.Key);
            return new ActionResult<IEnumerable<string>>(artists);
        }

        //GET /artists/Queen returns albums by Queen
        [HttpGet("{artistName}")]
        public ActionResult<IEnumerable<Album>> Get(string artistName)
        {
            var matches = _albums.Values.Where(a => a.Artist == artistName);
            return new ActionResult<IEnumerable<Album>>(matches);
        }
    }
}
