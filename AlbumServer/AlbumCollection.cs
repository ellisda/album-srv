using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace AlbumServer
{

    public class Album  {
        public int AlbumID {get; set;}
        public string Name {get; set; }
        public string Artist {get; set;}
        public string Genre {get; set;}
        public string Year {get; set;}
    }

    public class AlbumCollection : ConcurrentDictionary<int, Album>
    {
        private int _numRecords;
        private static AlbumCollection _default;
        public static AlbumCollection Default() => _default ?? (_default = new AlbumCollection(null));

        public static void InitializeFromFile(string fileName)
        {
            _default = new AlbumCollection(fileName);
        }

        public static AlbumCollection LoadFromFile(string filename) {
            var albums = new AlbumCollection(filename);
            return albums;
        }

        public void LoadRecords(TextReader r) {
            string line;
            while(null != (line = r.ReadLine()))
            {
//                Console.WriteLine(line);
                var fields = line.Split(',');
                if(fields.Length >= 1 && fields[0] == "album")
                    continue;
                //Console.WriteLine($"{fields.Length} - {fields[0]}");

                AddNewRecord(new Album{
                        Name = fields[0],
                        Artist = fields[1],
                        Genre = fields[2],
                        Year = fields[3],
                        });
                    
//                Console.WriteLine($"album: {album.album}");
            }
        }

        public void AddNewRecord(Album album)
        {
            var albumID = Interlocked.Increment(ref _numRecords) - 1;
            album.AlbumID = albumID;
            this[albumID] = album;
        }

        private AlbumCollection(string fileName) {
            if(fileName != null && File.Exists(fileName))
            {
                TextReader reader = new StreamReader(fileName);
                LoadRecords(reader);
            }
        }
    }
}