using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;
using ASP_Reactjs.Models;

namespace ASP_Reactjs.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT DepartmentID, DepartmentName from Department";

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
        public JsonResult Post(Department department)
        {
            string query = @"INSERT INTO Department (DepartmentName) VALUES (@DepartmentName);";

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
                    myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);

                    myReader = myCommand.ExecuteReader();

                    table.Load(myReader);
                    
                    myReader.Close();
                    myConnection.Close();
                }
            }
            
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department department)
        {
            string query = @"UPDATE Department 
                            SET DepartmentName = @DepartmentName
                            WHERE DepartmentID = @DepartmentID";

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
                    myCommand.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                    myCommand.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);

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
            string query = @"DELETE FROM Department
                            WHERE DepartmentID = @DepartmentID";

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
                    myCommand.Parameters.AddWithValue("@DepartmentID", id);

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