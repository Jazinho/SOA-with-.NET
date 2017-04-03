using LiteDB;
using Model;
using MovieRepoLib;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReviewAndPersonRepoLib
{
    public class ReviewRepository : IReviewsRepository
    {
        static string currentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        static string parentPath = Path.GetFullPath(Path.Combine(currentPath, "..\\..\\.."));
        private readonly string ReviewsConnection = parentPath + @"\reviews.db";

        public List<Review> GetAllReviews()
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<ReviewDB>("reviews");
                var res = repo.FindAll();

                var list =  res.Select(x => MapToReview(x) ).ToList();

                var repoP = db.GetCollection<PersonDB>("people");
                foreach(Review r in list)
                {
                    var id = r.Author.Id;
                    r.Author = MapToPerson(repoP.FindById(id));
                }
                return list;
            }
        }

        public List<Person> GetAllPeople()
        {
            using(var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<PersonDB>("people");
                var res = repo.FindAll();

                return res.Select(x => MapToPerson(x)).ToList();
            }
        }

        public Review GetReview(int Id)
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<ReviewDB>("reviews");
                var get = repo.FindById(Id);
                var mapped = MapToReview(get);

                var repo2 = db.GetCollection<PersonDB>("people");
                var id = mapped.Author.Id;
                mapped.Author = MapToPerson(repo2.FindById(id));

                return mapped;
            }
        }

        public Person GetPerson(int Id)
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<PersonDB>("people");
                var res = repo.FindById(Id);
                return MapToPerson(res);
            }
        }

        public int Create(Review R)
        {
            using(var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<ReviewDB>("reviews");
                if (repo.FindById(R.Id) != null)
                {
                    var upd = MapToReviewDB(R);
                    repo.Update(upd);
                }
                else
                {
                    var upd = MapToReviewDB(R);
                    repo.Insert(upd);
                }

                return R.Id;
            }
        }

        public int Create(Person P)
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<PersonDB>("people");
                if (repo.FindById(P.Id) != null)
                {
                    var upd = MapToPersonDB(P);
                    repo.Update(upd);
                }
                else
                {
                    var upd = MapToPersonDB(P);
                    repo.Insert(MapToPersonDB(P));
                }

                return P.Id;
            }
        }

        public Review Update(Review R)
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<ReviewDB>("reviews");
                var dbObj = MapToReviewDB(R);
                if (repo.Update(dbObj))
                    return R;
                else
                    return null;
            }
        }

        public Person Update(Person P)
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<PersonDB>("people");
                var dbObj = MapToPersonDB(P);
                if (repo.Update(dbObj))
                    return MapToPerson(dbObj);
                else
                    return null;
            }
        }

        public bool DeleteReview(int Id)
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<ReviewDB>("reviews");
                return repo.Delete(Id);
            }
        }

        public bool DeletePerson(int Id)
        {
            using (var db = new LiteDatabase(ReviewsConnection))
            {
                var repo = db.GetCollection<PersonDB>("people");
                return repo.Delete(Id);
            }
        }

        internal Review MapToReview(ReviewDB R)
        {
            if (R == null)
                return null;
            var MoviesRepo = new MoviesRepository();
            var Mov = MoviesRepo.Get(R.MovieId);
            var a = new Person();
            a.Id = R.Author;
            return new Review() { Id = R.Id, Author = a, Content = R.Content, Movie = Mov, Score = R.Score, MovieId = Mov.Id };
        }

        internal ReviewDB MapToReviewDB(Review R)
        {
            if (R == null)
                return null;
            return new ReviewDB() { Id = R.Id, Author = R.Author.Id, MovieId = R.Movie.Id, Movie = R.Movie.Id, Score = R.Score, Content = R.Content };
        }

        internal Person MapToPerson(PersonDB P)
        {
            if (P == null)
                return null;
            return new Person() { Id = P.Id, Name = P.Name, Surname = P.Surname };
        }

        internal PersonDB MapToPersonDB(Person P)
        {
            if (P == null)
                return null;
            return new PersonDB() { Id = P.Id, Surname = P.Surname, Name = P.Name };
        }
    }
}
