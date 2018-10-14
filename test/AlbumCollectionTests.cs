using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class AlbumCollectionTests
    {
        private AlbumServer.Album QueenAlbum => new AlbumServer.Album{AlbumID= -1, Name = "Jazz", Artist = "Queen", Genre = "Rock", Year = "1978"};

        [TestInitialize()]
        public void Startup()
        {
            //Reset the singleton to an empty AlbumCollection
            AlbumServer.AlbumCollection.InitializeFromFile(null);
        }

        [TestMethod]
        public void TestConstructor()
        {
            var albums = AlbumServer.AlbumCollection.Default();
            Assert.AreEqual(0, albums.Count);
        }

        /// <summary>
        /// Test a simple add, including the albumId renumbering
        /// </summary>
        [TestMethod]
        public void TestCreateAndRead()
        {
            var albums = AlbumServer.AlbumCollection.Default();

            var a = QueenAlbum;
            albums.AddNewRecord(a);

            Assert.IsFalse(albums.ContainsKey(-1));
            Assert.IsTrue(albums.ContainsKey(0));

            var actual = albums[0];
            assertAreEqual(a, albums[0]);
            Assert.AreEqual(0, actual.AlbumID);
        }

        private void assertAreEqual(AlbumServer.Album expected, AlbumServer.Album actual)
        {
            //NOTE: We're not asserting on AlbumID, that's tested separately
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Artist, actual.Artist);
            Assert.AreEqual(expected.Genre, actual.Genre);
            Assert.AreEqual(expected.Genre, actual.Genre);
        }


        // TODO - Pick a broken way to turn the /Users/ellda09/Documents/dotnetGetStarted/albums/test/bin/Debug/ folder
        //                                      /Users/ellda09/Documents/dotnetGetStarted/albums/albums.csv
        // [TestMethod]
        // public void TestLoadFromDisk()
        // {
        //     var current = System.Environment.CurrentDirectory;
        //     var anot = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;
        //     AlbumServer.AlbumCollection.InitializeFromFile("../albums.csv");
        //     var albums = AlbumServer.AlbumCollection.Default();
        //     Assert.AreEqual(101, albums.Count);
        // }

    }
}
