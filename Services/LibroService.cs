using System;
using System.Collections.Generic;
using System.Linq;
using SystemBooks.Models;

namespace SystemBooks.Services
{
    public class LibroService
    {
        private readonly List<Libro> _libros = new List<Libro>();

        // 1. Operaciones básicas
        public void AgregarLibro(Libro libro)
        {
            if (libro != null)
            {
                _libros.Add(libro);
            }
        }

        public bool EliminarLibroPorIsbn(string isbn)
        {
            Libro libroEncontrado = BuscarPorIsbn(isbn);
            if (libroEncontrado != null)
            {
                return _libros.Remove(libroEncontrado);
            }
            return false;
        }

        public List<Libro> ObtenerTodos()
        {
            return new List<Libro>(_libros);
        }

        // 2. Métodos de búsqueda
        public Libro BuscarPorIsbn(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn)) return null;
            return _libros.FirstOrDefault(l => l.Isbn.Equals(isbn.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<Libro> BuscarPorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo)) return new List<Libro>();
            return _libros
                .Where(l => l.Titulo.Contains(titulo.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Libro> BuscarPorAutor(string autor)
        {
            if (string.IsNullOrWhiteSpace(autor)) return new List<Libro>();
            return _libros
                .Where(l => l.Autor.Contains(autor.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // 3. Métodos de ordenación
        public List<Libro> OrdenarPorTitulo()
        {
            return _libros.OrderBy(l => l.Titulo).ToList();
        }

        public List<Libro> OrdenarPorAnio(bool descendente = false)
        {
            return descendente
                ? _libros.OrderByDescending(l => l.AnioPublicacion).ToList()
                : _libros.OrderBy(l => l.AnioPublicacion).ToList();
        }

        // 4. KPIs y estadísticas
        public int TotalLibros()
        {
            return _libros.Count;
        }

        public int ContarLibrosDisponibles()
        {
            return _libros.Count(l => l.Disponible);
        }

        public int ContarLibrosPrestados()
        {
            return _libros.Count(l => !l.Disponible);
        }
    }
}