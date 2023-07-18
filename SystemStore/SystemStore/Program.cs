using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemStore
{
     
    
    internal class Program
    {
        static string archivoInventario = "inventario.txt";
        static string[,] inventario = new string[100, 3]; 
        static int totalProductos = 0; 
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
                        if (totalProductos > 0)
                        {
                            EditarProducto();
                            GuardarInventario();
                        }
                        else
                        {
                            Console.WriteLine("El inventario está vacío. No es posible editar productos.");
                        }
                        break;
                    case "3":
                        if (totalProductos > 0)
                        {
                            EliminarProducto();
                            GuardarInventario();
                        }
                        else
                        {
                            Console.WriteLine("El inventario está vacío. No es posible eliminar productos.");
                        }
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
                while (!int.TryParse(cantidadStr, out cantidad) || cantidad < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Cantidad inválida. Por favor, ingrese un número válido: ");
                    cantidadStr = Console.ReadLine();
                    Console.ResetColor();
                }

                Console.Write("Precio: ");
                string precioStr = Console.ReadLine();
                double precio;

                // Validar que el precio sea un número válido
                while (!double.TryParse(precioStr, out precio) || precio < 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Precio inválido. Por favor, ingrese un número válido: ");
                    precioStr = Console.ReadLine();
                    
                    Console.ResetColor();
                }

                inventario[totalProductos, 0] = nombre;
                inventario[totalProductos, 1] = cantidad.ToString();
                inventario[totalProductos, 2] = precio.ToString("0.00");

                totalProductos++;

                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Producto ingresado exitosamente.");
                Console.ResetColor();
                GuardarInventario();
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
            while (!int.TryParse(opcion, out indice) || indice < 1 || indice > totalProductos)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Opción inválida. Por favor, seleccione un número válido: ");
                opcion = Console.ReadLine();
                Console.ResetColor();
            }

            

            Console.Write("Nuevo nombre (dejar en blanco para no cambiar): ");
            string nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(nuevoNombre))
                inventario[indice - 1, 0] = nuevoNombre;

            Console.Write("Nueva cantidad (dejar en blanco para no cambiar): ");
            string nuevaCantidadStr = Console.ReadLine();
            int nuevaCantidad;

            // Validar que la nueva cantidad sea un número válido
            if (!string.IsNullOrWhiteSpace(nuevaCantidadStr))
            {
                while (!int.TryParse(nuevaCantidadStr, out nuevaCantidad) || nuevaCantidad < 0)
                {
                    Console.Write("Cantidad inválida. Por favor, ingrese un número válido y no negativo: ");
                    nuevaCantidadStr = Console.ReadLine();
                }
                inventario[indice - 1, 1] = nuevaCantidad.ToString();
            }

            Console.Write("Nuevo precio (dejar en blanco para no cambiar): ");
            string nuevoPrecioStr = Console.ReadLine();
            double nuevoPrecio;

            // Validar que el nuevo precio sea un número válido
            if (!string.IsNullOrWhiteSpace(nuevoPrecioStr))
            {
                while (!double.TryParse(nuevoPrecioStr, out nuevoPrecio) || nuevoPrecio < 0)
                {
                    Console.Write("Precio inválido. Por favor, ingrese un número válido y no negativo: ");
                    nuevoPrecioStr = Console.ReadLine();
                }
                inventario[indice - 1, 2] = nuevoPrecio.ToString("0.00"); // Guardamos el precio con formato decimal completo.
            }
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
            while (!int.TryParse(opcion, out indice) || indice < 1 || indice > totalProductos)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Opción inválida. Por favor, seleccione un número válido: ");
                opcion = Console.ReadLine();
                Console.ResetColor();
            }

            for (int i = indice - 1; i < totalProductos - 1; i++)
            {
                inventario[i, 0] = inventario[i + 1, 0];
                inventario[i, 1] = inventario[i + 1, 1];
                inventario[i, 2] = inventario[i + 1, 2];
            }

            totalProductos--;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Producto eliminado exitosamente.");
            Console.ResetColor();
        }

        static void ListarProductos()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("====== Inventario ======");
            Console.ResetColor();
            for (int i = 0; i < totalProductos; i++)
            {
                Console.WriteLine($"{i + 1}. Nombre: {inventario[i, 0]}\tCantidad: {inventario[i, 1]}\tPrecio: {inventario[i, 2]}");
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

                for (int i = 0; i < lineas.Length; i++)
                {
                    string[] datos = lineas[i].Split(';');
                    if (datos.Length == 3)
                    {
                        inventario[i, 0] = datos[0];
                        inventario[i, 1] = datos[1];
                        inventario[i, 2] = datos[2];
                        totalProductos++;
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
                for (int i = 0; i < totalProductos; i++)
                {
                    string linea = $"{inventario[i, 0]};{inventario[i, 1]};{inventario[i, 2]}";
                    escritor.WriteLine(linea);
                }

            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Inventario guardado exitosamente.");
            Console.ResetColor();
        }
    }
}

