using System;
using System.Collections.Generic;
using System.Linq;
using SystemBooks.Models;
using SystemBooks.Services;

class Program
{
    private static readonly LibroService _libroService = new LibroService();
    private static readonly UsuarioService _usuarioService = new UsuarioService();
    private static readonly PrestamoService _prestamoService = new PrestamoService();
    private static int _contadorIsbnAuto = 1;

    static void Main(string[] args)
    {
        ShowMainMenu();
    }

    static void PausaVolver(string mensaje = "\nPresione una tecla para volver...")
    {
        Console.WriteLine(mensaje);
        Console.ReadKey(true);
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

            string input = Console.ReadLine() ?? "";

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
                        PausaVolver("\nPresione una tecla para volver al menú...");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Debe ingresar un número entero.");
                PausaVolver("\nPresione una tecla para volver al menú...");
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
        string confirm = Console.ReadLine() ?? "";

        if (confirm.Trim().ToUpper() == "S")
        {
            Console.WriteLine("\n[Acción] Resumen actual en memoria:");
            Console.WriteLine($"- Libros: {_libroService.TotalLibros()} | Usuarios: {_usuarioService.TotalUsuarios()} | Préstamos: {_prestamoService.TotalPrestamos()}");
        }
        else
        {
            Console.WriteLine("\n[Aviso] Saliendo del sistema...");
        }

        exit = true;
        Console.WriteLine("\nSaliendo del sistema Gestor de Biblioteca. ¡Hasta luego!");
        PausaVolver("\nPresione una tecla para finalizar...");
    }

    // ==========================================
    // AUXILIARES PARA BUSCAR LIBRO POR ISBN O #
    // ==========================================
    static string LeerTextoObligatorio(string etiqueta)
    {
        while (true)
        {
            Console.Write(etiqueta);
            string valor = (Console.ReadLine() ?? "").Trim();
            if (!string.IsNullOrWhiteSpace(valor))
            {
                return valor;
            }
            Console.WriteLine("Este campo es obligatorio y no puede quedar vacío. Intente de nuevo.");
        }
    }

    static Libro? BuscarLibroPorIsbnONumero(string criterio)
    {
        string limpio = criterio.Trim();
        if (string.IsNullOrWhiteSpace(limpio)) return null;

        Libro? porIsbn = _libroService.BuscarPorIsbn(limpio);
        if (porIsbn != null) return porIsbn;

        if (limpio.StartsWith("#")) limpio = limpio.Substring(1);
        if (int.TryParse(limpio, out int numero))
        {
            var todos = _libroService.ObtenerTodos();
            if (numero >= 1 && numero <= todos.Count)
            {
                return todos[numero - 1];
            }
        }

        return null;
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
            Console.WriteLine("3. Ver detalle (por ISBN o #)");
            Console.WriteLine("4. Actualizar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("6. Volver al menú principal");
            Console.Write("\nSeleccione una opción (1-6): ");

            string input = Console.ReadLine() ?? "";

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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Ingrese un número.");
                PausaVolver();
            }

        } while (!back);
    }

    static void RegisterBook()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR LIBRO ===");

        string titulo = LeerTextoObligatorio("Título (obligatorio): ");

        Console.Write("ISBN (opcional, si lo dejas vacío se asigna uno automático): ");
        string isbn = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrWhiteSpace(isbn))
        {
            isbn = $"LIB-{_contadorIsbnAuto++:000}";
        }

        Console.Write("Autor: ");
        string autor = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrWhiteSpace(autor))
        {
            autor = "Sin autor";
        }

        Console.Write("Año de publicación: ");
        if (!int.TryParse(Console.ReadLine(), out int anio) || anio <= 0)
        {
            Console.WriteLine("\nAño inválido o vacío. Se asignará el año actual.");
            anio = DateTime.Now.Year;
        }

        Libro libro = new Libro(isbn, titulo, autor, anio);
        _libroService.AgregarLibro(libro);

        Console.WriteLine($"\nLibro registrado con éxito. ISBN asignado: {libro.Isbn}");
        PausaVolver("\nPresione una tecla para volver al menú de libros...");
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

            string input = Console.ReadLine() ?? "";
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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                PausaVolver();
            }
        } while (!back);
    }

    static void ImprimirLibros(List<Libro> libros)
    {
        if (libros == null || libros.Count == 0)
        {
            Console.WriteLine("No hay libros para mostrar.");
            return;
        }

        for (int i = 0; i < libros.Count; i++)
        {
            var l = libros[i];
            string estado = l.Disponible ? "Disponible" : "Prestado";
            Console.WriteLine($"#{i + 1} | ISBN: {l.Isbn} | Título: {l.Titulo} | Autor: {l.Autor} | Año: {l.AnioPublicacion} | Estado: {estado}");
        }
    }

    static void ListBooksAll()
    {
        Console.Clear();
        Console.WriteLine("=== TODOS LOS LIBROS ===");
        ImprimirLibros(_libroService.ObtenerTodos());
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ListBooksAvailable()
    {
        Console.Clear();
        Console.WriteLine("=== LIBROS DISPONIBLES ===");
        var disponibles = _libroService.ObtenerTodos().Where(l => l.Disponible).ToList();
        ImprimirLibros(disponibles);
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ListBooksBorrowed()
    {
        Console.Clear();
        Console.WriteLine("=== LIBROS PRESTADOS ===");
        var prestados = _libroService.ObtenerTodos().Where(l => !l.Disponible).ToList();
        ImprimirLibros(prestados);
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ViewBookDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE LIBRO ===");
        Console.Write("Ingrese ISBN o # de lista: ");
        string criterio = Console.ReadLine() ?? "";

        Libro? libro = BuscarLibroPorIsbnONumero(criterio);
        if (libro == null)
        {
            Console.WriteLine("\nLibro no encontrado.");
        }
        else
        {
            Console.WriteLine($"\nISBN: {libro.Isbn}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Año de publicación: {libro.AnioPublicacion}");
            Console.WriteLine($"Disponible: {(libro.Disponible ? "Sí" : "No")}");
        }
        PausaVolver("\nPresione una tecla para volver al menú de libros...");
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
            Console.WriteLine("4. Editar ISBN");
            Console.WriteLine("5. Volver al menú de libros");
            Console.Write("\nSeleccione una opción (1-5): ");

            string input = Console.ReadLine() ?? "";
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
                        EditBookIsbn();
                        break;
                    case 5:
                        back = true;
                        break;
                    default:
                        Console.WriteLine("\nOpción no válida.");
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                PausaVolver();
            }
        } while (!back);
    }

    static void EditBookTitle()
    {
        Console.Clear();
        Console.Write("ISBN o # de lista del libro a editar: ");
        string criterio = Console.ReadLine() ?? "";
        Libro? libro = BuscarLibroPorIsbnONumero(criterio);

        if (libro == null)
        {
            Console.WriteLine("\nLibro no encontrado.");
        }
        else
        {
            libro.Titulo = LeerTextoObligatorio("Nuevo título (obligatorio): ");
            Console.WriteLine("\nTítulo actualizado.");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void EditBookAuthor()
    {
        Console.Clear();
        Console.Write("ISBN o # de lista del libro a editar: ");
        string criterio = Console.ReadLine() ?? "";
        Libro? libro = BuscarLibroPorIsbnONumero(criterio);

        if (libro == null)
        {
            Console.WriteLine("\nLibro no encontrado.");
        }
        else
        {
            libro.Autor = LeerTextoObligatorio("Nuevo autor (obligatorio): ");
            Console.WriteLine("\nAutor actualizado.");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void EditBookYearCategory()
    {
        Console.Clear();
        Console.Write("ISBN o # de lista del libro a editar: ");
        string criterio = Console.ReadLine() ?? "";
        Libro? libro = BuscarLibroPorIsbnONumero(criterio);

        if (libro == null)
        {
            Console.WriteLine("\nLibro no encontrado.");
        }
        else
        {
            Console.Write("Nuevo año de publicación: ");
            if (int.TryParse(Console.ReadLine(), out int nuevoAnio) && nuevoAnio > 0)
            {
                libro.AnioPublicacion = nuevoAnio;
                Console.WriteLine("\nAño actualizado.");
            }
            else
            {
                Console.WriteLine("\nAño inválido.");
            }
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void EditBookIsbn()
    {
        Console.Clear();
        Console.Write("ISBN actual o # de lista del libro: ");
        string criterio = Console.ReadLine() ?? "";
        Libro? libro = BuscarLibroPorIsbnONumero(criterio);

        if (libro == null)
        {
            Console.WriteLine("\nLibro no encontrado.");
        }
        else
        {
            string nuevoIsbn = LeerTextoObligatorio("Nuevo ISBN (obligatorio): ");
            libro.Isbn = nuevoIsbn;
            Console.WriteLine($"\nISBN actualizado a: {libro.Isbn}");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void DeleteBook()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR LIBRO ===");
        Console.Write("ISBN o # de lista del libro a eliminar: ");
        string criterio = Console.ReadLine() ?? "";

        Libro? libro = BuscarLibroPorIsbnONumero(criterio);
        if (libro == null)
        {
            Console.WriteLine("\nLibro no encontrado.");
        }
        else if (!libro.Disponible)
        {
            Console.WriteLine("\nNo se puede eliminar: el libro se encuentra prestado.");
        }
        else if (_libroService.EliminarLibroPorIsbn(libro.Isbn))
        {
            Console.WriteLine("\nLibro eliminado con éxito.");
        }
        PausaVolver("\nPresione una tecla para volver al menú de libros...");
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

            string input = Console.ReadLine() ?? "";

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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                PausaVolver();
            }

        } while (!back);
    }

    static void RegisterUser()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR USUARIO ===");

        string doc = LeerTextoObligatorio("Documento (obligatorio): ");
        string nombre = LeerTextoObligatorio("Nombre (obligatorio): ");

        Console.Write("Email: ");
        string email = (Console.ReadLine() ?? "").Trim();
        if (string.IsNullOrWhiteSpace(email))
        {
            email = "sin-correo@biblioteca.local";
        }

        Usuario usuario = new Usuario(doc, nombre, email);
        _usuarioService.AgregarUsuario(usuario);

        Console.WriteLine("\nUsuario registrado con éxito.");
        PausaVolver("\nPresione una tecla para volver al menú de usuarios...");
    }

    static void ListUsers()
    {
        Console.Clear();
        Console.WriteLine("=== LISTAR USUARIOS ===");
        var usuarios = _usuarioService.ObtenerTodos();

        if (usuarios.Count == 0)
        {
            Console.WriteLine("No hay usuarios registrados.");
        }
        else
        {
            foreach (var u in usuarios)
            {
                Console.WriteLine($"Documento: {u.Documento} | Nombre: {u.Nombre} | Email: {u.Email} | Activo: {(u.Activo ? "Sí" : "No")}");
            }
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ViewUserDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE USUARIO ===");
        Console.Write("Ingrese documento: ");
        string doc = (Console.ReadLine() ?? "").Trim();

        Usuario? u = _usuarioService.BuscarPorDocumento(doc);
        if (u == null)
        {
            Console.WriteLine("\nUsuario no encontrado.");
        }
        else
        {
            Console.WriteLine($"\nDocumento: {u.Documento}");
            Console.WriteLine($"Nombre: {u.Nombre}");
            Console.WriteLine($"Email: {u.Email}");
            Console.WriteLine($"Activo: {(u.Activo ? "Sí" : "No")}");
        }
        PausaVolver("\nPresione una tecla para volver...");
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

            string input = Console.ReadLine() ?? "";
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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                PausaVolver();
            }
        } while (!back);
    }

    static void EditUserName()
    {
        Console.Clear();
        Console.Write("Documento del usuario: ");
        string doc = (Console.ReadLine() ?? "").Trim();
        Usuario? u = _usuarioService.BuscarPorDocumento(doc);

        if (u == null)
        {
            Console.WriteLine("\nUsuario no encontrado.");
        }
        else
        {
            u.Nombre = LeerTextoObligatorio("Nuevo nombre (obligatorio): ");
            Console.WriteLine("\nNombre actualizado.");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void EditUserContact()
    {
        Console.Clear();
        Console.Write("Documento del usuario: ");
        string doc = (Console.ReadLine() ?? "").Trim();
        Usuario? u = _usuarioService.BuscarPorDocumento(doc);

        if (u == null)
        {
            Console.WriteLine("\nUsuario no encontrado.");
        }
        else
        {
            u.Email = LeerTextoObligatorio("Nuevo email (obligatorio): ");
            Console.WriteLine("\nContacto actualizado.");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ToggleUserActiveStatus()
    {
        Console.Clear();
        Console.Write("Documento del usuario: ");
        string doc = (Console.ReadLine() ?? "").Trim();
        Usuario? u = _usuarioService.BuscarPorDocumento(doc);

        if (u == null)
        {
            Console.WriteLine("\nUsuario no encontrado.");
        }
        else
        {
            u.Activo = !u.Activo;
            Console.WriteLine($"\nEstado actualizado. Activo: {(u.Activo ? "Sí" : "No")}");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void DeleteUser()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR USUARIO ===");
        Console.Write("Documento del usuario a eliminar: ");
        string doc = (Console.ReadLine() ?? "").Trim();

        bool tienePrestamosActivos = _prestamoService
            .BuscarPorEstado(EstadoPrestamo.Activo)
            .Any(p => p.UsuarioPrestatario != null &&
                      p.UsuarioPrestatario.Documento.Equals(doc, StringComparison.OrdinalIgnoreCase));

        if (tienePrestamosActivos)
        {
            Console.WriteLine("\nNo se puede eliminar: el usuario tiene préstamos activos.");
        }
        else if (_usuarioService.EliminarUsuarioPorDocumento(doc))
        {
            Console.WriteLine("\nUsuario eliminado con éxito.");
        }
        else
        {
            Console.WriteLine("\nUsuario no encontrado.");
        }
        PausaVolver("\nPresione una tecla para volver al menú de usuarios...");
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

            string input = Console.ReadLine() ?? "";

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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                PausaVolver();
            }

        } while (!back);
    }

    static void CreateLoan()
    {
        Console.Clear();
        Console.WriteLine("=== CREAR PRÉSTAMO ===");

        Console.Write("ID del préstamo (entero): ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nID inválido.");
            PausaVolver("\nPresione una tecla para volver...");
            return;
        }

        Console.Write("ISBN o # de lista del libro: ");
        string criterioLibro = Console.ReadLine() ?? "";
        Libro? libro = BuscarLibroPorIsbnONumero(criterioLibro);

        if (libro == null || !libro.Disponible)
        {
            Console.WriteLine("\nEl libro no existe o no está disponible.");
            PausaVolver("\nPresione una tecla para volver...");
            return;
        }

        Console.Write("Documento del usuario: ");
        string doc = (Console.ReadLine() ?? "").Trim();
        Usuario? usuario = _usuarioService.BuscarPorDocumento(doc);

        if (usuario == null || !usuario.Activo)
        {
            Console.WriteLine("\nEl usuario no existe o está inactivo.");
            PausaVolver("\nPresione una tecla para volver...");
            return;
        }

        Prestamo prestamo = new Prestamo
        {
            Id = id,
            LibroPrestado = libro,
            UsuarioPrestatario = usuario,
            FechaPrestamo = DateTime.Now,
            Estado = EstadoPrestamo.Activo
        };

        libro.Disponible = false;
        _prestamoService.AgregarPrestamo(prestamo);

        Console.WriteLine("\nPréstamo creado con éxito.");
        PausaVolver("\nPresione una tecla para volver al menú de préstamos...");
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

            string input = Console.ReadLine() ?? "";
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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida.");
                PausaVolver();
            }
        } while (!back);
    }

    static void ImprimirPrestamos(List<Prestamo> prestamos)
    {
        if (prestamos == null || prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos para mostrar.");
            return;
        }

        foreach (var p in prestamos)
        {
            string libroInfo = p.LibroPrestado?.Titulo ?? "(Sin libro)";
            string usuarioInfo = p.UsuarioPrestatario?.Nombre ?? "(Sin usuario)";
            Console.WriteLine($"ID: {p.Id} | Libro: {libroInfo} | Usuario: {usuarioInfo} | Fecha: {p.FechaPrestamo:yyyy-MM-dd} | Estado: {p.Estado}");
        }
    }

    static void ListLoansAll()
    {
        Console.Clear();
        Console.WriteLine("=== TODOS LOS PRÉSTAMOS ===");
        ImprimirPrestamos(_prestamoService.ObtenerTodos());
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ListLoansActive()
    {
        Console.Clear();
        Console.WriteLine("=== PRÉSTAMOS ACTIVOS ===");
        ImprimirPrestamos(_prestamoService.BuscarPorEstado(EstadoPrestamo.Activo));
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ListLoansClosed()
    {
        Console.Clear();
        Console.WriteLine("=== PRÉSTAMOS CERRADOS ===");
        ImprimirPrestamos(_prestamoService.BuscarPorEstado(EstadoPrestamo.Devuelto));
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ViewLoanDetail()
    {
        Console.Clear();
        Console.WriteLine("=== VER DETALLE DE PRÉSTAMO ===");
        Console.Write("Ingrese ID del préstamo: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nID inválido.");
            PausaVolver("\nPresione una tecla para volver...");
            return;
        }

        Prestamo? p = _prestamoService.BuscarPorId(id);
        if (p == null)
        {
            Console.WriteLine("\nPréstamo no encontrado.");
        }
        else
        {
            Console.WriteLine($"\nID: {p.Id}");
            Console.WriteLine($"Libro Prestado: {p.LibroPrestado?.Titulo ?? "(Sin libro)"} ({p.LibroPrestado?.Isbn ?? ""})");
            Console.WriteLine($"Usuario Prestatario: {p.UsuarioPrestatario?.Nombre ?? "(Sin usuario)"} ({p.UsuarioPrestatario?.Documento ?? ""})");
            Console.WriteLine($"Fecha Préstamo: {p.FechaPrestamo:yyyy-MM-dd}");
            Console.WriteLine($"Estado: {p.Estado}");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void RegisterReturn()
    {
        Console.Clear();
        Console.WriteLine("=== REGISTRAR DEVOLUCIÓN ===");
        Console.Write("Ingrese ID del préstamo: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nID inválido.");
            PausaVolver("\nPresione una tecla para volver...");
            return;
        }

        Prestamo? p = _prestamoService.BuscarPorId(id);
        if (p == null)
        {
            Console.WriteLine("\nPréstamo no encontrado.");
        }
        else
        {
            p.Estado = EstadoPrestamo.Devuelto;
            if (p.LibroPrestado != null)
            {
                p.LibroPrestado.Disponible = true;
            }
            Console.WriteLine("\nDevolución registrada con éxito.");
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void DeleteLoan()
    {
        Console.Clear();
        Console.WriteLine("=== ELIMINAR PRÉSTAMO ===");
        Console.Write("Ingrese ID del préstamo a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("\nID inválido.");
            PausaVolver("\nPresione una tecla para volver...");
            return;
        }

        Prestamo? p = _prestamoService.BuscarPorId(id);
        if (p != null && p.Estado == EstadoPrestamo.Activo && p.LibroPrestado != null)
        {
            p.LibroPrestado.Disponible = true;
        }

        if (_prestamoService.EliminarPrestamoPorId(id))
        {
            Console.WriteLine("\nPréstamo eliminado con éxito.");
        }
        else
        {
            Console.WriteLine("\nPréstamo no encontrado.");
        }
        PausaVolver("\nPresione una tecla para volver al menú de préstamos...");
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
            Console.WriteLine("3. Reporte de préstamos por libro y KPIs");
            Console.WriteLine("4. Reporte de vencimientos y retrasos");
            Console.WriteLine("5. Volver al menú principal");
            Console.Write("\nSeleccione una opción (1-5): ");

            string input = Console.ReadLine() ?? "";

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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Ingrese un número.");
                PausaVolver();
            }

        } while (!back);
    }

    static void SearchBooks()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR LIBROS ===");
        Console.Write("Ingrese título o autor: ");
        string texto = (Console.ReadLine() ?? "").Trim();

        var porTitulo = _libroService.BuscarPorTitulo(texto);
        var porAutor = _libroService.BuscarPorAutor(texto);
        var resultados = porTitulo.Union(porAutor).ToList();

        ImprimirLibros(resultados);
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void SearchUsers()
    {
        Console.Clear();
        Console.WriteLine("=== BUSCAR USUARIOS ===");
        Console.Write("Ingrese documento o nombre: ");
        string texto = (Console.ReadLine() ?? "").Trim();

        var resultados = new List<Usuario>();
        var porDoc = _usuarioService.BuscarPorDocumento(texto);
        if (porDoc != null) resultados.Add(porDoc);
        resultados.AddRange(_usuarioService.BuscarPorNombre(texto));
        resultados = resultados.Distinct().ToList();

        if (resultados.Count == 0)
        {
            Console.WriteLine("\nNo se encontraron usuarios.");
        }
        else
        {
            foreach (var u in resultados)
            {
                Console.WriteLine($"Documento: {u.Documento} | Nombre: {u.Nombre} | Email: {u.Email} | Activo: {(u.Activo ? "Sí" : "No")}");
            }
        }
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ReportMostBorrowedBooks()
    {
        Console.Clear();
        Console.WriteLine("=== REPORTE DE PRÉSTAMOS POR LIBRO Y KPIs ===");

        var libros = _libroService.ObtenerTodos();
        var prestamos = _prestamoService.ObtenerTodos();

        if (libros.Count == 0)
        {
            Console.WriteLine("No hay libros registrados para calcular conteo.");
        }
        else
        {
            var ranking = libros
                .Select(l => new
                {
                    Libro = l,
                    CantidadPrestamos = prestamos.Count(p =>
                        p.LibroPrestado != null &&
                        p.LibroPrestado.Isbn.Equals(l.Isbn, StringComparison.OrdinalIgnoreCase))
                })
                .OrderByDescending(x => x.CantidadPrestamos)
                .ThenBy(x => x.Libro.Titulo)
                .ToList();

            foreach (var item in ranking)
            {
                Console.WriteLine($"- {item.Libro.Titulo} [{item.Libro.Isbn}]: {item.CantidadPrestamos} préstamo(s) | Disponible: {(item.Libro.Disponible ? "Sí" : "No")}");
            }
        }

        Console.WriteLine("\n=== KPIs GENERALES ===");
        Console.WriteLine($"Libros -> Total: {_libroService.TotalLibros()} | Disponibles: {_libroService.ContarLibrosDisponibles()} | Prestados: {_libroService.ContarLibrosPrestados()}");
        Console.WriteLine($"Usuarios -> Total: {_usuarioService.TotalUsuarios()} | Activos: {_usuarioService.ContarUsuariosActivos()} | Inactivos: {_usuarioService.ContarUsuariosInactivos()}");
        Console.WriteLine($"Préstamos -> Total: {_prestamoService.TotalPrestamos()} | Activos: {_prestamoService.ContarPrestamosActivos()} | Vencidos: {_prestamoService.ContarPrestamosVencidos()} | Devueltos: {_prestamoService.ContarPrestamosDevueltos()} | Promedio días: {_prestamoService.PromedioDiasPrestamo():F1}");

        string[] arregloFijo = new string[2] { "Libro A", "Libro B" };
        List<string> listaDinamica = new List<string> { "Libro A", "Libro B" };
        listaDinamica.Add("Libro C");
        Console.WriteLine($"\nArray vs List<T> -> Array.Length: {arregloFijo.Length} (fijo) | List<T>.Count: {listaDinamica.Count} (dinámico)");
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void ReportOverdueLoans()
    {
        Console.Clear();
        Console.WriteLine("=== REPORTE DE VENCIMIENTOS Y RETRASOS ===");

        var prestamos = _prestamoService.ObtenerTodos();
        if (prestamos.Count == 0)
        {
            Console.WriteLine("No hay préstamos registrados.");
            PausaVolver("\nPresione una tecla para volver...");
            return;
        }

        const int diasLimite = 7;
        const int tarifaDiaria = 1000;

        foreach (var p in prestamos)
        {
            int diasTranscurridos = Math.Max(0, (DateTime.Now.Date - p.FechaPrestamo.Date).Days);
            int diasRetraso = Math.Max(0, diasTranscurridos - diasLimite);
            int multaEstimada = diasRetraso * tarifaDiaria;
            string estadoVencimiento = p.EstaVencido() ? "VENCIDO" : "AL DÍA";

            Console.WriteLine($"ID: {p.Id} | Libro: {p.LibroPrestado?.Titulo ?? "(Sin libro)"} | Estado: {p.Estado} | Días: {diasTranscurridos} | {estadoVencimiento} | Multa estimada: ${multaEstimada}");
        }

        PausaVolver("\nPresione una tecla para volver...");
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

            string input = Console.ReadLine() ?? "";

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
                        PausaVolver();
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Ingrese un número.");
                PausaVolver();
            }

        } while (!back);
    }

    static void SaveDataToFile()
    {
        Console.Clear();
        Console.WriteLine("=== GUARDAR DATOS ===");
        Console.WriteLine("[Acción] Serializando y guardando catálogos (Libros, Usuarios, Préstamos)...");
        PausaVolver("\nPresione una tecla para volver...");
    }

    static void LoadDataFromFile()
    {
        Console.Clear();
        Console.WriteLine("=== CARGAR DATOS ===");
        Console.WriteLine("[Acción] Leyendo y cargando información desde archivos simulados...");
        PausaVolver("\nPresione una tecla para volver...");
    }
}