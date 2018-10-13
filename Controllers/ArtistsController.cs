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
        //GET /artists returns the names of Artists
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            var artists = AlbumCollection.Default().GroupBy(a => a.Artist).Select(g => g.Key);
            return new ActionResult<IEnumerable<string>>(artists);
        }

        //GET /artists/Queen returns albums by Queen
        [HttpGet("{artistName}")]
        public ActionResult<IEnumerable<Album>> Get(string artistName)
        {
            var albums = AlbumCollection.Default();
            var matches = albums.Where(a => a.Artist == artistName);
            return new ActionResult<IEnumerable<Album>>(matches);
        }

    }
}
