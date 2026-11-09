using System;

namespace SystemBooks.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public Libro LibroPrestado { get; set; }
        public Usuario UsuarioPrestatario { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; } // Nullable para cuando aún no se ha devuelto
        public EstadoPrestamo Estado { get; set; }

        // Constructor vacío
        public Prestamo()
        {
            FechaPrestamo = DateTime.Now;
            Estado = EstadoPrestamo.Activo;
            FechaDevolucion = null;
        }

        // Constructor completo
        public Prestamo(int id, Libro libro, Usuario usuario)
        {
            Id = id;
            LibroPrestado = libro;
            UsuarioPrestatario = usuario;
            FechaPrestamo = DateTime.Now;
            Estado = EstadoPrestamo.Activo;
            FechaDevolucion = null;
        }

        // Lógica de negocio: Verificar si el préstamo está vencido (más de 7 días sin devolver)
        public bool EstaVencido()
        {
            if (Estado == EstadoPrestamo.Devuelto)
            {
                return false;
            }

            // Si pasan más de 7 días desde la fecha de préstamo y sigue activo
            return (DateTime.Now - FechaPrestamo).TotalDays > 7;
        }

        // Método para calcular días transcurridos
        public int DiasTranscurridos()
        {
            DateTime fechaFinal = FechaDevolucion ?? DateTime.Now;
            return (int)(fechaFinal - FechaPrestamo).TotalDays;
        }

        public string DetalleCompleto()
        {
            string devolucionStr = FechaDevolucion.HasValue ? FechaDevolucion.Value.ToShortDateString() : "Pendiente";
            return $"Préstamo #{Id} | Libro: {LibroPrestado.Titulo} | Usuario: {UsuarioPrestatario.Nombre} | " +
                $"Fecha: {FechaPrestamo.ToShortDateString()} | Estado: {Estado} | Vencido: {(EstaVencido() ? "Sí" : "No")}";
        }

        public override string ToString()
        {
            return DetalleCompleto();
        }
    }
}