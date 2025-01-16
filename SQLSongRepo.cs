using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace SQLSongRepos
{
    public class SQLSongRepo
    {
        public string _conectionString = "Server=OLEG\\SQLEXPRESS;Database=pilets; Integrated Security=true;";

        public void AddSong(Song song)
        {
            //insert into Song values('Tytul 2', 'Album 1', 1985, 'Jacek Placek')
            var query = $"insert into Song values('{song.Title}','{song.Album}',{song.Year} , '{song.Artist}')";
            Execute(query);
        }
        public void DeleteSong(Song song)
        {
            //Delete form Song Where Title = 'Tytul 3' and Artist = 'Jacek Placek'
            var query = $"Delete from Song Where Title ='{song.Title}' and Artist ='{song.Artist}' and Album ='{song.Album}' and [Year] ='{song.Year}'";
            Execute(query);
        }
        public void Update(Song song, Song newSong)
        {
            //update Song set [Year] = 1990 where [Year] 1988
            var query = $"update Song set Title = '{newSong.Title}',  Artist ='{newSong.Artist}',  Album = '{newSong.Album}', [Year] = '{newSong.Year}'" +
                $"where Title ='{song.Title}' and Artist ='{song.Artist}' and Album ='{song.Album}' and [Year] ='{song.Year}'";
            Execute(query);
        }
        private void Execute(string query)
        {
            using (var sqlConnection = new SqlConnection(_conectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        public Song[] ReadAll()
        {
            var query = "Select * from Song";
            return Read(query);
        }
        public Song[] ReadAllByArtistBetter(string filtr)
        {
            var query = $"Select * from Song where Artist like @Filter";
            var list = new List<Song>();
            using (var sqlConnection = new SqlConnection(_conectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {

                    sqlCommand.Parameters.AddWithValue("@Filter", filtr + "%");
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var Id = int.Parse(reader["Id"].ToString());
                        var title = reader["Title"].ToString();
                        var album = reader["Album"].ToString();
                        var year = int.Parse(reader["Year"].ToString());
                        var artist = reader["Artist"].ToString();


                        var song = new Song(Id, title, album, year, artist);
                        list.Add(song);
                    }

                }
            }
            return list.ToArray();
        }
        public Song[] ReadAllByArtist(string filtr)
        {
            var query = $"Select * from Song where Artist like '{filtr}%'";

            return Read(query);
        }
        private Song[] Read(string query)
        {

            var list = new List<Song>();
            using (var sqlConnection = new SqlConnection(_conectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var Id = int.Parse(reader["Id"].ToString()); // "i"
                        var title = reader["Title"].ToString();
                        var album = reader["Album"].ToString();
                        var year = int.Parse(reader["Year"].ToString());
                        var artist = reader["Artist"].ToString();



                        var song = new Song(Id, title, album, year, artist);
                        list.Add(song);
                    }

                }
            }
            return list.ToArray();
        }
        private Song[] ReadDBBetter(string query, string filter)
        {
            var list = new List<Song>();
            using (var sqlConnection = new SqlConnection(_conectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@Filter", filter + "%");
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = int.Parse(reader["Id"].ToString());
                        var title = reader["Title"].ToString();
                        var album = reader["Album"].ToString();
                        var year = int.Parse(reader["Year"].ToString());
                        var artist = reader["Artist"].ToString();
                        var song = new Song(id, title, album, year, artist);
                        list.Add(song);
                    }
                }
            }
            return list.ToArray();
        }
        public void ChooseTheOption()
        {
            while (true)
            {
                Console.WriteLine("--------");
                Console.WriteLine("Chose option: ");
                Console.WriteLine("1. Add song");
                Console.WriteLine("2. Delete song");
                Console.WriteLine("3. Update song");
                Console.WriteLine("4. Show all songs");
                Console.WriteLine("5. Leave");
                Console.WriteLine("--------");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("Enter data for a new song:");
                        var newSong = GetSongFromUser();
                        AddSong(newSong);
                        Console.WriteLine("Song added successfully.");
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("Enter date for delete the song:");
                        var songToDelete = GetSongFromUser();
                        DeleteSong(songToDelete);
                        Console.WriteLine("Song deleted successfully.");
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("Enter the data for the song you want to update:");
                        var oldSong = GetSongFromUser();
                        Console.WriteLine("Enter new data for the song:");
                        var updatedSong = GetSongFromUser();
                        Update(oldSong, updatedSong);
                        Console.WriteLine("Song updated successfully.");
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("List of all songs:");
                        var songs = ReadAll();
                        foreach (var song in songs)
                        {
                            Console.WriteLine($"{song.Id} - {song.Title}, {song.Album}, {song.Year}, {song.Artist}");
                        }
                        break;

                    case "5":
                        Console.WriteLine("Exit the program.");
                        return;

                    default:
                        Console.WriteLine("Wrong choice, try again.");
                        Console.Clear();
                        break;
                }
            }
        }
        private Song GetSongFromUser()
        {
            Console.Write("Song title(name): ");
            var title = Console.ReadLine();

            Console.Write("Album: ");
            var album = Console.ReadLine();

            Console.Write("Year: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Artist: ");
            var artist = Console.ReadLine();

            return new Song(0, title, album, year, artist);
        }
    }
}