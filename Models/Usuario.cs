using System;

namespace SystemBooks.Models
{
    public class Usuario
    {
        public string Documento { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public bool Activo { get; set; }

        // Constructor vacío (inicializa Activo en true por condición de la rúbrica)
        public Usuario()
        {
            Activo = true;
        }

        // Constructor completo
        public Usuario(string documento, string nombre, string email)
        {
            Documento = documento;
            Nombre = nombre;
            Email = email;
            Activo = true;
        }

        public string ResumenCorto()
        {
            return $"[Usuario] {Nombre} (Doc: {Documento}) - {(Activo ? "Activo" : "Inactivo")}";
        }

        public string DetalleCompleto()
        {
            return $"Documento: {Documento} | Nombre: {Nombre} | Email: {Email} | Estado: {(Activo ? "Activo" : "Inactivo")}";
        }

        public override string ToString()
        {
            return DetalleCompleto();
        }
    }
}