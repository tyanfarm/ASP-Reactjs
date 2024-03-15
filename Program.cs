using Newtonsoft.Json.Serialization;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Enable CORS
// cho phép truy cập từ mọi nguồn gốc
builder.Services.AddCors(c => {
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// JSON Serializer  
// đảm bảo rằng việc serialize và deserialize đối tượng JSON
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
.AddNewtonsoftJson(options => 
    options.SerializerSettings.ContractResolver = new DefaultContractResolver());

var app = builder.Build();

// Enable CORS
app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.MapGet("/validateConnection", async (context) => {
    var configuration = context.RequestServices.GetService<IConfiguration>();

    string sqlDataSource = configuration.GetConnectionString("DefaultConnection");

    try {
        using (MySqlConnection myConnection = new MySqlConnection(sqlDataSource)) {
            myConnection.Open();
            await context.Response.WriteAsync("Connected to the database successfully.");
        }
    } 
    catch (Exception ex) {
        await context.Response.WriteAsync("Error connecting to the database: " + ex.Message);
    }
});

app.Run();