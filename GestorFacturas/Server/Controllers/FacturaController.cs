using System.Data.SqlClient;
using Insight.Database;
using Microsoft.AspNetCore.Mvc;
using GestorFacturas.Shared.Entidades;
using System.Threading.Tasks;

namespace GestorFacturas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly string _connectionString;

        public FacturaController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public async Task<ActionResult<List<Factura>>> GetFacturas()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var facturas = await connection.QuerySqlAsync<Factura>("SELECT * FROM Facturas");
                return Ok(facturas);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Factura>> CreateFactura([FromBody] Factura factura)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Facturas (Fecha, ClienteId, Total) VALUES (@Fecha, @ClienteId, @Total); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                var id = await connection.ExecuteScalarSqlAsync<int>(sql, factura);
                factura.Id = id; 
                return Ok(factura);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Factura>> UpdateFactura(int id, [FromBody] Factura updatedFactura)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Facturas SET Fecha = @Fecha, ClienteId = @ClienteId, Total = @Total WHERE Id = @Id;";
                await connection.ExecuteSqlAsync(sql, updatedFactura);
                return Ok(updatedFactura);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFactura(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Facturas WHERE Id = @Id";
                await connection.ExecuteSqlAsync(sql, new { Id = id });
                return Ok();
            }
        }
    }
}
