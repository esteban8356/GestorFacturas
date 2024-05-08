using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Shared.Entidades
{
    public class Factura
    {
        public int Id { get; set; } 
        public int ClienteID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
    }
}
