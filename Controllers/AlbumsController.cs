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
            if(albums.ContainsKey(albumID))
                albums.Remove(albumID, out Album a);
            return new OkResult();
        }
    }
}
