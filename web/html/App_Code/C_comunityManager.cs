using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de C_comunityManager
/// </summary>
public class C_comunityManager
{
    public int id { get; set; }
        public int codigoRS { get; set; }
    public string codigoCM { get; set; }
    public string nombre { get; set; }
    public string nickname { get; set; }
    public byte activo { get; set; }
    public string numAmigos { get; set; }
    public string localizacion { get; set; }
    public byte asignado { get; set; }
    public string  ultimoPostTratado { get; set; }
    public byte borrado { get; set; }
    public int idLocalizacionBorrar { get; set; }
    public string contacto { get; set; }

    public C_comunityManager ()
    {
        //
        // TODO: Agregar aquí la lógica del constructor
        //
    }
    
    public DataTable getCM (string codigoCM, int codigoRS, bool? activo, string pais, string cadenaBusqueda,int id_room,bool isHashtag)
    {
        
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_cmGet";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            comando.Parameters.AddWithValue("@codigoCM", codigoCM);
            comando.Parameters.AddWithValue("@activo", activo);
            comando.Parameters.AddWithValue("@pais", pais);
            comando.Parameters.AddWithValue("@cadenaBusqueda", cadenaBusqueda);
            comando.Parameters.AddWithValue("@id_room", id_room);
            comando.Parameters.AddWithValue("@isHashtag", isHashtag);
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
    
        return datos;
    }

    public DataTable getCMByRoom(int idRoom)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_cmGetCMByRoom";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            comando.Parameters.AddWithValue("@id_room", idRoom);
        
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        //
        return datos;
    }


    public string dameTarea ( int codigoRS )
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
       
           string retorno="";
        try
        {
            string query = "sp_cmDameTarea";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
          //SqlDataAdapter da = new SqlDataAdapter(comando);

            comando.Parameters.Add("@codCM", SqlDbType.NVarChar,50);
            comando.Parameters["@codCM"].Direction = ParameterDirection.Output;
            comando.Parameters.Add("@ultimoPost", SqlDbType.NVarChar,100);
            comando.Parameters["@ultimoPost"].Direction = ParameterDirection.Output;
            comando.Parameters.Add("@isHashtag", SqlDbType.Bit);
            comando.Parameters["@isHashtag"].Direction = ParameterDirection.Output;
            comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            conexion.Open();
            int consulta=comando.ExecuteNonQuery();
            string codCM = comando.Parameters["@codCM"].Value.ToString();
            if (codCM != "0")
            {
                
                string ultimoPost = comando.Parameters["@ultimoPost"].Value.ToString();
               
                string isHashtag = comando.Parameters["@isHashtag"].Value.ToString();
                retorno = codCM + "###" + ultimoPost.ToString()+"###"+isHashtag;
            }
           
        
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return retorno;
    }

    public int deleteCM ( string codigoCM, int id_room )
    {
        int result = 0;
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        try
        {

            string query = "sp_cmDelete";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            //comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            comando.Parameters.AddWithValue("@codigoCM", codigoCM);
            comando.Parameters.AddWithValue("@id_room", id_room);

            comando.Connection.Open();
            result = Convert.ToInt32(comando.ExecuteScalar());

        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return result;

    }

    public int guardarCM (int codigoRS, string codigoCM, string nombre, string nickname,/* bool activo,*/ int numAmigos,string agrupacion, string localizacion, bool asignado,string idLocalizacion, string contacto,int id_room,bool hashtag)
    {
        
        if (idLocalizacion.Trim()=="") {
            idLocalizacion="0";
        }
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
         SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {
            string query = "sp_cmSave_v2";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;

            
            comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            comando.Parameters.AddWithValue("@codigoCM", codigoCM);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@nickname", nickname);
            //comando.Parameters.AddWithValue("@activo", activo);
            comando.Parameters.AddWithValue("@numAmigos", numAmigos);
            comando.Parameters.AddWithValue("@idAgrupacionPais", Convert.ToInt32(agrupacion));
            comando.Parameters.AddWithValue("@idLocalizacion", Convert.ToInt32(idLocalizacion));
            comando.Parameters.AddWithValue("@localizacion", localizacion);
            comando.Parameters.AddWithValue("@asignado", asignado);
            comando.Parameters.AddWithValue("@contacto", contacto);
            comando.Parameters.AddWithValue("@id_room", id_room);
            comando.Parameters.AddWithValue("@hashtag", hashtag);
            //comando.Parameters.Add("@retorno", SqlDbType.Int);
            //comando.Parameters["@retorno"].Direction = ParameterDirection.Output;

            conexion.Open();
            resultado=comando.ExecuteNonQuery();
            // resultado = Convert.ToInt32(comando.Parameters["@retorno"].Value);
            // resultado = comando.Parameters["@retorno"].Value;


        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }

    public DataTable getExcel(int codigoRS, string pais, string cadenaBusqueda,int id_room)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_cmGetExcel";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(comando);
            comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            comando.Parameters.AddWithValue("@pais", pais);
            comando.Parameters.AddWithValue("@cadenaBusqueda", cadenaBusqueda);
            comando.Parameters.AddWithValue("@id_room", id_room);
            da.Fill(datos);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        //
        return datos;
    }

    public int cerrarTarea ( int codigoRS, string codigoCM, int motivoCierre )
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();

        int retorno=0;
        try
        {
            string query = "sp_cmCerrarTarea";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            //  SqlDataAdapter da = new SqlDataAdapter(comando);

           
            comando.Parameters.AddWithValue("@codigoRS", codigoRS);
            comando.Parameters.AddWithValue("@codigoCM", codigoCM);
            comando.Parameters.AddWithValue("@motivoCierre", motivoCierre);
            conexion.Open();
            retorno=comando.ExecuteNonQuery();
            


        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }
        return retorno;
    }

}