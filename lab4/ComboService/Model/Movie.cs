using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }

        public Movie() { }

        /*public Movie(MovieDB M)
        {
            this.Id = M.Id;
            this.Title = M.Title;
            this.ReleaseYear = M.ReleaseYear;
        }*/
    }
}
