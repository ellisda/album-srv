using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AlbumServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        //GET /Queen/Jazz returns `{"name":"Jazz","artist":"Queen","genre":"Rock","year":"1978"}`
        [HttpGet()]//"{artistName}/{albumName}")]
        public ActionResult<Album> Get([FromQuery(Name = "artist")] string artistName, [FromQuery(Name = "album")] string albumName)
        {
            var albums = AlbumCollection.Default();
            if (artistName != null || albumName != null)
                return albums.Where(a =>
                (artistName == null || a.Artist == artistName)
                 && (albumName == null || a.Name == albumName)
                 ).FirstOrDefault();
            else
                return albums.FirstOrDefault();
        }


        // //GET api/values
        // [HttpGet]
        // public ActionResult<IEnumerable<Album>> Get()
        // {
        //     return AlbumCollection.Default();
        // }

        [HttpGet("genres")]
        public ActionResult<IEnumerable<IGrouping<string, Album>>> GetGenreRankings()
        {
            var albums = AlbumCollection.Default();
            var g = albums.GroupBy(a => a.Genre).OrderByDescending(gr => gr.Count()); // .ToDictionary(g => g.Key);

            IGrouping<string, Album> res = null;
            // res = AlbumCollection.Default().GroupBy(a => a.Genre);


            //TODO: Think about Return type, we need something with metadata
            //ex: Genre : { Number int, Albums []albums }
            return new ActionResult<IEnumerable<IGrouping<string, Album>>>(g);
        }


        // GET Albums for a given artist, case sensitive
        [HttpGet("artists/{artistName}")]
        public ActionResult<IEnumerable<Album>> Get(string artistName)
        {
            var match = AlbumCollection.Default().Where(a => a.Artist == artistName);
            return new ActionResult<IEnumerable<Album>>(match);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
