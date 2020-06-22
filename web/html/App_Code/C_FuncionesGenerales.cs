﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

/// <summary>
/// Descripción breve de C_FuncionesGenerales
/// </summary>
public class C_FuncionesGenerales
{
	public C_FuncionesGenerales()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
  
    public static string strConexion
    {
        get { return ConfigurationManager.ConnectionStrings["SqlConnection"].ToString(); }
    }

    public static string EncriptarCodigos ( string cadena )
    {
        string claveHex = "156A68E3DE46A8C586BCDF6C882D9D48";
        string vectorHex = "F3ECBA39CC0742540CFC7FCCF3CA50E2";
        string telefonoENC;
        string cadenaConPadding = cadena;
        for (int i = cadena.Length + 1; i <= 16; i++)
        {
            cadenaConPadding = cadenaConPadding + "*";
        }
        try
        {

            byte[] key = new byte[16];
            byte[] IV = new byte[16];
            byte[] decrypted = new byte[16];
            for (int i = 0; i <= 15; i++)
            {
                int p = i * 2;
                key[i] = Convert.ToByte(claveHex.Substring(p, 2), 16);
                IV[i] = Convert.ToByte(vectorHex.Substring(p, 2), 16);

                string valor = ((int)(Convert.ToChar(cadenaConPadding.Substring(i, 1)))).ToString("X");
                string clave = "";
                if (valor.Length == 1)
                {
                    clave = "0";
                }
                clave += valor;
                decrypted[i] = Convert.ToByte(clave, 16);
            }


            Rijndael miRijndael = Rijndael.Create();
            miRijndael.Padding = PaddingMode.None;
            miRijndael.Mode = CipherMode.CBC;
            // encriptar
            ICryptoTransform encryptor = miRijndael.CreateEncryptor(key, IV);
            byte[] fromPlain = new byte[decrypted.Length];
            System.IO.MemoryStream msEncrypt = new System.IO.MemoryStream(decrypted);
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Read);
            csEncrypt.Read(fromPlain, 0, fromPlain.Length);
            telefonoENC = "";
            for (int k = 0; k <= 15; k++)
            {
                //string valor = fromPlain[k].ToString("X");
                string valor = String.Format("{0:x2}", (uint)System.Convert.ToUInt32(fromPlain[k].ToString()));
                if (valor.Length == 1)
                {
                    telefonoENC += "0";
                }
                telefonoENC += valor;
            }
            return telefonoENC;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

    }

    public static string DesencriptarCodigos ( string cadena )
    {
        string claveHex = "156A68E3DE46A8C586BCDF6C882D9D48";
        string vectorHex = "F3ECBA39CC0742540CFC7FCCF3CA50E2";
        byte[] key = new byte[16];
        byte[] IV = new byte[16];
        byte[] encrypted = new byte[16];
        // int i =0;
        for (int i = 0; i <= 15; i++)
        {
            int p = i * 2;
            key[i] = Convert.ToByte(claveHex.Substring(p, 2), 16);
            IV[i] = Convert.ToByte(vectorHex.Substring(p, 2), 16);

            encrypted[i] = Convert.ToByte(cadena.Substring(p, 2), 16);
        }
        // desencriptar con Rijndael
        string cadenaFinal;
        Rijndael miRijndael = Rijndael.Create();
        miRijndael.Padding = PaddingMode.None;
        miRijndael.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = miRijndael.CreateDecryptor(key, IV);
        byte[] fromEncrypt = new byte[encrypted.Length];
        MemoryStream msDecrypt = new MemoryStream(encrypted);
        CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
        ASCIIEncoding textConverter = new ASCIIEncoding();
        cadenaFinal = textConverter.GetString(fromEncrypt);
        return cadenaFinal.Substring(0, cadenaFinal.IndexOf("*"));
    }

    public static DataTable getRRSS()
    {
        SqlConnection conexion = new SqlConnection(strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_rrssGet";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return datos;
    }

    public static DataTable getPaises()
    {
        SqlConnection conexion = new SqlConnection(strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_getListPaises";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return datos;
    }
    public static DataTable getRooms()
    {
        SqlConnection conexion = new SqlConnection(strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_getRoomsAdministrador";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@id_administrador", C_administradores.codigo);
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return datos;
    }

    public DataTable getListPaisesByAgrupacion ( string searchString, int agrupacion )
    {
        SqlConnection conexion = new SqlConnection(strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_getListPaisesByAgrupacion";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@searchString", searchString);
            comando.Parameters.AddWithValue("@agrupacion", agrupacion);
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return datos;
    }


    public static void getLimitesRelevancia(out int minRelevancia,out int maxRelevancia, out Int64 numPost)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();

        
        try
        {
            string query = "sp_limitesRelevancia";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;

            comando.Parameters.Add("@relevanciaMinValue", SqlDbType.Int);
            comando.Parameters["@relevanciaMinValue"].Direction = ParameterDirection.Output;
            comando.Parameters.Add("@relevanciaMaxValue", SqlDbType.Int);
            comando.Parameters["@relevanciaMaxValue"].Direction = ParameterDirection.Output;
            comando.Parameters.Add("@numPost", SqlDbType.BigInt);
            comando.Parameters["@numPost"].Direction = ParameterDirection.Output;
            conexion.Open();
            int consulta = comando.ExecuteNonQuery();
             minRelevancia = Convert.ToInt32(comando.Parameters["@relevanciaMinValue"].Value);
             maxRelevancia = Convert.ToInt32(comando.Parameters["@relevanciaMaxValue"].Value);
             numPost = Convert.ToInt64(comando.Parameters["@numPost"].Value);

        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
     
    }


    public static DataTable getAgrupacionesPaises (  )
    {
        SqlConnection conexion = new SqlConnection(strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_agrupacionesPaisesGet";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return datos;
    }
    //public int guardarError(string error)
    //{
    //    SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
    //    SqlCommand comando = new SqlCommand();
    //    int resultado = -10;
    //    try
    //    {
    //        string query = "sp_gestionRoom";
    //        comando.CommandText = query;
    //        comando.Connection = conexion;
    //        comando.CommandType = CommandType.StoredProcedure;
    //        comando.Parameters.AddWithValue("@nombre", nombreRoom);
    //        comando.Parameters.AddWithValue("@idAdministrador", C_administradores.codigo);
    //        comando.Parameters.AddWithValue("@codigoOperacion", 0);
    //        conexion.Open();
    //        resultado = comando.ExecuteNonQuery();
    //    }
    //    finally
    //    {
    //        conexion.Close();
    //        comando.Dispose();
    //    }

    //    return resultado;
    //}


}