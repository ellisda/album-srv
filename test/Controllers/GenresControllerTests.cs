using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test.Controllers
{
    [TestClass]
    public class GenresControllerTests
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
        public void TestCreateAndRead()
        {
             var albums = new AlbumServer.Controllers.AlbumsController();
            albums.Post(QueenAlbum);
            albums.Post(MonkAlbum);

            var c = new AlbumServer.Controllers.GenresController();

            var res = c.GetGenreRankings();
            Assert.AreEqual(2, res.Value.Count());
        }
    }
}
