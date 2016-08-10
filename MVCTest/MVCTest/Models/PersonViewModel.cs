using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCTest.DAL;

namespace MVCTest.Models
{
    public class PersonViewModel :Person
    {
        public PersonViewModel()
        {
            Persons=new List<Person>();
        }

        public List<Person> Persons { get; set; }

      

    }
}