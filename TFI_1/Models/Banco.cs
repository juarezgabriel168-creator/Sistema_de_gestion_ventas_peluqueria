namespace TFI_1.Models;

public class Banco
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;

    public override string ToString() => Nombre;

}