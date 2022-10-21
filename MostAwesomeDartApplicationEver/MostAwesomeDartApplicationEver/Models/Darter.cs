using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MostAwesomeDartApplicationEver.Models
{
    public class Darter
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Darter()
        {
            FirstName = "";
            LastName = "";
        }

        public Darter(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }    
}
