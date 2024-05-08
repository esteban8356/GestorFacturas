using System.Data.SqlClient;
using Insight.Database;
using Microsoft.AspNetCore.Mvc;
using GestorFacturas.Shared.Entidades;
using System.Threading.Tasks;

namespace GestorFacturas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly string _connectionString;

        public ClienteController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetCliente()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var clientes = await connection.QuerySqlAsync<Cliente>("SELECT * FROM Clientes");
                return Ok(clientes);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> CreateCliente([FromBody] Cliente cliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Clientes (ClienteName, ClienteEmail) VALUES (@ClienteName, @ClienteEmail); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                var id = await connection.ExecuteScalarSqlAsync<int>(sql, cliente);
                cliente.Id = id; 
                return Ok(cliente);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Cliente>> UpdateCliente(int id, [FromBody] Cliente updatedCliente)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Clientes SET ClienteName = @ClienteName, ClienteEmail = @ClienteEmail WHERE Id = @Id;";
                await connection.ExecuteSqlAsync(sql, updatedCliente);
                return Ok(updatedCliente);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCliente(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Clientes WHERE Id = @Id";
                await connection.ExecuteSqlAsync(sql, new { Id = id });
                return Ok();
            }
        }
    }
}
