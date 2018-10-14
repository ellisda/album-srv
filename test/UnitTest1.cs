using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestConstructor()
        {
            var albums = AlbumServer.AlbumCollection.Default();
            Assert.AreEqual(0, albums.Count);
        }

        [TestMethod]
        public void TestLoadFromDisk()
        {
            AlbumServer.AlbumCollection.InitializeFromFile("../albums.csv");
            var albums = AlbumServer.AlbumCollection.Default();
            Assert.AreEqual(101, albums.Count);
        }

    }
}
