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
        List<Movies> AllMoviesRated = new List<Movies>();
        List<Movies> WatchedMovies = new List<Movies>();
        List<Movies> FilterWatchlist = new List<Movies>();
        List<Movies> FilterWatched = new List<Movies>();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Add Movie to Movie List, calling class
        private void btnAddWatch_Click(object sender, RoutedEventArgs e)
        {
            string movieName = tbxMovieName.Text;
            string movieGenre = cbxGenre.SelectedItem.ToString();
            string movieDescription = tbxDescription.Text;
            //string movieRating = cbxAddRating.SelectedItem.ToString();

            Movies movie1 = new Movies(movieName, movieGenre, movieDescription, "");

            AllMovies.Add(movie1);

            lbxWatchlist.ItemsSource = null;
            lbxWatchlist.ItemsSource = AllMovies;

            tbxMovieName.Clear();
            tbxDescription.Clear();
        }

        // Window loaded, sorting combo boxes
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] Genres = { "Select..." ,"Horror", "Comedy", "Action", "Romance", "Superhero", "Drama" };
            cbxGenre.ItemsSource = Genres;
            cbxGenre.SelectedIndex = 0;

            cbxGenreSort.ItemsSource = Genres;
            cbxGenreSort.SelectedIndex = 0;

            string[] Ratings = { "Select...", "1 Star", "2 Stars", "3 Stars", "4 Stars", "5 Stars" };
            cbxAddRating.ItemsSource = Ratings;
            cbxAddRating.SelectedIndex = 0;

            cbxRatingFilter.ItemsSource = Ratings;
            cbxRatingFilter.SelectedIndex = 0;
        }

        // Add movie to watched list from watchlist
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Movies selectedMovie = lbxWatchlist.SelectedItem as Movies;

            if(selectedMovie != null)
            {
                if (cbxAddRating.SelectedItem != null)
                {
                    selectedMovie.MovieRating = cbxAddRating.SelectedItem.ToString();

                    lbxWatched.ItemsSource = null;
                    lbxWatched.ItemsSource = WatchedMovies;
                    AllMovies.Remove(selectedMovie);
                    WatchedMovies.Add(selectedMovie);

                    //string movieRating = cbxAddRating.SelectedItem.ToString();
                    RefreshScreen();
                }
            }
        }

        // Refresh the screen
        private void RefreshScreen()
        {
            lbxWatchlist.ItemsSource = null;
            lbxWatchlist.ItemsSource = AllMovies;

            lbxWatched.ItemsSource = null;
            lbxWatched.ItemsSource = WatchedMovies;
        }

        // Display movie description under watchlist on selected movie
        private void lbxWatchlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movies selectMovie = lbxWatchlist.SelectedItem as Movies;
            if(selectMovie != null)
            {
                tblkDescription.Text = selectMovie.MovieDescription;
            }
        }

        // Add movie to watched list with rating on selected movie
        private void lbxWatched_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movies selectMovie = lbxWatched.SelectedItem as Movies;
            if (selectMovie != null)
            {
                tblkRating.Text = selectMovie.MovieRating;
                AllMoviesRated.Add(selectMovie);
            }
        }

        // Sort by genre when selection changes on watchlist
        private void CbxGenreSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedGenre = cbxGenreSort.SelectedItem as string;
            //Movies selectMovie = lbxWatchlist.SelectedItem as Movies;

            //FilterWatchlist.Clear();

            foreach (Movies movie in AllMovies)
            {
                if (movie.MovieGenre == selectedGenre)
                {
                    //FilterWatchlist.Clear();
                    FilterWatchlist.Add(movie);
                    lbxWatchlist.ItemsSource = null;
                    lbxWatchlist.ItemsSource = FilterWatchlist;

                }

                else if(movie.MovieGenre != selectedGenre)
                {
                    FilterWatchlist.Clear();
                    lbxWatchlist.ItemsSource = null;
                    lbxWatchlist.ItemsSource = FilterWatchlist;
                }

                else
                {
                    lbxWatchlist.ItemsSource = FilterWatchlist;
                }
            }
        }

        // Sort by rating on watched list
        private void CbxRatingFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedRating = cbxRatingFilter.SelectedItem as string;
            //Movies selectMovie = lbxWatchlist.SelectedItem as Movies;

            FilterWatched.Clear();

            foreach (Movies movie in AllMoviesRated)
            {
                if (movie.MovieRating == selectedRating)
                {
                    FilterWatched.Add(movie);
                    lbxWatched.ItemsSource = null;
                    lbxWatched.ItemsSource = FilterWatched;

                }

                else if (movie.MovieRating != selectedRating)
                {
                    FilterWatched.Clear();
                    lbxWatched.ItemsSource = null;
                    lbxWatched.ItemsSource = FilterWatched;
                }
            }
        }
    }
}
