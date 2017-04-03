using Model;
using ReviewAndPersonRepoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ReviewsAndPeopleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ReviewService : IReviewService
    {
        private readonly ReviewRepository _reviewRepo;

        public ReviewService()
        {
            _reviewRepo = new ReviewRepository();
        }

        public List<Review> GetAllReviews()
        {
            return _reviewRepo.GetAllReviews();
        }

        public List<Person> GetAllPeople()
        {
            return _reviewRepo.GetAllPeople();
        }

        public int CreateReview(Review R)
        {
            return _reviewRepo.Create(R);
        }

        public int CreatePerson(Person P)
        {
            return _reviewRepo.Create(P);
        }

        public Review GetReview(int Id)
        {
            return _reviewRepo.GetReview(Id);
        }

        public Person GetPerson(int Id)
        {
            return _reviewRepo.GetPerson(Id);
        }

        public Review UpdateReview(Review R)
        {
            return _reviewRepo.Update(R);
        }

        public Person UpdatePerson(Person P)
        {
            return _reviewRepo.Update(P);
        }

        public bool DeleteReview(int Id)
        {
            return _reviewRepo.DeleteReview(Id);
        }

        public bool DeletePerson(int Id)
        {
            return _reviewRepo.DeletePerson(Id);
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
