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

        public Movies(string movieName, string movieGenre, string movieDescription)
        {
            MovieName = movieName;
            MovieGenre = movieGenre;
            MovieDescription = movieDescription;
        }

        public override string ToString()
        {
            return $"{MovieName}";
        }
    }

    public class MoviesWatched : Movies
    {
        public MoviesWatched(string movieName, string movieGenre, string movieDescription) : base(movieName, movieGenre, movieDescription)
        {

        }
        public string MovieRating { get; set; }

        public override string ToString()
        {
            return (this.MovieName + " - " + MovieRating);
        }
    }
}
