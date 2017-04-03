using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace ReviewsAndPeopleService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IReviewService
    {
        [OperationContract]
        List<Review> GetAllReviews();

        [OperationContract]
        int CreateReview(Review Review);

        [OperationContract]
        Review GetReview(int Id);

        [OperationContract]
        Review UpdateReview(Review Review);

        [OperationContract]
        bool DeleteReview(int Id);

        [OperationContract]
        List<Person> GetAllPeople();

        [OperationContract]
        int CreatePerson(Person Person);

        [OperationContract]
        Person GetPerson(int Id);

        [OperationContract]
        Person UpdatePerson(Person Person);

        [OperationContract]
        bool DeletePerson(int Id);

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "ReviewsAndPeopleService.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
