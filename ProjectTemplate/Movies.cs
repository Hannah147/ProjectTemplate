using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTemplate
{
    public class Movies
    {
        public string MovieName { get; set; }
        public string MovieGenre { get; set; }
        public string MovieDescription { get; set; }
        public string MovieRating { get; set; }

        public Movies(string movieName, string movieGenre, string movieDescription, string movieRating)
        {
            MovieName = movieName;
            MovieGenre = movieGenre;
            MovieDescription = movieDescription;
            MovieRating = movieRating;
        }

        public override string ToString()
        {
            return $"{MovieName}";
        }
    }

    /*public class MoviesWatched : Movies
    { 
        public MoviesWatched(string movieName, string movieGenre, string movieDescription, string movieRating) : base(movieName, movieGenre, movieDescription, movieRating)
        {
            MovieName = movieName;
            MovieGenre = movieGenre;
            MovieDescription = movieDescription;
            MovieRating = movieRating;
        }
        

        public override string ToString()
        {
            return (MovieName + " - " + MovieRating);
        }
    }*/
}
