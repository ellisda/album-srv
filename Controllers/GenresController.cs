using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlbumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        [HttpGet()]
        public ActionResult<IEnumerable<IGrouping<string, Album>>> GetGenreRankings()
        {
            var albums = AlbumCollection.Default().Values;
            var g = albums.GroupBy(a => a.Genre).OrderByDescending(gr => gr.Count());

            //TODO: Think about Return type, we need something with metadata
            //ex: Genre : { Number int, Albums []albums }
            return new ActionResult<IEnumerable<IGrouping<string, Album>>>(g);
        }
    }
}
