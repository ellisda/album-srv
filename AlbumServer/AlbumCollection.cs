using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace AlbumServer
{

    public class Album {
        public int AlbumID {get; set;}
        public string Name {get; set; }
        public string Artist {get; set;}
        public string Genre {get; set;}
        public string Year {get; set;}
    }


    /// <summary>
    /// AlbumCollection is a typed data store for the Albums
    /// </summary>
    /// <typeparam name="int">the id for the album, assigned automatically during load</typeparam>
    /// <typeparam name="Album">the Album Value</typeparam>
    public class AlbumCollection : ConcurrentDictionary<int, Album>
    {
        private static AlbumCollection _default;
        private int _numRecords;


        /// <summary>
        /// Returns the singleton instance of an AlbumCollection, that may have been pre-loaded
        /// `InitializeFromFile`
        /// </summary>
        public static AlbumCollection Default() => _default ?? (_default = new AlbumCollection(null));

        /// <summary>
        /// Instantiate or Overwrite the Singleton with a copy from a file
        /// This chould be called before any components access `Default()`
        /// 
        /// DESIGN REVIEW: This method is a race condition with the first access of `Default()`
        ///                TODO: replace this in-memory collection with a real database
        /// </summary>
        public static void InitializeFromFile(string fileName)
        {
            _default = new AlbumCollection(fileName);
        }

        /// <summary>
        /// LoadRecords into the collection, assigning Ids as they are added
        /// </summary>
        public void LoadRecords(TextReader r, bool skipHeaderLine = true) {
            string line;
            if(skipHeaderLine)
                line = r.ReadLine();
            while(null != (line = r.ReadLine()))
            {
                var fields = line.Split(',');
                // if(fields.Length >= 1 && fields[0] == "album")
                //     continue;

                AddNewRecord(new Album{
                        Name = fields[0],
                        Artist = fields[1],
                        Genre = fields[2],
                        Year = fields[3],
                        });
            }
        }

        /// <summary>
        /// Assign an ID to the album and add it to the collection
        /// </summary>
        public void AddNewRecord(Album album)
        {
            var albumID = Interlocked.Increment(ref _numRecords) - 1;
            album.AlbumID = albumID;
            this[albumID] = album;
        }

        /// <summary>
        /// private c'tor to instantiate an empty collection or load one from a file
        /// </summary>
        /// <param name="fileName">optional path to a csv file</param>
        private AlbumCollection(string fileName = null) {
            if(fileName != null && File.Exists(fileName))
            {
                TextReader reader = new StreamReader(fileName);
                LoadRecords(reader);
            }
        }
    }
}