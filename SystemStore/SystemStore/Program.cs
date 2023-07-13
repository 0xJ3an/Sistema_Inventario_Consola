using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStore
{
    class Producto
    {
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        public override string ToString()
        {
            return $"Nombre: {Nombre}\tCantidad: {Cantidad}\tPrecio: {Precio:C}";
        }
    }
    internal class Program
    {
        static List<Producto>  inventario = new List<Producto>();
        static string archivoInventario = "inventario.txt";
        static void Main(string[] args)
        {
            CargarInventario();
            MostrarMenu();
            GuardarInventario();
        }

        static void MostrarMenu()
        {
            bool salir = false;

            while (!salir)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("========== Menú ==========");
                Console.ResetColor();
                Console.WriteLine("1. Ingresar producto");
                Console.WriteLine("2. Editar producto");
                Console.WriteLine("3. Eliminar producto");
                Console.WriteLine("4. Listar productos");
                Console.WriteLine("5. Salir");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("===========================");
                Console.ResetColor();

                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        IngresarProducto();
                        break;
                    case "2":
                        EditarProducto();
                        break;
                    case "3":
                        EliminarProducto();
                        break;
                    case "4":
                        ListarProductos();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opcion invalida.  Por favor, seleccione nuevamente.");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine();
            }
        }

        static void IngresarProducto()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==== Ingresar Producto ====");
            Console.ResetColor();
            Console.Write("¿Cuántos productos desea ingresar?: ");
            int cantidadProductos;

            while (!int.TryParse(Console.ReadLine(), out cantidadProductos) || cantidadProductos <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Cantidad inválida. Por favor, ingrese un número válido: ");
                Console.ResetColor();
            }

            for (int i = 0; i < cantidadProductos; i++)
            {
                Console.WriteLine($"\nIngresando producto #{i + 1}");

                Console.Write("Nombre del producto: ");
                string nombre = Console.ReadLine();

                // Validar que el nombre no esté vacío
                while (string.IsNullOrWhiteSpace(nombre))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("El nombre no puede estar vacío. Por favor, ingrese un nombre válido: ");
                    nombre = Console.ReadLine();
                    Console.ResetColor();
                }

                Console.Write("Cantidad: ");
                string cantidadStr = Console.ReadLine();
                int cantidad;

                // Validar que la cantidad sea un número válido
                while (!int.TryParse(cantidadStr, out cantidad))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Cantidad inválida. Por favor, ingrese un número válido: ");
                    cantidadStr = Console.ReadLine();
                    Console.ResetColor();
                }

                Console.Write("Precio: ");
                string precioStr = Console.ReadLine();
                decimal precio;

                // Validar que el precio sea un número válido
                while (!decimal.TryParse(precioStr, out precio))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Precio inválido. Por favor, ingrese un número válido: ");
                    precioStr = Console.ReadLine();
                    Console.ResetColor();
                }

                Producto producto = new Producto()
                {
                    Nombre = nombre,
                    Cantidad = cantidad,
                    Precio = precio
                };

                inventario.Add(producto);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Producto ingresado exitosamente.");
                Console.ResetColor();
            }
        }

        static void EditarProducto()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("==== Editar Producto ====");
            Console.ResetColor();
            ListarProductos();

            Console.Write("Seleccione el número de producto a editar: ");
            int indice;
            string opcion = Console.ReadLine();

            // Validar que la opción sea un número válido
            while (!int.TryParse(opcion, out indice) || indice < 1 || indice > inventario.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Opción inválida. Por favor, seleccione un número válido: ");
                opcion = Console.ReadLine();
                Console.ResetColor();
            }

            Producto producto = inventario[indice - 1];

            Console.Write("Nuevo nombre (dejar en blanco para no cambiar): ");
            string nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
                producto.Nombre = nuevoNombre;

            Console.Write("Nueva cantidad (dejar en blanco para no cambiar): ");
            string nuevaCantidadStr = Console.ReadLine();
            int nuevaCantidad;

            // Validar que la nueva cantidad sea un número válido
            if (!string.IsNullOrWhiteSpace(nuevaCantidadStr) && int.TryParse(nuevaCantidadStr, out nuevaCantidad))
                producto.Cantidad = nuevaCantidad;

            Console.Write("Nuevo precio (dejar en blanco para no cambiar): ");
            string nuevoPrecioStr = Console.ReadLine();
            decimal nuevoPrecio;

            // Validar que el nuevo precio sea un número válido
            if (!string.IsNullOrWhiteSpace(nuevoPrecioStr) && decimal.TryParse(nuevoPrecioStr, out nuevoPrecio))
                producto.Precio = nuevoPrecio;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Producto editado exitosamente.");
            Console.ResetColor();
        }

        static void EliminarProducto()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("==== Eliminar Producto ====");
            Console.ResetColor();
            ListarProductos();

            Console.Write("Seleccione el número de producto a eliminar: ");
            int indice;
            string opcion = Console.ReadLine();

            // Validar que la opción sea un número válido
            while (!int.TryParse(opcion, out indice) || indice < 1 || indice > inventario.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Opción inválida. Por favor, seleccione un número válido: ");
                opcion = Console.ReadLine();
                Console.ResetColor();
            }

            Producto producto = inventario[indice - 1];
            inventario.RemoveAt(indice - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Producto eliminado exitosamente.");
            Console.ResetColor();
        }

        static void ListarProductos()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====== Inventario ======");
            Console.ResetColor();
            for (int i = 0; i < inventario.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {inventario[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("========================");
            Console.ResetColor();
        }

        static void CargarInventario()
        {
            if (File.Exists(archivoInventario))
            {
                string[] lineas = File.ReadAllLines(archivoInventario);

                foreach (string linea in lineas)
                {
                    string[] datos = linea.Split(';');
                    if (datos.Length == 3)
                    {
                        Producto producto = new Producto()
                        {
                            Nombre = datos[0],
                            Cantidad = int.Parse(datos[1]),
                            Precio = decimal.Parse(datos[2])
                        };

                        inventario.Add(producto);
                    }
                }

                Console.ForegroundColor= ConsoleColor.Green;
                Console.WriteLine("Inventario cargado exitosamente.");
                Console.ResetColor();
            }
        }

        static void GuardarInventario()
        {
            using (StreamWriter escritor = new StreamWriter(archivoInventario))
            {
                foreach (Producto producto in inventario)
                {
                    string linea = $"{producto.Nombre};{producto.Cantidad};{producto.Precio}";
                    escritor.WriteLine(linea);
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Inventario guardado exitosamente.");
            Console.ResetColor();
        }
    }
}

