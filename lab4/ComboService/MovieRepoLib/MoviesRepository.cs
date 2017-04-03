using LiteDB;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MovieRepoLib
{
    public class MoviesRepository : IMoviesRepository
    {
        static string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string parentPath = Path.GetFullPath(Path.Combine(currentPath, "..\\..\\.."));
        private readonly string MoviesConnection = parentPath + @"\movies.db";
        public List<Movie> GetAll()
        {
            using (var db = new LiteDatabase(MoviesConnection))
            {
                var repo = db.GetCollection<MovieDB>("movies");
                var res = repo.FindAll();

                return res.Select(x => Map(x)).ToList();
            }
        }

        public int Create(Movie Movie)
        {
            using (var db = new LiteDatabase(MoviesConnection))
            {
                var repo = db.GetCollection<MovieDB>("movies");
                var dbObj = InvMap(Movie);

                if (repo.FindById(Movie.Id) == null)
                    repo.Insert(dbObj);
                else
                    repo.Update(dbObj);

                return dbObj.Id;
            }
        }

        public Movie Get(int Id)
        {
            using (var db = new LiteDatabase(MoviesConnection))
            {
                var repo = db.GetCollection<MovieDB>("movies");
                return Map(repo.FindById(Id));
            }
        }

        public Movie Update(Movie Movie)
        {
            using (var db = new LiteDatabase(MoviesConnection))
            {
                var repo = db.GetCollection<MovieDB>("movies");
                var dbObj = InvMap(Movie);
                if (repo.Update(dbObj))
                    return Map(dbObj);
                else
                    return null;
            }
        }

        public bool Delete(int Id)
        {
            using (var db = new LiteDatabase(MoviesConnection))
            {
                var repo = db.GetCollection<MovieDB>("movies");
                return repo.Delete(Id);
            }
        }

        internal Movie Map(MovieDB MovDB)
        {
            if (MovDB == null)
                return null;
            return new Movie() { Id = MovDB.Id, Title = MovDB.Title, ReleaseYear = MovDB.ReleaseYear };
        }

        internal MovieDB InvMap(Movie M)
        {
            if (M == null)
                return null;
            return new MovieDB() { Id = M.Id, Title = M.Title, ReleaseYear = M.ReleaseYear };
        }
    }
}
