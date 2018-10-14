using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test.Controllers
{
    [TestClass]
    public class AlbumsControllerTests
    {
        private AlbumServer.Album QueenAlbum => new AlbumServer.Album{AlbumID= -1, Name = "Jazz", Artist = "Queen", Genre = "Rock", Year = "1978"};

        [TestInitialize()]
        public void Startup()
        {
            //Reset the singleton to an empty AlbumCollection
            AlbumServer.AlbumCollection.InitializeFromFile(null);
        }

        [TestMethod]
        public void TestCreateAndRead()
        {
            var c = new AlbumServer.Controllers.AlbumsController();

            var expectedAlbumId = 0;
            var res = c.GetAlbum(expectedAlbumId);
            Assert.IsNull(res.Value);

            var album = QueenAlbum;
            album.AlbumID = 42;
            c.Post(album);

            Assert.IsNotNull(c.GetAlbum(expectedAlbumId).Value);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var c = new AlbumServer.Controllers.AlbumsController();

            var album = QueenAlbum;
            album.AlbumID = 42;
            c.Put(42, album);
            Assert.AreEqual(album.Genre, c.GetAlbum(42).Value.Genre);

            album.Genre = "Unknown";
            c.Put(42, album);
            Assert.AreEqual("Unknown", c.GetAlbum(42).Value.Genre);
        }

        [TestMethod]
        public void TestDelete()
        {
            var c = new AlbumServer.Controllers.AlbumsController();

            var album = QueenAlbum;
            album.AlbumID = 42;
            c.Put(42, album);

            var res = c.GetAlbum(42);
            Assert.IsNotNull(res.Value);

            c.Delete(42);
            res = c.GetAlbum(42);
            Assert.IsNull(res.Value);
        }
    }
}
