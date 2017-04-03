using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoLib
{
    public interface IMoviesRepository
    {
        List<Movie> GetAll();
        int Create(Movie Movie);
        Movie Get(int Id);
        Movie Update(Movie Movie);
        bool Delete(int Id);
    }
}
