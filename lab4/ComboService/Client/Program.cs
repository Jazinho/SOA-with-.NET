//using Client.ServiceReference1;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.MovieServiceClient MoviesClient = new ServiceReference1.MovieServiceClient();
            ServiceReference2.ReviewServiceClient ReviewClient = new ServiceReference2.ReviewServiceClient();
            string key;
            bool condition = true;

            Movie[] m = MoviesClient.GetAll();

            if (m.Length == 0)
            {
                Movie m1 = new Movie();
                m1.Id = 1;
                m1.Title = "Braveheart";
                m1.ReleaseYear = 1999;
                Movie m2 = new Movie();
                m2.Id = 2;
                m2.Title = "Desperado";
                m2.ReleaseYear = 2004;
                Movie m3 = new Movie();
                m3.Id = 3;
                m3.Title = "Sztuka kochania";
                m3.ReleaseYear = 2017;
                MoviesClient.Create(m1);
                MoviesClient.Create(m2);
                MoviesClient.Create(m3);
            }

            Console.WriteLine("Type your name:");
            String name = Console.ReadLine();
            Console.WriteLine("Type your surname:");
            String surname = Console.ReadLine();

            var l = ReviewClient.GetAllPeople().ToList();
            var user = l.Find(x => (x.Name.Equals(name) && x.Surname.Equals(surname)));

            if (user == null)
            {
                user = new Person();
                user.Id = l.Count == 0 ? 1 : l.Last().Id + 1;
                user.Name = name;
                user.Surname = surname;
                ReviewClient.CreatePerson(user);
            }

            while (condition)
            {
                Console.WriteLine("Press 'a' to add new review");
                Console.WriteLine("Press 'e' to edit your review");
                Console.WriteLine("Press 'd' to delete your review");
                Console.WriteLine("Press 'm' to create new movie");
                Console.WriteLine("Press 'g' to get movies reviews");
                Console.WriteLine("Press 'x' to exit");
                key = Console.ReadLine();
                if (key.Equals("m"))
                {
                    Console.WriteLine("Set the title:");
                    var title = Console.ReadLine();
                    Console.WriteLine("Set the release year:");
                    var year = Int16.Parse(Console.ReadLine());
                    var mo = new Movie();
                    mo.Id = MoviesClient.GetAll() == null ? 1 : MoviesClient.GetAll().Last().Id + 1;
                    mo.Title = title;
                    mo.ReleaseYear = year;

                    MoviesClient.Create(mo);
                }
                else if (key.Equals("a"))
                {
                    var movies = MoviesClient.GetAll();
                    for (int i = 0; i < movies.Length; i++)
                    {
                        Console.WriteLine("{0}. {1}, {2}", i + 1, movies[i].Title, movies[i].ReleaseYear);
                    }
                    Console.WriteLine("Review of which film would you like to add? (type number)");
                    var id = Int16.Parse(Console.ReadLine());

                    if (id <= movies.Length)
                    {
                        Console.WriteLine("Set the grade of a film: (0-100)");
                        var grade = Int16.Parse(Console.ReadLine());
                        Console.WriteLine("Set a review content:");
                        var content = Console.ReadLine();

                        var r = new Review();
                        var reviews = ReviewClient.GetAllReviews();
                        r.Id = reviews.Length == 0 ? 1 : reviews.Last().Id + 1;
                        r.Movie = movies[id - 1];
                        r.MovieId = movies[id - 1].Id;
                        r.Score = grade;
                        r.Content = content;
                        r.Author = user;

                        ReviewClient.CreateReview(r);
                    }
                }
                else if (key.Equals("e"))
                {
                    var reviewsList = ReviewClient.GetAllReviews().ToList();
                    var reviews = new Review[reviewsList.Count()];
                    var ind = 0;
                    reviewsList.ForEach(x => { if (x.Author.Id == user.Id) { reviews[ind] = x; ind++; } });
                    if (reviews.Length == 0)
                        Console.WriteLine("You have no reviews");
                    else
                    {
                        for (int i = 0; i < reviews.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}, grade: {2}", i + 1, reviews[i].Movie.Title, reviews[i].Score);
                            Console.WriteLine(reviews[i].Content);
                        }
                        Console.WriteLine("Which review would you like to edit? (type number)");
                        var id = Int32.Parse(Console.ReadLine());

                        if (id <= reviews.Length)
                        {
                            var edited = reviews[id - 1];

                            Console.WriteLine("Film title: {0}", edited.Movie.Title);
                            Console.WriteLine("Grade: {0}", edited.Score);
                            Console.WriteLine("Review content:\n {0}", edited.Content);

                            Console.WriteLine("Set new grade: (0-100)");
                            var grade = Int16.Parse(Console.ReadLine());
                            edited.Score = grade;

                            Console.WriteLine("Set new content:");
                            var content = Console.ReadLine();
                            edited.Content = content;

                            ReviewClient.UpdateReview(edited);
                        }
                    }
                }
                else if (key.Equals("d"))
                {
                    var reviews = ReviewClient.GetAllReviews();
                    if (reviews.Length == 0)
                        Console.WriteLine("You have no reviews.");
                    else
                    {
                        for (int i = 0; i < reviews.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}, grade: {2}", i + 1, reviews[i].Movie.Title, reviews[i].Score);
                            Console.WriteLine(reviews[i].Content);
                        }
                        Console.WriteLine("Which review would you like to delete? (type number)");
                        var id = Int32.Parse(Console.ReadLine());

                        if (id <= reviews.Length)
                        {
                            Console.WriteLine("Are you sure? (Y/N)");
                            var dec = Console.ReadLine();
                            if (dec.Equals("Y"))
                                ReviewClient.DeleteReview(reviews[id - 1].Id);
                        }
                    }
                }
                else if (key.Equals("g"))
                {
                    var movies = MoviesClient.GetAll();
                    if (movies.Length > 0)
                    {
                        for (int i = 0; i < movies.Length; i++)
                        {
                            Console.WriteLine("{0}. {1}, {2}", i + 1, movies[i].Title, movies[i].ReleaseYear);
                        }
                        Console.WriteLine("Reviews of which film would you like to see? (type number)");
                        var id = Int16.Parse(Console.ReadLine());

                        if (id <= movies.Length)
                        {
                            var reviews = ReviewClient.GetAllReviews().ToList()
                                .FindAll(x => (x.Movie.Id == movies[id - 1].Id));

                            if (reviews.Count == 0)
                                Console.WriteLine("No reviews found.");
                            else
                            {
                                var sum = 0;
                                reviews.ForEach(x => { Console.WriteLine("'{0}' ~ {1} {2}", x.Content, x.Author.Name, x.Author.Surname); sum += x.Score; });
                                Console.WriteLine("Average grade for this film is: {0}", sum / reviews.Count);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No films in database.");
                    }
                }
                else if (key.Equals("x"))
                    condition = false;
                else
                {
                    Console.WriteLine("Unknown command.");
                    condition = false;
                }
            }
        }
    }
}
