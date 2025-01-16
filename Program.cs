using SQLSongRepos;

var songRepo = new SQLSongRepo();

var Filter = "1";
var songs = songRepo.ReadAllByArtistBetter(Filter);

foreach (var song in songs)
    Console.WriteLine(song.ToString());

songRepo.ChooseTheOption();

Console.ReadKey();