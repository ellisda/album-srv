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

        // GET Albums for a given artist, case sensitive
        [HttpGet("artists/{artistName}")]
        public ActionResult<IEnumerable<Album>> Get(string artistName)
        {
            var match = AlbumCollection.Default().Values.Where(a => a.Artist == artistName);
            return new ActionResult<IEnumerable<Album>>(match);
        }

        [HttpGet("{albumID}")]
        public ActionResult<Album> GetAlbum(int albumID)
        {
             var albums = AlbumCollection.Default();
            if(!albums.ContainsKey(albumID))
                return new BadRequestResult();
            return albums[albumID];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Album a)
        {
            AlbumCollection.Default().AddNewRecord(a);
        }

        // PUT api/albums/5
        [HttpPut("{albumID}")]
        public ActionResult<Album> Put(int albumID, [FromBody] Album a)
        {
            if(albumID != a.AlbumID) {
                return new BadRequestObjectResult("supplied path param does not match albumID in payload");
            }
            //NOTE: This might no
            AlbumCollection.Default()[albumID] = a;
            return a;
        }

        // DELETE api/values/5
        [HttpDelete("{albumID}")]
        public IActionResult Delete(int albumID)
        {
            var albums = AlbumCollection.Default();
            if(!albums.ContainsKey(albumID))
                return new BadRequestResult();
            
            albums.Remove(albumID, out Album a);
            return new OkResult();
        }
    }
}
