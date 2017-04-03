using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAndPersonRepoLib
{
    public interface IReviewsRepository
    {
        List<Review> GetAllReviews();
        int Create(Review Review);
        Review GetReview(int Id);
        Review Update(Review Review);
        bool DeleteReview(int Id);


        List<Person> GetAllPeople();
        int Create(Person Person);
        Person GetPerson(int Id);
        Person Update(Person Person);
        bool DeletePerson(int Id);
    }
}
