using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TFI_1.EFCore.Dtos;
using TFI_1.EFCore.Entidades;

namespace TFI_1.EFCore.Repositorios;

public class AuditoriaRepositorio
{
	public void Registrar(int usuarioId, string accion, string entidad, int? idEntidad, string? detalle)
	{
		using var db = new PeluqueriaDbContext();
		db.Auditorias.Add(new Auditoria
		{
			IdUsuario = usuarioId,
			Accion = accion,
			Entidad = entidad,
			IdEntidad = idEntidad,
			Detalle = string.IsNullOrWhiteSpace(detalle) ? null : detalle,
		});
		db.SaveChanges();
	}

	public List<AuditoriaListadoDto> Listar(DateTime desde, DateTime hasta, int? usuarioId,
		string? accion, int? limite = null, int? desplazamiento = null)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		IQueryable<Auditoria> consulta = db.Auditorias.AsNoTracking()
			.Where(a => a.Fecha >= inicio && a.Fecha < fin);

		if (usuarioId.HasValue)
			consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);
		if (!string.IsNullOrWhiteSpace(accion))
			consulta = consulta.Where(a => a.Accion == accion);

		IQueryable<AuditoriaListadoDto> proyeccion = consulta
			.OrderByDescending(a => a.Fecha)
			.ThenByDescending(a => a.Id)
			.Select(a => new AuditoriaListadoDto
			{
				Id = a.Id,
				Fecha = a.Fecha,
				Usuario = a.Usuario.NombreUsuario,
				Codigo = a.Accion,
				Accion = string.Empty,
				Entidad = a.Entidad,
				IdEntidad = a.IdEntidad,
				Detalle = a.Detalle,
			});

		List<AuditoriaListadoDto> filas = Comun.Paginar(proyeccion, limite, desplazamiento).ToList();
		foreach (AuditoriaListadoDto fila in filas)
			fila.Accion = TraducirAccion(fila.Codigo);

		return filas;
	}

	public int Contar(DateTime desde, DateTime hasta, int? usuarioId, string? accion)
	{
		using var db = new PeluqueriaDbContext();
		(DateTime inicio, DateTime fin) = Comun.Rango(desde, hasta);

		IQueryable<Auditoria> consulta = db.Auditorias.AsNoTracking()
			.Where(a => a.Fecha >= inicio && a.Fecha < fin);

		if (usuarioId.HasValue)
			consulta = consulta.Where(a => a.IdUsuario == usuarioId.Value);
		if (!string.IsNullOrWhiteSpace(accion))
			consulta = consulta.Where(a => a.Accion == accion);

		return consulta.Count();
	}

	public List<AccionAuditoriaDto> Acciones()
	{
		using var db = new PeluqueriaDbContext();
		return db.Auditorias.AsNoTracking()
			.Select(a => a.Accion)
			.Distinct()
			.ToList()
			.Select(codigo => new AccionAuditoriaDto
			{
				Codigo = codigo,
				Nombre = TraducirAccion(codigo),
			})
			.OrderBy(a => a.Nombre)
			.ToList();
	}

	private static string TraducirAccion(string accion) => accion switch
	{
		"venta" => "Venta",
		"anulacion_venta" => "Anulación de venta",
		"abono" => "Pago de cuenta corriente",
		"gasto_alta" => "Alta de gasto",
		"gasto_edicion" => "Edición de gasto",
		"ingreso_mercaderia" => "Ingreso de mercadería",
		"usuario_alta" => "Alta de usuario",
		"usuario_edicion" => "Edición de usuario",
		"usuario_desactivacion" => "Desactivación de usuario",
		"usuario_reactivacion" => "Reactivación de usuario",
		"caja_apertura" => "Apertura de caja",
		"caja_cierre" => "Cierre de caja",
		"caja_ingreso" => "Ingreso de caja",
		"caja_egreso" => "Egreso de caja",
		"backup_generado" => "Copia de seguridad generada",
		_ => accion,
	};
}
