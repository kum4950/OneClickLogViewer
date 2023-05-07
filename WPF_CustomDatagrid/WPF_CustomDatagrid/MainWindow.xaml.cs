using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPF_CustomDatagrid
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GRD_RowEditEnding(object sender, System.Windows.Controls.DataGridRowEditEndingEventArgs e)
        {
            Song? newsong = e.Row.DataContext as Song;
            var numOfSongsInFile = File.ReadAllLines(file).Length;
            if(numOfSongsInFile < Songs.SongList.Count)
            {
                AddNewSong(newsong!);
            }
            else
            {
                UpdateSong();
            }
        }
        const string file = @"C:\Songs.config";
        void UpdateSong()
        {
            string lines = "";
            foreach (var s in Songs.SongList)
            {
                lines += $"{s.Id},{s.Title},{s.Genre},{s.Artist},{s.MovieTitle},{s.ReleaseYear.Year},{s.URL}\n";
            }
            File.WriteAllText(file, lines);
        }

        void AddNewSong(Song? s)
        {
            if(s == null) return;
            File.AppendAllText(file, $"{s.Id},{s.Title},{s.Genre},{s.Artist},{s.MovieTitle},{s.ReleaseYear.Year},{s.URL}\n");
        }

        private void GRD_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var source = e.OriginalSource.GetType().Name;
            if (e.Key == Key.Delete && source == "DataGridCell")
            {
                var song = GRD.SelectedItem as Song;
                var result = MessageBox.Show("Are you Sure?", "Delete Song", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteSong(song);
                }
            }
        }

        void DeleteSong(Song song)
        {
            string lines = "";
            foreach (var s in Songs.SongList)
            {
                if (s.Id != song.Id)
                {
                    lines += $"{s.Id},{s.Title},{s.Genre},{s.Artist},{s.MovieTitle},{s.ReleaseYear.Year},{s.URL}\n";
                }
            }
            File.WriteAllText(file, lines);
        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var tbx = sender as TextBox;
            if(tbx!=null&&tbx.Text !="")
            {
                var filteredList = Songs.SongList.Where(x => x.Title.ToLower().Contains(tbx.Text.ToLower()));
                GRD.ItemsSource = null;
                GRD.ItemsSource = filteredList;
            }
            else
            {
                GRD.ItemsSource=Songs.SongList;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataGrid dg = new DataGrid();
            dg.ItemsSource = Songs.SongList;
            STKPNL.Children.Add(dg);
        }
    }

    public class Songs
    {
        public static List<Song> SongList { get; set; } = GetSongs();
        public static List<Song> GetSongs()
        {
            var file = @"C:\Songs.config";
            var lines = File.ReadAllLines(file);
            var list = new List<Song>();
            for(int i=0; i<lines.Length; i++)
            {
                var line = lines[i].Split(',');
                var g = line[2].Split(' ', '&', '-');
                var gr = g.Length > 1 ? g[0] + g[1] : g[0];
                List<Artist> artists = new List<Artist>();
                if (line.Length > 6)
                {
                    for(int j=6; j<line.Length; j++)
                    {
                        var artist=new Artist() { Name = line[j] };
                        artists.Add(artist);
                    }
                }
                var song = new Song()
                {
                    Id = int.Parse(line[0]),
                    Title = line[1],
                    Artist = line[3],
                    IsSoundtrack = line[4] != "Unknown",
                    MovieTitle = line[4],
                    Genre = (Genre)Enum.Parse(typeof(Genre), gr),
                    ReleaseYear = DateTime.Parse(line[5] + ",01,01"),
                    URL = new Uri($"www.{line[3]}.com", UriKind.Relative),
                    Artists = artists,
                };
                list.Add(song);
            }
            return list;
        }
    }

    public class Artist
    {
        public string? Name { get; set; }
    }

    public class Song
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Artist { get; set; }
        public bool IsSoundtrack { get; set; }
        public string? MovieTitle { get; set; }
        public int MyProperty { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseYear { get; set; }
        public Uri? URL { get; set; }
        public List<Artist>? Artists { get; set; }
    }

    public enum Genre
    {
        HeavyMetal,HardRock,SoftRock,ClassicRock,Rock,Pop,PopSoul,Soul,Blues,Jazz,RB,Country,Folk,Funk,Classical,ChristmasCarol
    }
}
