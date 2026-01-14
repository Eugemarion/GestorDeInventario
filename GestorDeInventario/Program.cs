using GestorDeInventario;

ProductoRepository repo = new ProductoRepository();

bool continuar = true;

while (continuar)
{
    Console.WriteLine("=== GESTOR DE INVENTARIO ===");
    Console.WriteLine("1. Agregar producto");
    Console.WriteLine("2. Listar productos");
    Console.WriteLine("3. Ajustar stock");
    Console.WriteLine("4. Reportes");
    Console.WriteLine("5. Salir");
    Console.Write("Opción: ");

    string opcion = Console.ReadLine();
    Console.WriteLine();

    switch (opcion)
    {
        case "1":
            AgregarProducto(repo);
            break;
        case "2":
            ListarProductos(repo);
            break;
        case "3":
            AjustarStock(repo);
            break;
        case "4":
            Reportes(repo);
            break;
        case "5":
            continuar = false;
            break;
        default:
            Console.WriteLine("Opción inválida.");
            break;
    }

    Console.WriteLine();
}

void AgregarProducto(ProductoRepository repo)
{
    Console.Write("SKU: ");
    string sku = Console.ReadLine();

    Console.Write("Nombre: ");
    string nombre = Console.ReadLine();

    Console.Write("Categoría: ");
    string categoria = Console.ReadLine();

    Console.Write("Precio: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal precio))
    {
        Console.WriteLine("Precio inválido.");
        return;
    }

    Console.Write("Stock inicial: ");
    if (!int.TryParse(Console.ReadLine(), out int stock))
    {
        Console.WriteLine("Stock inválido.");
        return;
    }

    Producto p = new Producto
    {
        Sku = sku ?? "",
        Nombre = nombre ?? "",
        Categoria = categoria ?? "General",
        Precio = precio,
        Stock = stock
    };

    bool ok = repo.Agregar(p);
    Console.WriteLine(ok ? "Producto agregado." : "✖ No se pudo agregar (SKU duplicado o datos inválidos).");
}

void ListarProductos(ProductoRepository repo)
{
    var productos = repo.ObtenerTodos();

    if (productos.Count == 0)
    {
        Console.WriteLine("No hay productos.");
        return;
    }

    foreach (var p in productos)
    {
        Console.WriteLine($"[{p.Id}] {p.Nombre} | SKU: {p.Sku} | Cat: {p.Categoria} | $" +
                          $"{p.Precio} | Stock: {p.Stock}");
    }
}

void AjustarStock(ProductoRepository repo)
{
    Console.Write("ID del producto: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("ID inválido.");
        return;
    }

    var p = repo.ObtenerPorId(id);
    if (p == null)
    {
        Console.WriteLine("No existe ese producto.");
        return;
    }

    Console.WriteLine($"Producto: {p.Nombre} | Stock actual: {p.Stock}");
    Console.Write("Delta de stock (ej: 5 o -3): ");

    if (!int.TryParse(Console.ReadLine(), out int delta))
    {
        Console.WriteLine("Delta inválido.");
        return;
    }

    bool ok = repo.AjustarStock(id, delta);
    Console.WriteLine(ok ? "Stock actualizado." : "No se pudo (stock quedaría negativo).");
}


void Reportes(ProductoRepository repo)
{
    bool seguir = true;

    while (seguir)
    {
        Console.WriteLine("=== REPORTES ===");
        Console.WriteLine("1. Stock bajo");
        Console.WriteLine("2. Total stock por categoría");
        Console.WriteLine("3. Valor total inventario");
        Console.WriteLine("4. Volver");
        Console.Write("Opción: ");

        string op = Console.ReadLine();
        Console.WriteLine();

        switch (op)
        {
            case "1":
                ReporteStockBajo(repo);
                break;
            case "2":
                ReporteTotalPorCategoria(repo);
                break;
            case "3":
                ReporteValorTotal(repo);
                break;
            case "4":
                seguir = false;
                break;
            default:
                Console.WriteLine("Opción inválida.");
                break;
        }

        Console.WriteLine();
    }
}

void ReporteStockBajo(ProductoRepository repo)
{
    Console.Write("Umbral (ej: 5): ");
    if (!int.TryParse(Console.ReadLine(), out int umbral))
    {
        Console.WriteLine("Umbral inválido.");
        return;
    }

    var bajos = repo.ObtenerStockBajo(umbral);

    if (bajos.Count == 0)
    {
        Console.WriteLine("No hay productos con stock bajo");
        return;
    }

    Console.WriteLine("Productos con stock bajo:");
    foreach (var p in bajos)
        Console.WriteLine($"[{p.Id}] {p.Nombre} | SKU: {p.Sku} | Stock: {p.Stock}");
}

void ReporteTotalPorCategoria(ProductoRepository repo)
{
    var totales = repo.TotalStockPorCategoria();

    if (totales.Count == 0)
    {
        Console.WriteLine("No hay productos.");
        return;
    }

    Console.WriteLine("Stock total por categoría:");
    foreach (var kv in totales)
        Console.WriteLine($"{kv.Key}: {kv.Value}");
}

void ReporteValorTotal(ProductoRepository repo)
{
    decimal total = repo.ValorTotalInventario();
    Console.WriteLine($"Valor total del inventario: ${total}");
}

