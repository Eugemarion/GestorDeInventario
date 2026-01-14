using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorDeInventario
{
    public class ProductoRepository
    {
        private readonly List<Producto> _productos = new List<Producto>();
        private int _ultimoId = 0;

        private string NormalizarSku(string sku)
            => (sku ?? "").Trim().ToUpper();

        public bool SkuExiste(string sku)
        {
            string s = NormalizarSku(sku);
            return _productos.Any(p => NormalizarSku(p.Sku) == s);
        }

        public bool Agregar(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Sku)) return false;
            if (string.IsNullOrWhiteSpace(producto.Nombre)) return false;
            if (producto.Precio < 0) return false;
            if (producto.Stock < 0) return false;

            if (SkuExiste(producto.Sku)) return false;

            _ultimoId++;
            producto.Id = _ultimoId;

            producto.Sku = NormalizarSku(producto.Sku);
            producto.Nombre = producto.Nombre.Trim();
            producto.Categoria = string.IsNullOrWhiteSpace(producto.Categoria) ? "General" : producto.Categoria.Trim();

            _productos.Add(producto);
            return true;
        }

        public List<Producto> ObtenerTodos()
        {
            return _productos;
        }

        public Producto? ObtenerPorId(int id)
        {
            return _productos.FirstOrDefault(p => p.Id == id);
        }

        public bool AjustarStock(int id, int delta)
        {
            var p = _productos.FirstOrDefault(x => x.Id == id);
            if (p == null) return false;

            int nuevoStock = p.Stock + delta;
            if (nuevoStock < 0) return false; 

            p.Stock = nuevoStock;
            return true;
        }

        public List<Producto> ObtenerStockBajo(int umbral)
        {
            return _productos
                .Where(p => p.Stock <= umbral)
                .OrderBy(p => p.Stock)
                .ToList();
        }

        public Dictionary<string, int> TotalStockPorCategoria()
        {
            return _productos
                .GroupBy(p => string.IsNullOrWhiteSpace(p.Categoria) ? "General" : p.Categoria)
                .ToDictionary(g => g.Key, g => g.Sum(p => p.Stock));
        }

        public decimal ValorTotalInventario()
        {
            return _productos.Sum(p => p.Precio * p.Stock);
        }

    }
}
