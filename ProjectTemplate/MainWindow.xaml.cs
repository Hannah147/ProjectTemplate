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

            Movies movie1 = new Movies(movieName, movieGenre, movieDescription, "");

            AllMovies.Add(movie1);

            lbxWatchlist.ItemsSource = null;
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

            string[] Ratings = { "1 Star", "2 Stars", "3 Stars", "4 Stars", "5 Stars" };
            cbxAddRating.ItemsSource = Ratings;

            cbxRatingFilter.ItemsSource = Ratings;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Movies selectedMovie = lbxWatchlist.SelectedItem as Movies;

            if(selectedMovie != null)
            {
                /*string movieName = tbxMovieName.Text;
                string movieGenre = cbxGenre.SelectedItem.ToString();
                string movieDescription = tbxDescription.Text;
                string movieRating = cbxGenre.SelectedItem.ToString();

                MoviesWatched movieWatched = new MoviesWatched(movieName, movieGenre, movieDescription, movieRating);

                WatchedMovies.Add(movieWatched);*/

                lbxWatched.ItemsSource = null;
                lbxWatched.ItemsSource = WatchedMovies;
                AllMovies.Remove(selectedMovie);
                WatchedMovies.Add(selectedMovie);

                //string movieRating = cbxAddRating.SelectedItem.ToString();
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

        private void lbxWatched_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movies selectMovie = lbxWatched.SelectedItem as Movies;
            if (selectMovie != null)
            {
                tblkDescription.Text = selectMovie.MovieRating;
            }
        }
    }
}
