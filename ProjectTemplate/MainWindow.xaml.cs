using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Movies> AllMovies = new List<Movies>();
        List<Movies> WatchedMovies = new List<Movies>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string movieName = tbxMovieName.Text;
            string movieGenre = cbxGenre.SelectedItem.ToString();
            string movieDescription = tbxDescription.Text;

            /*Movies[] MoviesAll = new Movies[100];

            for (int counter = 0; counter < 5; counter++)
            {
                MoviesAll[counter] = new Movies(movieName, movieGenre, movieDescription);
                AllMovies.Add(MoviesAll[counter]);
            }*/

            Movies movie1 = new Movies(movieName, movieGenre, movieDescription);

            AllMovies.Add(movie1);

            lbxWatchlist.ItemsSource = AllMovies;

            tbxMovieName.Clear();
            tbxDescription.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] Genres = { "Horror", "Comedy", "Action", "Romance", "Superhero", "Drama" };
            cbxGenre.ItemsSource = Genres;
            cbxGenre.SelectedIndex = 0;

            cbxGenreSort.ItemsSource = Genres;
            cbxGenreSort.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Movies selectedMovie = lbxWatchlist.SelectedItem as Movies;

            if(selectedMovie != null)
            {
                AllMovies.Remove(selectedMovie);
                WatchedMovies.Add(selectedMovie);

                RefreshScreen();
            }
        }

        private void RefreshScreen()
        {
            lbxWatchlist.ItemsSource = null;
            lbxWatchlist.ItemsSource = AllMovies;

            lbxWatched.ItemsSource = null;
            lbxWatched.ItemsSource = WatchedMovies;
        }

        private void lbxWatchlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movies selectMovie = lbxWatchlist.SelectedItem as Movies;
            if(selectMovie != null)
            {
                tblkDescription.Text = selectMovie.MovieDescription;
            }
        }
    }
}
