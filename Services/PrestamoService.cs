using System;
using System.Collections.Generic;
using System.Linq;
using SystemBooks.Models;

namespace SystemBooks.Services
{
    public class PrestamoService
    {
        private readonly List<Prestamo> _prestamos = new List<Prestamo>();
        private const int DiasLimitePrestamo = 7;

        // 1. Operaciones básicas
        public void AgregarPrestamo(Prestamo prestamo)
        {
            if (prestamo != null)
            {
                _prestamos.Add(prestamo);
            }
        }

        public bool EliminarPrestamoPorId(int id)
        {
            Prestamo prestamoEncontrado = BuscarPorId(id);
            if (prestamoEncontrado != null)
            {
                return _prestamos.Remove(prestamoEncontrado);
            }
            return false;
        }

        public List<Prestamo> ObtenerTodos()
        {
            return new List<Prestamo>(_prestamos);
        }

        // 2. Métodos de búsqueda
        public Prestamo BuscarPorId(int id)
        {
            return _prestamos.FirstOrDefault(p => p.Id == id);
        }

        public List<Prestamo> BuscarPorEstado(EstadoPrestamo estado)
        {
            return _prestamos.Where(p => p.Estado == estado).ToList();
        }

        // 3. Ordenación por fecha límite (FechaPrestamo + 7 días)
        public List<Prestamo> OrdenarPorFechaLimite(bool descendente = false)
        {
            return descendente
                ? _prestamos.OrderByDescending(p => p.FechaPrestamo.AddDays(DiasLimitePrestamo)).ToList()
                : _prestamos.OrderBy(p => p.FechaPrestamo.AddDays(DiasLimitePrestamo)).ToList();
        }

        // 4. KPIs y estadísticas
        public int TotalPrestamos()
        {
            return _prestamos.Count;
        }

        public int ContarPrestamosActivos()
        {
            return _prestamos.Count(p => p.Estado == EstadoPrestamo.Activo && !p.EstaVencido());
        }

        public int ContarPrestamosVencidos()
        {
            return _prestamos.Count(p => p.EstaVencido());
        }

        public int ContarPrestamosDevueltos()
        {
            return _prestamos.Count(p => p.Estado == EstadoPrestamo.Devuelto);
        }

        public double PromedioDiasPrestamo()
        {
            if (_prestamos.Count == 0) return 0;
            return _prestamos.Average(p => p.DiasTranscurridos());
        }
    }
}