using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTemplate
{
    public partial class WatchedMovies
    {
        public WatchedMovies(string movieName, string movieGenre, string movieDescription, string movieRating, DateTime dateWatched)
        {
            MovieName = movieName;
            MovieGenre = movieGenre;
            MovieDescription = movieDescription;
            MovieRating = movieRating;
            DateWatched = DateWatched;
        }

        public override string ToString()
        {
            return $"{MovieName}";
        }
    }
}
