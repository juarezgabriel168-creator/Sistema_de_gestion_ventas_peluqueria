using System;
using System.Collections.Generic;
using TFI_1.Datos;
using TFI_1.Models;

namespace TFI_1.Negocio;

public class BancoNegocio
{
	private readonly BancoDatos _datos = new();

	public List<Banco> Listar(bool soloActivos = true) => _datos.Listar(soloActivos);

	public int Insertar(Banco banco)
	{
		Validar(banco);
		return _datos.Insertar(banco);
	}

	public void Actualizar(Banco banco)
	{
		if (banco.Id <= 0) throw new ArgumentException("Banco inválido.");
		Validar(banco);
		_datos.Actualizar(banco);
	}

	public void Desactivar(int id)
	{
		if (id <= 0) throw new ArgumentException("Banco inválido.");
		_datos.Desactivar(id);
	}

	public void Reactivar(int id)
	{
		if (id <= 0) throw new ArgumentException("Banco inválido.");
		_datos.Reactivar(id);
	}

	private void Validar(Banco banco)
	{
		if (banco == null || string.IsNullOrWhiteSpace(banco.Nombre))
			throw new ArgumentException("Debe ingresar el nombre del banco.");
		banco.Nombre = banco.Nombre.Trim();
		if (_datos.ExisteNombre(banco.Nombre, banco.Id > 0 ? banco.Id : null))
			throw new ArgumentException($"Ya existe el banco '{banco.Nombre}'.");
	}
}
