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
        public static AlbumCollection Default() {
            return (_default ?? (_default = new AlbumCollection("/Users/ellda09/Downloads/albums.csv")));
        }

        //C'tor for the Dependency Injection
        public AlbumCollection(IConfiguration config)
        {
            var myStringValue = config["MyStringKey"];
            var fileName = "/Users/ellda09/Downloads/albums.csv";
            TextReader reader = new StreamReader(fileName);
            LoadRecords(reader);
        }

        private AlbumCollection(string fileName) {
            if(fileName != null)
            {
                TextReader reader = new StreamReader(fileName);
                LoadRecords(reader);
            }
        }

        public static AlbumCollection LoadFromFile(string filename) {
            var albums = new AlbumCollection("/Users/ellda09/Downloads/albums.csv");
            TextReader reader = new StreamReader(filename);
            albums.LoadRecords(reader);
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
    }
}