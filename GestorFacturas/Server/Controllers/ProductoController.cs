using System.Data.SqlClient;
using Insight.Database;
using Microsoft.AspNetCore.Mvc;
using GestorFacturas.Shared.Entidades;
using System.Threading.Tasks;

namespace GestorFacturas.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly string _connectionString;

        public ProductoController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
          
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProducto()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var productos = await connection.QuerySqlAsync<Producto>("SELECT * FROM Productos");
                return Ok(productos);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto([FromBody] Producto producto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Productos (Nombre, Precio) VALUES (@Nombre, @Precio); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                var id = await connection.ExecuteScalarSqlAsync<int>(sql, producto);
                producto.Id = id;
                return Ok(producto);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Producto>> UpdateProducto(int id, [FromBody] Producto updatedProducto)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Productos SET Nombre = @Nombre, Precio = @Precio WHERE Id = @Id;";
                await connection.ExecuteSqlAsync(sql, updatedProducto);
                return Ok(updatedProducto);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Productos WHERE Id = @Id";
                await connection.ExecuteSqlAsync(sql, new { Id = id });
                return Ok();
            }
        }
    }
}

