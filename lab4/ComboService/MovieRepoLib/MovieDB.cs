using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoLib
{
    public class MovieDB
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }

        public MovieDB() { }

        /*public MovieDB(Movie M)
        {
            this.Id = M.Id;
            this.Title = M.Title;
            this.ReleaseYear = M.ReleaseYear;
        }*/
    }
}
