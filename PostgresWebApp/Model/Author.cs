using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }
    }
}
