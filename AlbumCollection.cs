using System;
using System.IO;
using System.Collections.Generic;

namespace AlbumServer
{

    public class Album  {
        public string Name {get; set; }
        public string Artist {get; set;}
        public string Genre {get; set;}
        public string Year {get; set;}
    }

    public class AlbumCollection : List<Album>
    {
        private static AlbumCollection _default;
        public static AlbumCollection Default() {
            return (_default ?? (_default = LoadFromFile("/Users/ellda09/Downloads/albums.csv")));
        }

        public static AlbumCollection LoadFromFile(string filename) {
            TextReader reader = new StreamReader(filename);
            return GetRecords(reader);

        }

        static AlbumCollection GetRecords(TextReader r) {
            var albums = new AlbumCollection();
            string line;
            while(null != (line = r.ReadLine()))
            {
//                Console.WriteLine(line);
                var fields = line.Split(',');
                //Console.WriteLine($"{fields.Length} - {fields[0]}");
                var album = new Album{
                    Name = fields[0],
                    Artist = fields[1],
                    Genre = fields[2],
                    Year = fields[3],
                };
//                Console.WriteLine($"album: {album.album}");
                albums.Add(album);
            }
            return albums;
        }
    }
}
