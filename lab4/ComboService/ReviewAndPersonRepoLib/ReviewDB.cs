using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewAndPersonRepoLib
{
    public class ReviewDB
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public int Movie { get; set; }
        public int Author { get; set; }
        public int MovieId { get; set; }
    }
}
