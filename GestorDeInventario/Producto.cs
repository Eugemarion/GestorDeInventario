using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeInventario
{
    public class Producto
    {
        public int Id { get; set; }
        public string Sku { get; set; } = "";
        public string Nombre { get; set; } = "";
        public string Categoria { get; set; } = "General";
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
