using System;
using System.Collections.Generic;
using System.Linq;
using SystemBooks.Models;

namespace SystemBooks.Services
{
    public class UsuarioService
    {
        private readonly List<Usuario> _usuarios = new List<Usuario>();

        // 1. Operaciones básicas
        public void AgregarUsuario(Usuario usuario)
        {
            if (usuario != null)
            {
                _usuarios.Add(usuario);
            }
        }

        public bool EliminarUsuarioPorDocumento(string documento)
        {
            Usuario usuarioEncontrado = BuscarPorDocumento(documento);
            if (usuarioEncontrado != null)
            {
                return _usuarios.Remove(usuarioEncontrado);
            }
            return false;
        }

        public List<Usuario> ObtenerTodos()
        {
            return new List<Usuario>(_usuarios);
        }

        // 2. Métodos de búsqueda
        public Usuario BuscarPorDocumento(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento)) return null;
            return _usuarios.FirstOrDefault(u => u.Documento.Equals(documento.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        public List<Usuario> BuscarPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return new List<Usuario>();
            return _usuarios
                .Where(u => u.Nombre.Contains(nombre.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // 3. Método de ordenación
        public List<Usuario> OrdenarPorNombre()
        {
            return _usuarios.OrderBy(u => u.Nombre).ToList();
        }

        // 4. KPIs y estadísticas
        public int TotalUsuarios()
        {
            return _usuarios.Count;
        }

        public int ContarUsuariosActivos()
        {
            return _usuarios.Count(u => u.Activo);
        }

        public int ContarUsuariosInactivos()
        {
            return _usuarios.Count(u => !u.Activo);
        }
    }
}