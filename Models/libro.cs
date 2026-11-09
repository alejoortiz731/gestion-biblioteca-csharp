using System;

namespace SystemBooks.Models
{
    public class Libro
    {
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public int AnioPublicacion { get; set; }
        public bool Disponible { get; set; }

        // Constructor vacío
        public Libro()
        {
            Disponible = true; // Condición solicitada
        }

        // Constructor completo
        public Libro(string isbn, string titulo, string autor, int anioPublicacion)
        {
            Isbn = isbn;
            Titulo = titulo;
            Autor = autor;
            AnioPublicacion = anioPublicacion;
            Disponible = true; // Condición solicitada
        }

        public string ResumenCorto()
        {
            return $"[Libro] {Titulo} - {Autor} ({(Disponible ? "Disponible" : "Prestado")})";
        }

        public string DetalleCompleto()
        {
            return $"ISBN: {Isbn} | Título: {Titulo} | Autor: {Autor} | Año: {AnioPublicacion} | Estado: {(Disponible ? "Disponible" : "Prestado")}";
        }

        public override string ToString()
        {
            return DetalleCompleto();
        }
    }
}