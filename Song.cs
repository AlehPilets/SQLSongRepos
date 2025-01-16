using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLSongRepos
{
    public record Song(int Id, string Title, string Album, int Year, string Artist);

    //internal class Song
    //{
    //    public Song(int id, string title, string album, int year, string artist)
    //    {
    //        Id = id;
    //        Title = title;
    //        Album = album;
    //        Year = year;
    //        Artist = artist;
    //    }

    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public string Album { get; set; }
    //    public int Year { get; set; }
    //    public string Artist { get; set; }
    //    public override string ToString()
    //    {
    //        return $"Id: {Id}, Title: {Title}, Album: {Album}, Year: {Year}, Artist: {Artist}.";
    //    }
}
