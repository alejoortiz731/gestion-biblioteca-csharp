using System;

class Program
{
    static void Main(string[] args)
    {
        ShowMainMenu();
    }

    // ==========================================
    // MENÚ PRINCIPAL
    // ==========================================
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
                        ShowBooksMenu();
                        break;
                    case 2:
                        ShowUsersMenu();
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

    // ==========================================
    // 1. MÓDULO DE LIBROS
    // ==========================================
    static void ShowBooksMenu()
    {
        bool back = false;

        do
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("            GESTIÓN DE LIBROS           ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Listar libros");
            Console.WriteLine("3. Ver detalle (por ID/ISBN)");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("6. Volver al menú principal");
            Console.Write("\nSeleccione una opción (1-6): ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        RegisterBook();
                        break;
                    case 2:
                        ListBooksMenu();
                        break;
                    case 3:
                        ViewBookDetail();
                        break;
                    case 4:
                        UpdateBookMenu();
                        break;
                    case 5:
                        DeleteBook();
                        break;
                    case 6:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Elija entre 1 y 6.");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Ingrese un número.");
                Console.ReadKey();
            }

        } while (!back);
    }

    static void RegisterBook()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LIBRO ===");
        Console.WriteLine("[Acción] Registrando un nuevo libro en el sistema (Simulación)...");
        Console.ReadKey();
    }

    static void ListBooksMenu()
    {
        bool back = false;
        do
        {
            Console.Clear();
            Console.WriteLine("=== LISTAR LIBROS ===");
            Console.WriteLine("1. Listar todos");
            Console.WriteLine("2. Listar disponibles");
            Console.WriteLine("3. Listar prestados");
            Console.WriteLine("4. Volver al menú de libros");
            Console.Write("\nSeleccione una opción (1-4): ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        ListBooksAll();
                        break;
                    case 2:
                        ListBooksAvailable();
                        break;
                    case 3:
                        ListBooksBorrowed();
                        break;
                    case 4:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                Console.ReadKey();
            }
        } while (!back);
    }

    static void ListBooksAll()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Mostrando el listado completo de libros...");
        Console.ReadKey();
    }

    static void ListBooksAvailable()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Mostrando únicamente los libros disponibles...");
        Console.ReadKey();
    }

    static void ListBooksBorrowed()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Mostrando únicamente los libros actualmente prestados...");
        Console.ReadKey();
    }

    static void ViewBookDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE LIBRO ===");
        Console.WriteLine("[Acción] Buscando y mostrando el detalle del libro por ID/ISBN...");
        Console.ReadKey();
    }

    static void UpdateBookMenu()
    {
        bool back = false;
        do
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR LIBRO ===");
            Console.WriteLine("1. Editar título");
            Console.WriteLine("2. Editar autor");
            Console.WriteLine("3. Editar año / categoría");
            Console.WriteLine("4. Volver al menú de libros");
            Console.Write("\nSeleccione una opción (1-4): ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        EditBookTitle();
                        break;
                    case 2:
                        EditBookAuthor();
                        break;
                    case 3:
                        EditBookYearCategory();
                        break;
                    case 4:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                Console.ReadKey();
            }
        } while (!back);
    }

    static void EditBookTitle()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Actualizando título del libro...");
        Console.ReadKey();
    }

    static void EditBookAuthor()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Actualizando autor del libro...");
        Console.ReadKey();
    }

    static void EditBookYearCategory()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Actualizando año / categoría del libro...");
        Console.ReadKey();
    }

    static void DeleteBook()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR LIBRO ===");
        Console.WriteLine("[Acción] Validar no permitir eliminar si el libro se encuentra prestado.");
        Console.ReadKey();
    }

    // ==========================================
    // 2. MÓDULO DE USUARIOS
    // ==========================================
    static void ShowUsersMenu()
    {
        bool back = false;

        do
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("           GESTIÓN DE USUARIOS          ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Registrar usuario");
            Console.WriteLine("2. Listar usuarios");
            Console.WriteLine("3. Ver detalle (por ID/documento)");
            Console.WriteLine("4. Actualizar usuario");
            Console.WriteLine("5. Eliminar usuario");
            Console.WriteLine("6. Volver al menú principal");
            Console.Write("\nSeleccione una opción (1-6): ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        RegisterUser();
                        break;
                    case 2:
                        ListUsers();
                        break;
                    case 3:
                        ViewUserDetail();
                        break;
                    case 4:
                        UpdateUserMenu();
                        break;
                    case 5:
                        DeleteUser();
                        break;
                    case 6:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Elija entre 1 y 6.");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Ingrese un número.");
                Console.ReadKey();
            }

        } while (!back);
    }

    static void RegisterUser()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR USUARIO ===");
        Console.WriteLine("[Acción] Registrando un nuevo usuario en el sistema (Simulación)...");
        Console.ReadKey();
    }

    static void ListUsers()
    {
        Console.Clear();
        Console.WriteLine("=== LISTAR USUARIOS ===");
        Console.WriteLine("[Acción] Mostrando el listado completo de usuarios registrados...");
        Console.ReadKey();
    }

    static void ViewUserDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE USUARIO ===");
        Console.WriteLine("[Acción] Buscando y mostrando el detalle del usuario por ID o documento...");
        Console.ReadKey();
    }

    static void UpdateUserMenu()
    {
        bool back = false;
        do
        {
            Console.Clear();
            Console.WriteLine("=== ACTUALIZAR USUARIO ===");
            Console.WriteLine("1. Editar nombre");
            Console.WriteLine("2. Editar contacto");
            Console.WriteLine("3. Activar / desactivar usuario");
            Console.WriteLine("4. Volver al menú de usuarios");
            Console.Write("\nSeleccione una opción (1-4): ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        EditUserName();
                        break;
                    case 2:
                        EditUserContact();
                        break;
                    case 3:
                        ToggleUserActiveStatus();
                        break;
                    case 4:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                Console.ReadKey();
            }
        } while (!back);
    }

    static void EditUserName()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Actualizando el nombre del usuario...");
        Console.ReadKey();
    }

    static void EditUserContact()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Actualizando la información de contacto del usuario...");
        Console.ReadKey();
    }

    static void ToggleUserActiveStatus()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Cambiando estado (Activo / Desactivar) del usuario...");
        Console.ReadKey();
    }

    static void DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR USUARIO ===");
        Console.WriteLine("[Acción] Validar no permitir eliminar si el usuario tiene préstamos activos.");
        Console.ReadKey();
    }
}