using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public class MockDbService : iDbService
    {
        private static IEnumerable<Śtudent> _students;

        static MockDbService()
        {
            _students = new List<Śtudent>
            {
                new Śtudent{IdStudent=1, FirstName="Jan", LastName="Kowalksi"},
                new Śtudent{IdStudent=2, FirstName="Anna", LastName="Malewska"},
                new Śtudent{IdStudent=1, FirstName="Andrzej", LastName="Andrzejow"},
            };
        }

        public IEnumerable<Śtudent> GetStudents()
        {
            return _students;
        }
    }
}
