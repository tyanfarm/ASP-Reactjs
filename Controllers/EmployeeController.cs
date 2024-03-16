using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using ASP_Reactjs.Models;

namespace ASP_Reactjs.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase {
        private readonly IConfiguration _configuration;

        public EmployeeController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT ID, employeeName, Department,
                            DATE_FORMAT(DateOfJoining,'%Y-%m-%d') AS DateOfJoining, PhotoFile 
                            FROM employee";

            DataTable table = new DataTable();

            // Kết nối đến CSDL MYSQL
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            MySqlDataReader myReader;

            using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource))
            {
                myConnection.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);
                    
                    myReader.Close();
                    myConnection.Close();
                }
            }
            
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            string query = @"INSERT INTO employee (employeeName, Department, DateOfJoining, PhotoFile) 
                            VALUES (@employeeName, @Department, @DateOfJoining, @PhotoFile)";

            DataTable table = new DataTable();

            // Kết nối đến CSDL MYSQL
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            MySqlDataReader myReader;

            using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource))
            {
                myConnection.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, myConnection))
                {
                    // Add variable
                    myCommand.Parameters.AddWithValue("@employeeName", employee.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", employee.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFile", employee.PhotoFile);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);
                    
                    myReader.Close();
                    myConnection.Close();
                }
            }
            
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            string query = @"UPDATE employee 
                            SET employeeName = @employeeName,
                                Department = @Department,
                                DateOfJoining = @DateOfJoining,
                                PhotoFile = @PhotoFile
                            WHERE ID = @employeeID";

            DataTable table = new DataTable();

            // Kết nối đến CSDL MYSQL
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            MySqlDataReader myReader;

            using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource))
            {
                myConnection.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, myConnection))
                {
                    // Add variable
                    myCommand.Parameters.AddWithValue("@employeeID", employee.EmployeeId);
                    myCommand.Parameters.AddWithValue("@employeeName", employee.EmployeeName);
                    myCommand.Parameters.AddWithValue("@Department", employee.Department);
                    myCommand.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    myCommand.Parameters.AddWithValue("@PhotoFile", employee.PhotoFile);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);
                    
                    myReader.Close();
                    myConnection.Close();
                }
            }
            
            return new JsonResult("Update Successfully");
        }

        // api/Department/id
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"DELETE FROM employee
                            WHERE ID = @employeeID";

            DataTable table = new DataTable();

            // Kết nối đến CSDL MYSQL
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");

            MySqlDataReader myReader;

            using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource))
            {
                myConnection.Open();

                using (MySqlCommand myCommand = new MySqlCommand(query, myConnection))
                {
                    // Add variable
                    myCommand.Parameters.AddWithValue("@employeeID", id);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);
                    
                    myReader.Close();
                    myConnection.Close();
                }
            }
            
            return new JsonResult("Deleted Successfully");
        }
    }
}