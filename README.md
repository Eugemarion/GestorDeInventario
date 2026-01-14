# GestorDeInventario — C# Console App

Aplicación de consola desarrollada en C# para gestionar un inventario de productos, enfocada en **reglas de negocio reales**, **manejo de stock** y **reportes**.  
Proyecto orientado a practicar lógica de negocio, LINQ y diseño limpio en C#.

## Tecnologías
- C# (.NET)
- Aplicación de Consola
- LINQ

## Funcionalidades
- Agregar productos con validaciones
- SKU único (no se permiten duplicados)
- Listar productos
- Buscar productos por nombre o SKU
- Editar productos (nombre, categoría, precio)
- Ajustar stock de forma incremental (+ / -)
- Eliminar productos
- Reportes:
  - Productos con stock bajo
  - Total de stock por categoría
  - Valor total del inventario

## Reglas de negocio implementadas
- El **SKU es único** (case-insensitive y normalizado)
- No se permiten precios negativos
- No se permite stock negativo
- El stock se ajusta de forma **incremental** (delta), no se reemplaza directamente
- Las categorías se normalizan automáticamente

## Estructura del proyecto
- `Producto.cs` → Modelo de dominio
- `ProductoRepository.cs` → Reglas de negocio y gestión del inventario
- `Program.cs` → Interfaz de usuario por consola (menú)

Separación clara entre:
- **UI (consola)**
- **Lógica de negocio**
- **Gestión de datos en memoria**


Agregar producto:
