using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using TFI_1.Models;

namespace TFI_1.Datos;

public class BancoDatos
{
    public List<Banco> Listar(bool soloActivos = true)
    {
        List<Banco> lista = new();
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "SELECT id_banco, nombre, activo FROM bancos " +
            "WHERE (@soloActivos = 0 OR activo = 1) ORDER BY nombre;", conexion);
        cmd.Parameters.Add(Parametros.Entero("@soloActivos", soloActivos ? 1 : 0));
        conexion.Open();
        using MySqlDataReader rd = cmd.ExecuteReader();
        while (rd.Read())
            lista.Add(new Banco
            {
                Id = rd.GetInt32("id_banco"),
                Nombre = rd.GetString("nombre"),
                Activo = rd.GetBoolean("activo")
            });
        return lista;
    }

    public bool ExisteNombre(string nombre, int? exceptoId = null)
    {
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "SELECT COUNT(*) FROM bancos WHERE nombre = @nombre " +
            "AND (@exceptoId IS NULL OR id_banco <> @exceptoId);", conexion);
        cmd.Parameters.Add(Parametros.Texto("@nombre", nombre));
        cmd.Parameters.Add(Parametros.EnteroNulo("@exceptoId", exceptoId));
        conexion.Open();
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }

    public int Insertar(Banco banco)
    {
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "INSERT INTO bancos (nombre, activo) VALUES (@nombre, @activo); " +
            "SELECT LAST_INSERT_ID();", conexion);
        cmd.Parameters.Add(Parametros.Texto("@nombre", banco.Nombre));
        cmd.Parameters.Add(Parametros.Booleano("@activo", banco.Activo));
        conexion.Open();
        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    public void Actualizar(Banco banco)
    {
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "UPDATE bancos SET nombre = @nombre, activo = @activo WHERE id_banco = @id;", conexion);
        cmd.Parameters.Add(Parametros.Entero("@id", banco.Id));
        cmd.Parameters.Add(Parametros.Texto("@nombre", banco.Nombre));
        cmd.Parameters.Add(Parametros.Booleano("@activo", banco.Activo));
        conexion.Open();
        cmd.ExecuteNonQuery();
    }

    // Al desactivar un banco también se desactivan sus tarjetas.
    public void Desactivar(int id)
    {
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "UPDATE bancos SET activo = 0 WHERE id_banco = @id; " +
            "UPDATE tarjetas_credito SET activo = 0 WHERE id_banco = @id;", conexion);
        cmd.Parameters.Add(Parametros.Entero("@id", id));
        conexion.Open();
        cmd.ExecuteNonQuery();
    }

    // Al reactivar un banco también se reactivan sus tarjetas.
    public void Reactivar(int id)
    {
        using MySqlConnection conexion = ConexionBD.ObtenerConexion();
        using MySqlCommand cmd = new MySqlCommand(
            "UPDATE bancos SET activo = 1 WHERE id_banco = @id; " +
            "UPDATE tarjetas_credito SET activo = 1 WHERE id_banco = @id;", conexion);
        cmd.Parameters.Add(Parametros.Entero("@id", id));
        conexion.Open();
        cmd.ExecuteNonQuery();
    }
}