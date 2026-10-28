using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class UsuarioRepositorio
{
	public Usuario? ObtenerPorNombre(string nombreUsuario)
	{
		using var db = new PeluqueriaDbContext();
		return db.Usuarios
			.AsNoTracking()
			.Include(u => u.Rol)
			.SingleOrDefault(u => u.NombreUsuario == nombreUsuario);
	}

	public List<Usuario> Listar(string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Usuario> consulta = db.Usuarios.AsNoTracking().Include(u => u.Rol);

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(u => u.NombreUsuario.Contains(filtro));

		return consulta.OrderBy(u => u.NombreUsuario).ToList();
	}

	public List<Rol> ObtenerRoles()
	{
		using var db = new PeluqueriaDbContext();
		return db.Roles.AsNoTracking().OrderBy(r => r.Nombre).ToList();
	}

	public bool ExisteNombre(string nombreUsuario, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.Usuarios.Any(u =>
			u.NombreUsuario == nombreUsuario &&
			(exceptoId == null || u.Id != exceptoId));
	}

	public int Insertar(Usuario usuario)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new Usuario
		{
			NombreUsuario = usuario.NombreUsuario,
			Contrasena = usuario.Contrasena,
			IdRol = usuario.IdRol != 0 ? usuario.IdRol : usuario.Rol.Id,
			Activo = usuario.Activo,
		};

		db.Usuarios.Add(nuevo);
		db.SaveChanges();
		usuario.Id = nuevo.Id;
		return nuevo.Id;
	}

	public void Actualizar(Usuario usuario)
	{
		using var db = new PeluqueriaDbContext();
		Usuario actual = db.Usuarios.Find(usuario.Id)
			?? throw new InvalidOperationException("El usuario no existe.");

		actual.NombreUsuario = usuario.NombreUsuario;
		actual.Contrasena = usuario.Contrasena;
		actual.IdRol = usuario.IdRol != 0 ? usuario.IdRol : usuario.Rol.Id;
		actual.Activo = usuario.Activo;
		db.SaveChanges();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Usuarios.Where(u => u.Id == id).ExecuteUpdate(s => s.SetProperty(u => u.Activo, false));
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Usuarios.Where(u => u.Id == id).ExecuteUpdate(s => s.SetProperty(u => u.Activo, true));
	}

	public bool TieneDependencias(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Ventas.Any(v => v.IdUsuario == id || v.IdUsuarioAnulacion == id)
			|| db.MovimientosCuentaCorriente.Any(m => m.IdUsuario == id)
			|| db.Gastos.Any(g => g.IdUsuario == id)
			|| db.IngresosMercaderia.Any(i => i.IdUsuario == id)
			|| db.Auditorias.Any(a => a.IdUsuario == id)
			|| db.Cajas.Any(c => c.IdUsuarioApertura == id || c.IdUsuarioCierre == id)
			|| db.CajaMovimientos.Any(m => m.IdUsuario == id);
	}

	public bool EsUltimoAdministrador(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Usuarios.Any(u => u.Id == id && u.IdRol == 1 && u.Activo)
			&& !db.Usuarios.Any(u => u.IdRol == 1 && u.Activo && u.Id != id);
	}

	public void Eliminar(int id)
	{
		using var db = new PeluqueriaDbContext();
		Usuario? usuario = db.Usuarios.Find(id);
		if (usuario == null)
			return;

		db.Usuarios.Remove(usuario);
		db.SaveChanges();
	}
}
