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
        //TODO: Replace this singleton with a DI-based IConfiguration c'tor
        private static AlbumCollection _albums = AlbumCollection.Default();

        [HttpGet("{albumID}")]
        public ActionResult<Album> GetAlbum(int albumID)
        {
            if(!_albums.ContainsKey(albumID))
                return new BadRequestResult();
            return _albums[albumID];
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Album a)
        {
            _albums.AddNewRecord(a);
        }

        // PUT api/albums/5
        [HttpPut("{albumID}")]
        public ActionResult<Album> Put(int albumID, [FromBody] Album a)
        {
            if(albumID != a.AlbumID) {
                return new BadRequestObjectResult("supplied path param does not match albumID in payload");
            }
            //NOTE: This might no
            _albums[albumID] = a;
            return a;
        }

        // DELETE api/values/5
        [HttpDelete("{albumID}")]
        public IActionResult Delete(int albumID)
        {
            if(_albums.ContainsKey(albumID))
                _albums.Remove(albumID, out Album a);
            return new OkResult();
        }
    }
}
