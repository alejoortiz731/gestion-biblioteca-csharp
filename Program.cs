using System;

class Program
{
    static void Main(string[] args)
    {
        ShowMainMenu();
    }

    static void ShowMainMenu()
    {
        bool exit = false;

        do
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("    SISTEMA GESTOR DE BIBLIOTECA        ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Libros");
            Console.WriteLine("2. Usuarios");
            Console.WriteLine("3. Préstamos");
            Console.WriteLine("4. Búsquedas y reportes");
            Console.WriteLine("5. Guardar / Cargar datos");
            Console.WriteLine("6. Salir");
            Console.Write("\nSeleccione una opción (1-6): ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        Console.WriteLine("\n[Stub] Abriendo módulo de Libros...");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("\n[Stub] Abriendo módulo de Usuarios...");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.WriteLine("\n[Stub] Abriendo módulo de Préstamos...");
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("\n[Stub] Abriendo módulo de Búsquedas y reportes...");
                        Console.ReadKey();
                        break;
                    case 5:
                        Console.WriteLine("\n[Stub] Abriendo módulo de Guardar / Cargar datos...");
                        Console.ReadKey();
                        break;
                    case 6:
                        Console.Write("\n¿Guardar antes de salir? (S/N): ");
                        string confirm = Console.ReadLine();
                        if (confirm?.Trim().ToUpper() == "S")
                        {
                            Console.WriteLine("Guardando datos simulados...");
                        }
                        exit = true;
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Por favor elija un número entre 1 y 6.");
                        Console.WriteLine("Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Debe ingresar un número entero.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }

        } while (!exit);
    }
}