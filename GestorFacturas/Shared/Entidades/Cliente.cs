using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorFacturas.Shared.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }

        public string ClienteName { get; set; } = null!;

        public string ClienteEmail { get; set; } = null!;

    }
}
