namespace TFI_1.EFCore.Entidades;

public enum TipoItem
{
	Servicio,
	Producto,
}

public enum Modalidad
{
	Turnos,
	OrdenLlegada,
}

public enum TipoAtencion
{
	Turno,
	Llegada,
}

public enum EstadoAtencion
{
	Pendiente,
	EnAtencion,
	Finalizada,
	Cancelada,
}

public enum TipoMovimientoCuentaCorriente
{
	Cargo,
	Abono,
}
