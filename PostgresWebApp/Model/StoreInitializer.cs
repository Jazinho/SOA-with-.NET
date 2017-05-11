using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public class StoreInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            var books = new List<Book>
            {
                new Book() { BookTitle = "Winetou", ISBN = "123-123-123" },
                new Book() { BookTitle = "Ania z Zielonego Wzgorza", ISBN = "321-321-321" }
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();

            var authors = new List<Author>
            {
                new Author() { AuthorName = "Jacek", AuthorSurname = "Placek" },
                new Author() { AuthorName = "Zbigniew", AuthorSurname = "Stonoga" }
            };
            authors.ForEach(a => context.Authors.Add(a));
            context.SaveChanges();
        }
    }
}
