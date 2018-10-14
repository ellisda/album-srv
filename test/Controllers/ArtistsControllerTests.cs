using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test.Controllers
{
    [TestClass]
    public class ArtistsControllerTests
    {
        private AlbumServer.Album QueenAlbum => new AlbumServer.Album{AlbumID= -1, Name = "Jazz", Artist = "Queen", Genre = "Rock", Year = "1978"};
        private AlbumServer.Album MonkAlbum => new AlbumServer.Album{AlbumID= -1, Name = "Unerground", Artist = "Thelonius Monk", Genre = "Jazz", Year = "1968"};

        [TestInitialize()]
        public void Startup()
        {
            //Reset the singleton to an empty AlbumCollection
            AlbumServer.AlbumCollection.InitializeFromFile(null);
        }

        [TestMethod]
        public void TestGetIndex()
        {
            var albums = new AlbumServer.Controllers.AlbumsController();
            albums.Post(QueenAlbum);
            albums.Post(MonkAlbum);

            var c = new AlbumServer.Controllers.ArtistsController();

            var res = c.Get();
            var actual = res.Value.Count();
            Assert.AreEqual(2, actual);
        }

        [TestMethod]
        public void TestGetArtistsAlbums()
        {
            var albums = new AlbumServer.Controllers.AlbumsController();
            albums.Post(QueenAlbum);
            albums.Post(MonkAlbum);


            var c = new AlbumServer.Controllers.ArtistsController();

            var res = c.Get(QueenAlbum.Artist);
            Assert.AreEqual(1, res.Value.Count());

            res = c.Get(MonkAlbum.Artist);
            Assert.AreEqual(1, res.Value.Count());

            res = c.Get("not an artist");
            Assert.AreEqual(0, res.Value.Count());
        }
    }
}
