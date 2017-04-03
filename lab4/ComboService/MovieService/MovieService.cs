using Model;
using MovieRepoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MovieService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MovieService : IMovieService
    {
        private readonly MoviesRepository _movieRepoLib;

        public MovieService()
        {
            _movieRepoLib = new MoviesRepository();
        }

        public List<Movie> GetAll()
        {
            return _movieRepoLib.GetAll();
        }

        public int Create(Movie Movie)
        {
            return _movieRepoLib.Create(Movie);
        }

        public Movie Get(int Id)
        {
            return _movieRepoLib.Get(Id);
        }

        public Movie Update(Movie Movie)
        {
            return _movieRepoLib.Update(Movie);
        }

        public bool Delete(int Id)
        {
            return _movieRepoLib.Delete(Id);
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
