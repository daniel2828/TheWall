using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
/// <summary>
/// Summary description for C_cuentas
/// </summary>
public class C_rooms
{
    public C_rooms()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static int id_room { get; set; }
    public string nombre_room { get; set; }
    public int guardarRoomAdministrador(string nombreRoom)
    { 
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {
            string query = "sp_gestionRoom";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombreRoom);
            comando.Parameters.AddWithValue("@idAdministrador", C_administradores.codigo);
            comando.Parameters.AddWithValue("@codigoOperacion", 0);
            conexion.Open();
            resultado = comando.ExecuteNonQuery();
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }
    public int borrarRoomAdministrador(int id_room)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {
            string query = "sp_gestionRoom";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@id_room", id_room);
            comando.Parameters.AddWithValue("@codigoOperacion", 2);
            conexion.Open();
             resultado = comando.ExecuteNonQuery();
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }
    public int modificarRoomAdministrador(int id_room, string nombreRoomNuevo)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {
            string query = "sp_gestionRoom";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@nombre", nombreRoomNuevo);
            comando.Parameters.AddWithValue("@id_room", id_room);
            comando.Parameters.AddWithValue("@codigoOperacion", 1);
            conexion.Open();
            resultado = comando.ExecuteNonQuery();
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }


}