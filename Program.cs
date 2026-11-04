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
                        ShowLoansMenu();
                        break;
                    case 4:
                        ShowSearchReportsMenu();
                        break;
                    case 5:
                        ShowPersistenceMenu();
                        break;
                    case 6:
                        HandleExitFlow(ref exit);
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
    // FLUJO DE SALIDA (EXIT FLOW)
    // ==========================================
    static void HandleExitFlow(ref bool exit)
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("           SALIR DEL SISTEMA            ");
        Console.WriteLine("========================================");
        Console.Write("¿Desea guardar los cambios antes de salir? (S/N): ");
        string confirm = Console.ReadLine();

        if (confirm?.Trim().ToUpper() == "S")
        {
            Console.WriteLine("\n[Acción] Guardando datos de forma simulada en archivos...");
            Console.WriteLine("¡Datos guardados con éxito!");
        }
        else
        {
            Console.WriteLine("\n[Aviso] Saliendo sin guardar los últimos cambios...");
        }

        exit = true;
        Console.WriteLine("\nSaliendo del sistema Gestor de Biblioteca. ¡Hasta luego!");
        Console.ReadKey();
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

    static void RegisterUser()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR USUARIO ===");
        Console.WriteLine("[Acción] Registrando un nuevo usuario en el sistema...");
        Console.ReadKey();
    }

    static void ListUsers()
    {
        Console.Clear();
        Console.WriteLine("=== LISTAR USUARIOS ===");
        Console.WriteLine("[Acción] Mostrando listado completo de usuarios...");
        Console.ReadKey();
    }

    static void ViewUserDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE USUARIO ===");
        Console.WriteLine("[Acción] Mostrando detalle del usuario por documento...");
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
        Console.WriteLine("[Acción] Actualizando nombre del usuario...");
        Console.ReadKey();
    }

    static void EditUserContact()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Actualizando contacto del usuario...");
        Console.ReadKey();
    }

    static void ToggleUserActiveStatus()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Cambiando estado activo/inactivo del usuario...");
        Console.ReadKey();
    }

    static void DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR USUARIO ===");
        Console.WriteLine("[Acción] Validar no permitir eliminar si tiene préstamos activos.");
        Console.ReadKey();
    }

    // ==========================================
    // 3. MÓDULO DE PRÉSTAMOS
    // ==========================================
    static void ShowLoansMenu()
    {
        bool back = false;

        do
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("          GESTIÓN DE PRÉSTAMOS          ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Crear préstamo");
            Console.WriteLine("2. Listar préstamos");
            Console.WriteLine("3. Ver detalle de préstamo");
            Console.WriteLine("4. Registrar devolución");
            Console.WriteLine("5. Eliminar préstamo");
            Console.WriteLine("6. Volver al menú principal");
            Console.Write("\nSeleccione una opción (1-6): ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        CreateLoan();
                        break;
                    case 2:
                        ListLoansMenu();
                        break;
                    case 3:
                        ViewLoanDetail();
                        break;
                    case 4:
                        RegisterReturn();
                        break;
                    case 5:
                        DeleteLoan();
                        break;
                    case 6:
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

    static void CreateLoan()
    {
        Console.Clear();
        Console.WriteLine("=== CREAR PRÉSTAMO ===");
        Console.WriteLine("[Acción] Simulando creación de préstamo.");
        Console.WriteLine("Validaciones requeridas:");
        Console.WriteLine(" - Validar que el libro exista y esté disponible.");
        Console.WriteLine(" - Validar que el usuario exista y esté activo.");
        Console.WriteLine(" - Validar límite de préstamos del usuario.");
        Console.ReadKey();
    }

    static void ListLoansMenu()
    {
        bool back = false;
        do
        {
            Console.Clear();
            Console.WriteLine("=== LISTAR PRÉSTAMOS ===");
            Console.WriteLine("1. Todos");
            Console.WriteLine("2. Activos");
            Console.WriteLine("3. Cerrados");
            Console.WriteLine("4. Volver al menú de préstamos");
            Console.Write("\nSeleccione una opción (1-4): ");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        ListLoansAll();
                        break;
                    case 2:
                        ListLoansActive();
                        break;
                    case 3:
                        ListLoansClosed();
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

    static void ListLoansAll()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Mostrando todos los préstamos...");
        Console.ReadKey();
    }

    static void ListLoansActive()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Mostrando préstamos activos...");
        Console.ReadKey();
    }

    static void ListLoansClosed()
    {
        Console.Clear();
        Console.WriteLine("[Acción] Mostrando préstamos cerrados...");
        Console.ReadKey();
    }

    static void ViewLoanDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE PRÉSTAMO ===");
        Console.WriteLine("[Acción] Mostrando detalle del préstamo por ID...");
        Console.ReadKey();
    }

    static void RegisterReturn()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR DEVOLUCIÓN ===");
        Console.WriteLine("[Acción] Marcando préstamo como devuelto y libro disponible.");
        Console.ReadKey();
    }

    static void DeleteLoan()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR PRÉSTAMO ===");
        Console.WriteLine("[Acción] Eliminando préstamo según reglas del sistema.");
        Console.ReadKey();
    }

    // ==========================================
    // 4. MÓDULO DE BÚSQUEDAS Y REPORTES
    // ==========================================
    static void ShowSearchReportsMenu()
    {
        bool back = false;

        do
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("         BÚSQUEDAS Y REPORTES           ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Buscar libros por título o autor");
            Console.WriteLine("2. Buscar usuario por documento o nombre");
            Console.WriteLine("3. Reporte de libros más prestados");
            Console.WriteLine("4. Reporte de multas o préstamos vencidos");
            Console.WriteLine("5. Volver al menú principal");
            Console.Write("\nSeleccione una opción (1-5): ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        SearchBooks();
                        break;
                    case 2:
                        SearchUsers();
                        break;
                    case 3:
                        ReportMostBorrowedBooks();
                        break;
                    case 4:
                        ReportOverdueLoans();
                        break;
                    case 5:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Elija entre 1 y 5.");
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

    static void SearchBooks()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR LIBROS ===");
        Console.WriteLine("[Acción] Buscando libros por coincidencia de título o autor...");
        Console.ReadKey();
    }

    static void SearchUsers()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR USUARIOS ===");
        Console.WriteLine("[Acción] Buscando usuarios por documento o nombre...");
        Console.ReadKey();
    }

    static void ReportMostBorrowedBooks()
    {
        Console.Clear();
        Console.WriteLine("=== REPORTE: LIBROS MÁS PRESTADOS ===");
        Console.WriteLine("[Acción] Generando reporte estadístico de libros...");
        Console.ReadKey();
    }

    static void ReportOverdueLoans()
    {
        Console.Clear();
        Console.WriteLine("=== REPORTE: PRÉSTAMOS VENCIDOS ===");
        Console.WriteLine("[Acción] Consultando préstamos que exceden la fecha límite...");
        Console.ReadKey();
    }

    // ==========================================
    // 5. MÓDULO DE PERSISTENCIA (GUARDAR / CARGAR)
    // ==========================================
    static void ShowPersistenceMenu()
    {
        bool back = false;

        do
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("        GUARDAR / CARGAR DATOS          ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Guardar datos en archivo (Simulado)");
            Console.WriteLine("2. Cargar datos desde archivo (Simulado)");
            Console.WriteLine("3. Volver al menú principal");
            Console.Write("\nSeleccione una opción (1-3): ");

            string input = Console.ReadLine();

            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1:
                        SaveDataToFile();
                        break;
                    case 2:
                        LoadDataFromFile();
                        break;
                    case 3:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida. Elija entre 1 y 3.");
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

    static void SaveDataToFile()
    {
        Console.Clear();
        Console.WriteLine("=== GUARDAR DATOS ===");
        Console.WriteLine("[Acción] Serializando y guardando catálogos (Libros, Usuarios, Préstamos)...");
        Console.ReadKey();
    }

    static void LoadDataFromFile()
    {
        Console.Clear();
        Console.WriteLine("=== CARGAR DATOS ===");
        Console.WriteLine("[Acción] Leyendo y cargando información desde archivos simulados...");
        Console.ReadKey();
    }
}