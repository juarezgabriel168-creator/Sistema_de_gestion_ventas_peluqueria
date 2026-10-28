using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class ClienteRepositorio
{
	public List<Cliente> Listar(string? filtro = null)
	{
		using var db = new PeluqueriaDbContext();
		IQueryable<Cliente> consulta = db.Clientes.AsNoTracking();

		if (!string.IsNullOrEmpty(filtro))
			consulta = consulta.Where(c =>
				c.Dni.Contains(filtro) ||
				c.Apellido.Contains(filtro) ||
				c.Nombre.Contains(filtro));

		return consulta.OrderBy(c => c.Apellido).ThenBy(c => c.Nombre).ToList();
	}

	public bool ExisteDni(string dni, int? exceptoId = null)
	{
		using var db = new PeluqueriaDbContext();
		return db.Clientes.Any(c =>
			c.Dni == dni &&
			(exceptoId == null || c.Id != exceptoId));
	}

	public Cliente? ObtenerPorDni(string dni)
	{
		using var db = new PeluqueriaDbContext();
		return db.Clientes.AsNoTracking().SingleOrDefault(c => c.Dni == dni);
	}

	public int Insertar(Cliente cliente)
	{
		using var db = new PeluqueriaDbContext();
		var nuevo = new Cliente
		{
			Nombre = cliente.Nombre,
			Apellido = cliente.Apellido,
			Dni = cliente.Dni,
			Telefono = Comun.Limpiar(cliente.Telefono),
			Activo = cliente.Activo,
		};

		db.Clientes.Add(nuevo);
		db.SaveChanges();
		cliente.Id = nuevo.Id;
		return nuevo.Id;
	}

	public void Actualizar(Cliente cliente)
	{
		using var db = new PeluqueriaDbContext();
		Cliente actual = db.Clientes.Find(cliente.Id)
			?? throw new InvalidOperationException("El cliente no existe.");

		actual.Nombre = cliente.Nombre;
		actual.Apellido = cliente.Apellido;
		actual.Dni = cliente.Dni;
		actual.Telefono = Comun.Limpiar(cliente.Telefono);
		actual.Activo = cliente.Activo;
		db.SaveChanges();
	}

	public void Desactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Clientes.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, false));
	}

	public void Reactivar(int id)
	{
		using var db = new PeluqueriaDbContext();
		db.Clientes.Where(c => c.Id == id).ExecuteUpdate(s => s.SetProperty(c => c.Activo, true));
	}

	public bool EsConsumidorFinal(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Clientes.Any(c => c.Id == id && c.Dni == "00000000");
	}

	public bool TieneDependencias(int id)
	{
		using var db = new PeluqueriaDbContext();
		return db.Ventas.Any(v => v.IdCliente == id)
			|| db.CuentasCorrientes.Any(c => c.IdCliente == id)
			|| db.Atenciones.Any(a => a.IdCliente == id);
	}

	public void Eliminar(int id)
	{
		using var db = new PeluqueriaDbContext();
		Cliente? cliente = db.Clientes.Find(id);
		if (cliente == null)
			return;

		db.Clientes.Remove(cliente);
		db.SaveChanges();
	}
}
