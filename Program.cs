using System;
using SystemBooks.Models;

namespace SystemBooks
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("    EVALUACIÓN INTEGRAL - MODELOS POO (SENA)      ");
            Console.WriteLine("==================================================");

            // 1. Instanciar un Libro
            Libro miLibro = new Libro("978-0321125217", "Domain-Driven Design", "Eric Evans", 2003);
            Console.WriteLine("\n[1] Libro Registrado:");
            Console.WriteLine(miLibro.DetalleCompleto());

            // 2. Instanciar un Usuario
            Usuario miUsuario = new Usuario("1020304050", "Carlos Perez", "carlos.perez@sena.edu.co");
            Console.WriteLine("\n[2] Usuario Registrado:");
            Console.WriteLine(miUsuario.DetalleCompleto());

            // 3. Simular un Préstamo (Cambiando el estado del libro a prestado)
            miLibro.Disponible = false;
            Prestamo miPrestamo = new Prestamo(1, miLibro, miUsuario);

            // Simulamos que el préstamo se hizo hace 9 días para probar la lógica de vencimiento
            miPrestamo.FechaPrestamo = DateTime.Now.AddDays(-9);

            Console.WriteLine("\n[3] Transacción de Préstamo:");
            Console.WriteLine(miPrestamo.DetalleCompleto());
            Console.WriteLine($"Días transcurridos desde el préstamo: {miPrestamo.DiasTranscurridos()} días");
            Console.WriteLine($"¿El préstamo está vencido?: {(miPrestamo.EstaVencido() ? "SÍ (Alerta de mora)" : "NO")}");

            Console.WriteLine("\n==================================================");
            Console.WriteLine("Prueba finalizada con éxito. Presiona una tecla...");
            Console.ReadKey();
        }
    }
}   