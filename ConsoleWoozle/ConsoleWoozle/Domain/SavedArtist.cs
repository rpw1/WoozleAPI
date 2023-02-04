using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Domain
{
    public class SavedArtist
    {
        public SavedArtist(string artistId)
        {
            Id = artistId;
            Songs = new();
            Albums = new();
        }
        public string Id { get; set; }
        public string? Name { get; set; }
        public List<SavedAlbum> Albums { get; }
        public Dictionary<string,SavedSong> Songs { get; }
    }

    public class SavedAlbum
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
    }


    public class SavedSong
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool? Explicit { get; set; }
        public string? Href { get; set; }
        public string? Uri { get; set; }
    }
}
