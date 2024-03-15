using System.Data;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Http;

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
            string query = @"SELECT DepartmentID, DepartmentName from Department";

            DataTable table = new DataTable();

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
            // return Ok("CON CAC");
        }
    }
}