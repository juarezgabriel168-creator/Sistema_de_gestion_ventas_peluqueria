using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class CategoriaRepositorio
{
	public List<Categoria> Listar(string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Categoria> consulta = db.Categorias.AsNoTracking();

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(c => c.Nombre.Contains(filtro));

		return consulta.OrderBy(c => c.Nombre).ToList();
	}

	public bool ExisteNombre(string nombre, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.Categorias.Any(c =>
			c.Nombre == nombre &&
			(exceptoId == null || c.Id != exceptoId));
	}

	public int Insertar(Categoria categoria)
	{
		using var db = new PeluqueriaDbContext();
		var nueva = new Categoria { Nombre = categoria.Nombre, Activo = categoria.Activo };
		db.Categorias.Add(nueva);
		db.SaveChanges();
		categoria.Id = nueva.Id;
		return nueva.Id;
	}

	public void Actualizar(Categoria categoria)
	{
		using var db = new PeluqueriaDbContext();
		Categoria actual = db.Categorias.Find(categoria.Id)
			?? throw new InvalidOperationException("La categoría no existe.");

		actual.Nombre = categoria.Nombre;
		actual.Activo = categoria.Activo;
		db.SaveChanges();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Categorias.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, false));
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Categorias.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, true));
	}

	public bool TieneDependencias(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Items.Any(i => i.IdCategoria == id);
	}

	public void Eliminar(int id)
	{
		using var db = new PeluqueriaDbContext();
		Categoria? categoria = db.Categorias.Find(id);
		if (categoria == null)
			return;

		db.Categorias.Remove(categoria);
		db.SaveChanges();
	}
}
