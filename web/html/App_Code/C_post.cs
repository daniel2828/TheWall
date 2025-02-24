﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de C_posts
/// </summary>
public class C_post
{
    public int codigoRS { get; set; }
    public string codigoCM { get; set; }
    public string codigoPost { get; set; }
    public string fecha { get; set; }
    public string texto { get; set; }
    public string multimedia { get; set; }
    public string urlDirecta { get; set; }
    public string relevancia { get; set; }
    public string sharer { get; set; }
    public string likes { get; set; }
    public string comentarios { get; set; }
    public int amigosCM { get; set; }
    public string ultimaActualizacion { get; set; }
    public string localizacion { get; set; }
    public string nombreCM { get; set; }
    public string nicknameCM { get; set; }
    public string imagenCM { get; set; }
    public string tipoPost { get; set; }

    public bool favorito { get; set; }
    
    public string eficaciaRelevancia { get; set; }



	public C_post()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}



    public DataTable getPosts ( int? codigoRS ,int? codigoCM ,int? codigoPost ,DateTime? fechaInicio ,DateTime? fechaFin ,string localizacion ,int pagina, int registrosporPagina)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_postsGet";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            comando.Parameters.AddWithValue("@codigoCM", codigoCM);
            comando.Parameters.AddWithValue("@codigoPost", codigoPost);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            comando.Parameters.AddWithValue("@localizacion", localizacion);
            comando.Parameters.AddWithValue("@pagina", pagina);
            comando.Parameters.AddWithValue("@RegistrosporPagina", registrosporPagina);
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

    public DataTable getPosts_v2 ( int fb, int tw, int instagram, int yt, DateTime? fechaInicio, DateTime? fechaFin, DataTable localizacion,int room, DataTable cuentas,int criterioOrden, bool ascendente, bool favorito, int pagina, int registrosporPagina )   
    {
        int favoritoN = 0;
        if(favorito == false)
        {
            favoritoN = 0;
        }
        else
        {
            favoritoN = 1;
        }
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_postsGet_v2";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@fb", fb);
            comando.Parameters.AddWithValue("@tw", tw);
            comando.Parameters.AddWithValue("@yt", yt);
            comando.Parameters.AddWithValue("@instagram", instagram);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            comando.Parameters.AddWithValue("@localizacion", localizacion);
            comando.Parameters.AddWithValue("@cuentas", cuentas);
            comando.Parameters.AddWithValue("@criterioOrden", criterioOrden);
            comando.Parameters.AddWithValue("@ascendente", ascendente);
            comando.Parameters.AddWithValue("@favorito", favoritoN);
            comando.Parameters.AddWithValue("@idAdministrador", C_administradores.codigo);
            comando.Parameters.AddWithValue("@id_room", room);
            comando.Parameters.AddWithValue("@pagina", pagina);
            comando.Parameters.AddWithValue("@RegistrosporPagina", registrosporPagina);
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

   // public int guardarPost ( Int64 codigoRS, string codigoCM, string codigoPost, DateTime fecha, string texto, string multimedia, string urlDirecta, Int64 sharer, Int64 likes, Int64 comentarios, Int64 amigosCM, string tipoPost, string nextPageToken )
    public int guardarPost ( DataTable posts )
       
{
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {



            string query = "sp_postsSave_v2";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@posts", posts);
          
           // comando.Parameters.AddWithValue("@codigoRS", codigoRS);
           // comando.Parameters.AddWithValue("@codigoCM", codigoCM);
           // comando.Parameters.AddWithValue("@codigoPost", codigoPost);
           // comando.Parameters.AddWithValue("@fecha", fecha);
           // comando.Parameters.AddWithValue("@texto", texto);
           // comando.Parameters.AddWithValue("@multimedia", multimedia);
           // comando.Parameters.AddWithValue("@urlDirecta", urlDirecta);
           //comando.Parameters.AddWithValue("@sharer", sharer);
           // comando.Parameters.AddWithValue("@likes", likes);
           // comando.Parameters.AddWithValue("@comentarios", comentarios);
           // comando.Parameters.AddWithValue("@amigosCM", amigosCM);
           // comando.Parameters.AddWithValue("@tipoPost", tipoPost);
           // comando.Parameters.AddWithValue("@nextPageToken", nextPageToken);
       
            conexion.Open();
            resultado= comando.ExecuteNonQuery();
       

        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }


    public int guardarFavorito ( string codigoPost, int codigoRS, string codigoCM, bool favorito )
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {



            string query = "sp_postsSaveFavourite";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            comando.Parameters.AddWithValue("@codigoCM", codigoCM);
            comando.Parameters.AddWithValue("@codigoPost", codigoPost);
            comando.Parameters.AddWithValue("@favorito", favorito);
            comando.Parameters.AddWithValue("@idAdministrador", C_administradores.codigo);
          

            conexion.Open();
            resultado= comando.ExecuteNonQuery();


        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }



    public DataTable postFavoritosGet (  )
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_postFavoritosGet";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@idAdministrador", C_administradores.codigo);
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

 




}