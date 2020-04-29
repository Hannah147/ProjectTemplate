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
using System.Data.SqlClient;

namespace ProjectTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<WatchedMovies> AllMovies = new List<WatchedMovies>();
        //List<Movies> AllMoviesRated = new List<Movies>();
        List<WatchedMovies> WatchedMovies = new List<WatchedMovies>();
        List<WatchedMovies> FilterWatchlist = new List<WatchedMovies>();
        List<WatchedMovies> FilterWatched = new List<WatchedMovies>();
        string[] AllMoviesArray = new string[100];

        WatchedMoviesContainer db = new WatchedMoviesContainer();
        Random rng = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Add Movie to Movie List, calling class - Need all code, checked over
        private void btnAddWatch_Click(object sender, RoutedEventArgs e)
        {
            string movieName = tbxMovieName.Text;
            string movieGenre = cbxGenre.SelectedItem.ToString();
            string movieDescription = tbxDescription.Text;
            //string movieRating = cbxAddRating.SelectedItem.ToString();

            WatchedMovies movie1 = new WatchedMovies(movieName, movieGenre, movieDescription, "", DateTime.MinValue);

            AllMovies.Add(movie1); // might be doing this bit wrong

            lbxWatchlist.ItemsSource = null;
            lbxWatchlist.ItemsSource = AllMovies;

            tbxMovieName.Clear();
            tbxDescription.Clear();
            cbxGenre.SelectedIndex = 0;

            //db.WatchedMovies.Add(AllMovies[0]);

            // Add code for database here !!!!!!!!!!!!!!
            //SqlConnection con = new SqlConnection(@"Data Source=.\")
            //SqlCommand cmd = new SqlCommand("sp_insert", db);
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
            WatchedMovies selectedMovie = lbxWatchlist.SelectedItem as WatchedMovies;

            if(selectedMovie != null)
            {
                if (cbxAddRating.SelectedItem != null && DPDateWatched != null)
                {
                    selectedMovie.MovieRating = cbxAddRating.SelectedItem.ToString();
                    selectedMovie.DateWatched = DPDateWatched.SelectedDate.Value;

                    lbxWatched.ItemsSource = null;
                    lbxWatched.ItemsSource = WatchedMovies;
                    lbxWatchedAll.ItemsSource = null;
                    lbxWatched.ItemsSource = WatchedMovies;
                    AllMovies.Remove(selectedMovie);
                    WatchedMovies.Add(selectedMovie);

                    //string movieRating = cbxAddRating.SelectedItem.ToString();
                    RefreshScreen();

                    //AllMoviesRated.Add(selectedMovie);

                    tblkDescription.Text = "";
                    cbxAddRating.SelectedIndex = 0;
                    DPDateWatched.SelectedDate = null;

                    db.WatchedMovies.Add(selectedMovie);

                    db.SaveChanges();
                }
            }
        }

        // Refresh the screen after movie is moved from watchlist to watched
        private void RefreshScreen()
        {
            lbxWatchlist.ItemsSource = null;
            lbxWatchlist.ItemsSource = AllMovies;

            lbxWatched.ItemsSource = null;
            lbxWatched.ItemsSource = WatchedMovies;

            lbxWatchedAll.ItemsSource = null;
            lbxWatchedAll.ItemsSource = WatchedMovies;
        }

        // Display movie description under watchlist on selected movie
        private void lbxWatchlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WatchedMovies selectMovie = lbxWatchlist.SelectedItem as WatchedMovies;
            if(selectMovie != null)
            {
                tblkDescription.Text = selectMovie.MovieDescription;
            }
        }

        /* Add movie to watched list with rating on selected movie
        private void lbxWatched_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Movies selectMovie = lbxWatched.SelectedItem as Movies;
            if (selectMovie != null)
            {
                tblkRating.Text = selectMovie.MovieRating;
            }
        }*/

        // Sort by genre when selection changes on watchlist
        private void CbxGenreSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedGenre = cbxGenreSort.SelectedItem as string;
            //Movies selectMovie = lbxWatchlist.SelectedItem as Movies;

            FilterWatchlist.Clear();

            foreach (WatchedMovies movie in AllMovies)
            {
                if (movie.MovieGenre == selectedGenre)
                {
                    
                    FilterWatchlist.Add(movie);
                    

                }
                // Too much code, one if works
                //else if(movie.MovieGenre != selectedGenre)
                //{
                //    FilterWatchlist.Clear();
                //    lbxWatchlist.ItemsSource = null;
                //    lbxWatchlist.ItemsSource = FilterWatchlist;
                //}

                //else
                //{
                //    lbxWatchlist.ItemsSource = FilterWatchlist;
                //}
            }

            // Set the source after the foreach after the list has been filled
            lbxWatchlist.ItemsSource = null;
            lbxWatchlist.ItemsSource = FilterWatchlist;
        }

        // Sort by rating on watched list
        private void CbxRatingFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedRating = cbxRatingFilter.SelectedItem as string;
            //Movies selectMovie = lbxWatchlist.SelectedItem as Movies;

            //FilterWatched.Clear();

            foreach (WatchedMovies movie in WatchedMovies)
            {
                if (movie.MovieRating == selectedRating)
                {
                    FilterWatched.Clear();
                    FilterWatched.Add(movie);
                    lbxWatched.ItemsSource = null;
                    lbxWatched.ItemsSource = FilterWatched;

                }

                else if (movie.MovieRating != selectedRating)
                {
                    //FilterWatched.Clear();
                    lbxWatched.ItemsSource = null;
                    //lbxWatched.ItemsSource = FilterWatched;
                }
            }
        }

        private void LbxWatchedAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WatchedMovies selectMovie = lbxWatchedAll.SelectedItem as WatchedMovies;

            tblkMovieList.Text = selectMovie.MovieName;
            tblkMovieGenre.Text = selectMovie.MovieGenre;
            tblkMovieDescription.Text = selectMovie.MovieDescription;
            tblkMovieRating.Text = selectMovie.MovieRating;
            tblkMovieDate.Text = selectMovie.DateWatched.ToShortDateString();
        }

        private void BtnSelectRandom_Click(object sender, RoutedEventArgs e)
        {
            int sizel = AllMovies.Count;
            int randomID = rng.Next(0, AllMovies.Count);

            string MovieText = $"Movie Name: {AllMovies[randomID].MovieName}\nMovie Genre: {AllMovies[randomID].MovieGenre}\nMovie Description: {AllMovies[randomID].MovieDescription}";

            tblkMovieInfo.Text = MovieText;
        }
    }
}
