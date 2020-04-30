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
using System.IO;
using Newtonsoft.Json;

namespace ProjectTemplate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<WatchedMovies> AllMovies = new List<WatchedMovies>();
        List<WatchedMovies> WatchedMovies = new List<WatchedMovies>();
        List<WatchedMovies> FilterWatchlist = new List<WatchedMovies>();
        List<WatchedMovies> FilterWatched = new List<WatchedMovies>();
        List<WatchedMovies> FilterAllWatched = new List<WatchedMovies>();

        //static WatchedMovies[] FilteredWatchlist = new WatchedMovies[10];

        WatchedMoviesContainer db = new WatchedMoviesContainer();
        Random rng = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Add Movie to Movie List, calling class - Need all code, checked over
        private void btnAddWatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                    string movieName = tbxMovieName.Text;
                    string movieGenre = cbxGenre.SelectedItem.ToString();
                    string movieDescription = tbxDescription.Text;

                    WatchedMovies movie1 = new WatchedMovies(movieName, movieGenre, movieDescription, "", DateTime.MinValue);

                    AllMovies.Add(movie1);

                    lbxWatchlist.ItemsSource = null;
                    lbxWatchlist.ItemsSource = AllMovies;

                    tbxMovieName.Clear();
                    tbxDescription.Clear();
                    cbxGenre.SelectedIndex = 0;

                imgGenre.Source = null;
                tblkGenreHeading.Text = "";
            }

            catch(Exception ex)
            {
                MessageBox.Show("A handled exception just occured" + ex.Message);
            }
        }

        // Window loaded, sorting combo boxes
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string[] Genres = { "Select...", "Horror", "Comedy", "Action", "Romance", "Superhero", "Drama" };
                cbxGenre.ItemsSource = Genres;
                cbxGenre.SelectedIndex = 0;

                cbxGenreSort.ItemsSource = Genres;
                cbxGenreSort.SelectedIndex = 0;

                cbxFilterGenre.ItemsSource = Genres;
                cbxFilterGenre.SelectedIndex = 0;

                string[] Ratings = { "Select...", "1 Star", "2 Stars", "3 Stars", "4 Stars", "5 Stars" };
                cbxAddRating.ItemsSource = Ratings;
                cbxAddRating.SelectedIndex = 0;

                cbxRatingFilter.ItemsSource = Ratings;
                cbxRatingFilter.SelectedIndex = 0;
            }

            catch(Exception ex)
            {
                MessageBox.Show("There was an error loading the window" + ex.Message);

            }
        }

        // Add movie to watched list from watchlist
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            WatchedMovies selectedMovie = lbxWatchlist.SelectedItem as WatchedMovies;

                if (selectedMovie != null)
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

                        RefreshScreen();

                        FilterWatchlist.Remove(selectedMovie);
                        lbxWatchlist.ItemsSource = null;
                        lbxWatchlist.ItemsSource = FilterWatchlist;

                        tblkDescription.Text = "";
                        cbxAddRating.SelectedIndex = 0;
                        DPDateWatched.SelectedDate = null;

                        db.WatchedMovies.Add(selectedMovie);

                        db.SaveChanges();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error adding the movie to the watchlist" + ex.Message);

            }
        }

        // Refresh the screen after movie is moved from watchlist to watched
        private void RefreshScreen()
        {
            //lbxWatchlist.ItemsSource = null;
            //lbxWatchlist.ItemsSource = AllMovies;

            lbxWatched.ItemsSource = null;
            lbxWatched.ItemsSource = WatchedMovies;

            lbxWatchedAll.ItemsSource = null;
            lbxWatchedAll.ItemsSource = WatchedMovies;
        }

        // Display movie description under watchlist on selected movie
        private void lbxWatchlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WatchedMovies selectMovie = lbxWatchlist.SelectedItem as WatchedMovies;
                if (selectMovie != null)
                {
                    tblkDescription.Text = selectMovie.MovieDescription;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error displaying the movie description" + ex.Message);

            }
        }

        // Sort by genre when selection changes on watchlist
        private void CbxGenreSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int counter = 0;

                string selectedGenre = cbxGenreSort.SelectedItem as string;
                FilterWatchlist.Clear();

                foreach (WatchedMovies movie in AllMovies)
                {
                    if (movie.MovieGenre == selectedGenre)
                    {
                        FilterWatchlist.Add(movie);
                        counter = 1;
                    }
                    else if (selectedGenre == "Select...")
                    {
                        FilterWatchlist.Clear();
                        lbxWatchlist.ItemsSource = AllMovies;
                        counter = 2;
                    }

                    //else if (movie.MovieGenre != selectedGenre)
                    //{
                    //    //MessageBox.Show("There are no movies of this genre");
                    //    counter = 3;
                    //}
                }

                if (counter == 1)
                {
                    // Set the source after the foreach after the list has been filled
                    lbxWatchlist.ItemsSource = null;
                    lbxWatchlist.ItemsSource = FilterWatchlist;
                }

                else if (counter == 2)
                {
                    lbxWatchlist.ItemsSource = null;
                    lbxWatchlist.ItemsSource = AllMovies;
                }

                //else if(counter == 3)
                //{
                //    MessageBox.Show("There are no movies of this genre");
                //    lbxWatchlist.ItemsSource = null;
                //}

                //string selectedGenre = cbxGenreSort.SelectedItem as string;
                //int counter = 0;

                //switch(selectedGenre)
                //{
                //    case "Select...":
                //        //FilterWatchlist.Clear();
                //        lbxWatchlist.ItemsSource = AllMovies;
                //        break;

                //    case "Horror":
                //        //FilterWatchlist.Clear();
                //        foreach (WatchedMovies movie in AllMovies)
                //        {
                //            //FilterWatchlist.Clear();
                //            if (movie.MovieGenre == "Horror")
                //            {
                //                //FilterWatchlist.Add(movie);
                //                FilteredWatchlist[counter] = movie;
                //                counter++;
                //            }
                //        }

                //        lbxWatchlist.ItemsSource = FilteredWatchlist;
                //        break;

                //    case "Comedy":
                //        //FilterWatchlist.Clear();
                //        foreach (WatchedMovies movie in AllMovies)
                //        {

                //            //FilterWatchlist.Clear();
                //            if (movie.MovieGenre == "Comedy")
                //            {
                //                //FilterWatchlist.Add(movie);
                //                FilteredWatchlist[counter] = movie;
                //                counter++;
                //            }
                //        }

                //        lbxWatchlist.ItemsSource = FilteredWatchlist;
                //        break;

                //    case "Action":
                //        //FilterWatchlist.Clear();
                //        foreach (WatchedMovies movie in AllMovies)
                //        {
                //            //FilterWatchlist.Clear();
                //            if (movie.MovieGenre == "Action")
                //            {
                //                //FilterWatchlist.Add(movie);
                //                FilteredWatchlist[counter] = movie;
                //                counter++;
                //            }
                //        }

                //        lbxWatchlist.ItemsSource = FilteredWatchlist;
                //        break;

                //    case "Romance":
                //        //FilterWatchlist.Clear();
                //        foreach (WatchedMovies movie in AllMovies)
                //        {
                //            //FilterWatchlist.Clear();
                //            if (movie.MovieGenre == "Romance")
                //            {
                //                //FilterWatchlist.Add(movie);
                //                FilteredWatchlist[counter] = movie;
                //                counter++;
                //            }
                //        }

                //        lbxWatchlist.ItemsSource = FilteredWatchlist;
                //        break;

                //    case "Superhero":
                //        //FilterWatchlist.Clear();
                //        foreach (WatchedMovies movie in AllMovies)
                //        {
                //            //FilterWatchlist.Clear();
                //            if (movie.MovieGenre == "Superhero")
                //            {
                //                //FilterWatchlist.Add(movie);
                //                FilteredWatchlist[counter] = movie;
                //                counter++;
                //            }
                //        }

                //        lbxWatchlist.ItemsSource = FilteredWatchlist;
                //        break;

                //    case "Drama":
                //        //FilterWatchlist.Clear();
                //        foreach (WatchedMovies movie in AllMovies)
                //        {
                //            //FilterWatchlist.Clear();
                //            if (movie.MovieGenre == selectedGenre)
                //            {
                //                //FilterWatchlist.Add(movie);
                //                FilteredWatchlist[counter] = movie;
                //                counter++;
                //            }
                //        }

                //        lbxWatchlist.ItemsSource = FilteredWatchlist;
                //        break;
                //}

            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error sorting by genre" + ex.Message);

            }
        }

        // Sort by rating on watched list
        private void CbxRatingFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selectedRating = cbxRatingFilter.SelectedItem as string;
                int counter = 0;

                FilterWatched.Clear();

                foreach (WatchedMovies movie in WatchedMovies)
                {
                    if (movie.MovieRating == selectedRating)
                    {
                        FilterWatched.Add(movie);
                        counter = 1;

                    }

                    else if (movie.MovieRating == "Select...")
                    {
                        FilterWatched.Clear();
                        lbxWatched.ItemsSource = WatchedMovies;
                        counter = 2;
                    }
                }

                if(counter == 1)
                {
                    lbxWatched.ItemsSource = null;
                    lbxWatched.ItemsSource = FilterWatched;
                }
                
                else if(counter == 2)
                {
                    lbxWatched.ItemsSource = null;
                    lbxWatched.ItemsSource = WatchedMovies;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error sorting by genre" + ex.Message);

            }
        }

        private void LbxWatchedAll_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                WatchedMovies selectMovie = lbxWatchedAll.SelectedItem as WatchedMovies;

                tblkMovieList.Text = selectMovie.MovieName;
                tblkMovieGenre.Text = selectMovie.MovieGenre;
                tblkMovieDescription.Text = selectMovie.MovieDescription;
                tblkMovieRating.Text = selectMovie.MovieRating;
                tblkMovieDate.Text = selectMovie.DateWatched.ToShortDateString();
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error displaying the watchlist" + ex.Message);

            }
        }

        private void BtnSelectRandom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int sizel = AllMovies.Count;
                int randomID = rng.Next(0, AllMovies.Count);

                string MovieText = $"Movie Name: {AllMovies[randomID].MovieName}\nMovie Genre: {AllMovies[randomID].MovieGenre}\nMovie Description: {AllMovies[randomID].MovieDescription}";

                tblkMovieInfo.Text = MovieText;
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error selecting a random movie from the watchlist" + ex.Message);

            }
        }

        private void CbxGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selectedGenre = cbxGenre.SelectedItem as string;

                if (selectedGenre == "Horror")
                {
                    tblkGenreHeading.Text = "Horror Movie";
                    imgGenre.Source = new BitmapImage(new Uri($"/images/Horror.jpg", UriKind.Relative));
                }

                else if (selectedGenre == "Comedy")
                {
                    tblkGenreHeading.Text = "Comedy Movie";
                    imgGenre.Source = new BitmapImage(new Uri($"/images/Comedy.jpg", UriKind.Relative));
                }

                else if (selectedGenre == "Action")
                {
                    tblkGenreHeading.Text = "Action Movie";
                    imgGenre.Source = new BitmapImage(new Uri($"/images/Action.jpg", UriKind.Relative));
                }

                else if (selectedGenre == "Romance")
                {
                    tblkGenreHeading.Text = "Romance Movie";
                    imgGenre.Source = new BitmapImage(new Uri($"/images/Romance.jpg", UriKind.Relative));
                }

                else if (selectedGenre == "Superhero")
                {
                    tblkGenreHeading.Text = "Superhero Movie";
                    imgGenre.Source = new BitmapImage(new Uri($"/images/Superhero.jpg", UriKind.Relative));
                }

                else if (selectedGenre == "Drama")
                {
                    tblkGenreHeading.Text = "Drama Movie";
                    imgGenre.Source = new BitmapImage(new Uri($"/images/Drama.jpg", UriKind.Relative));
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error displaying the genre image" + ex.Message);

            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                string UnwatchedMoviesJSON = JsonConvert.SerializeObject(AllMovies, Formatting.Indented);

                using (StreamWriter sw = new StreamWriter("c:/temp/UnwatchedMoviesData.json"))
                {
                    sw.Write(UnwatchedMoviesJSON);
                    sw.Close();
                }

                string WatchedMoviesJSON = JsonConvert.SerializeObject(WatchedMovies, Formatting.Indented);

                using (StreamWriter sw = new StreamWriter("c:/temp/WatchedMoviesData.json"))
                {
                    sw.Write(WatchedMoviesJSON);
                    sw.Close();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error writing to a JSON File" + ex.Message);

            }
        }

        private void CbxFilterGenre_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int counter = 0;

                string selectedGenre = cbxFilterGenre.SelectedItem as string;
                FilterAllWatched.Clear();

                foreach (WatchedMovies movie in WatchedMovies)
                {
                    if (movie.MovieGenre == selectedGenre)
                    {
                        FilterAllWatched.Add(movie);
                        counter = 1;
                    }
                    else if (selectedGenre == "Select...")
                    {
                        FilterAllWatched.Clear();
                        lbxWatchedAll.ItemsSource = WatchedMovies;
                        counter = 2;
                    }
                }

                if (counter == 1)
                {
                    // Set the source after the foreach after the list has been filled
                    lbxWatchedAll.ItemsSource = null;
                    lbxWatchedAll.ItemsSource = FilterAllWatched;
                }

                else if (counter == 2)
                {
                    lbxWatchedAll.ItemsSource = null;
                    lbxWatchedAll.ItemsSource = WatchedMovies;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("There was an error sorting by genre" + ex.Message);

            }
        }
    }
}
