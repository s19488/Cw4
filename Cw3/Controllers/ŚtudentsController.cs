using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class ŚtudentsController : ControllerBase
    {
        private readonly iDbService _dbService;

        public ŚtudentsController(iDbService dbService)
        {
            _dbService = dbService;
        }

       [HttpGet]
       public IActionResult GetStudents(string orederBy)
        {
            var list = new List< Śtudent>();

            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19488;Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = client;
                command.CommandText = "select * from Student";
                client.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    while (reader.Read())
                    {
                        list.Add(new Śtudent()
                        {
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            IndexNumber = reader["IndexNumber"].ToString(),
                            BirthDate = DateTime.Parse(reader["BirthDate"].ToString())

                        });

                    }
                }

            }


            return Ok(_dbService.GetStudents());
        }

        [HttpPut]
        public IActionResult PutStudents(string orederBy)
        {
            return Ok("200");
        }

        [HttpDelete]
        public IActionResult DeleteStudents(string orederBy)
        {
            return Ok("aktualizacja zakonczona");
        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(string id)
        {
            var list = new List<Object>();
            using (var client = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19488; Integrated Security=True"))
            using (var command = new SqlCommand())
            {
                command.Connection = client;

                command.CommandText = "select e.Semester Semester, " +
                                        "st.Name StudiesName from Student s" +
                                        "inner join Enrollment e on s.IdEnrollment=e.IdEnrollment" +
                                        "inner join Studies st on e.IdStudy=st.IdStudy" +
                                        $"where s.IndexNumber=@id";
                command.Parameters.AddWithValue("id", id);


                client.Open();
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var studia = new
                    {
                        Semestr = reader["Semester"].ToString(),
                        StudiesName = reader["StudiesName"].ToString()
                    };
                    list.Add(studia);
                }
            }

            return Ok(list);
        }

        [HttpPost]

        public IActionResult CreateStudent(Śtudent student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

    }
}