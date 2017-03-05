using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoUniversity.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Person> Members { get; set; }


    }
}