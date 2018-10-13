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
        //GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Album>> Get()
        {
            return AlbumCollection.Default();
        }

        [HttpGet("genres")]
        public ActionResult<IEnumerable<IGrouping<string, Album>>> GetGenreRankings()
        {
            var albums = AlbumCollection.Default();
            var g = albums.GroupBy(a => a.Genre);
            return new ActionResult<IEnumerable<IGrouping<string, Album>>>(g);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Album> Get(int id)
        {
            return AlbumCollection.Default()[id];
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
