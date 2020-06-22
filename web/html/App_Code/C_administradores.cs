using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de C_administradores
/// </summary>
public class C_administradores
{

    public static int codigo
    {
        //Forma de declarar getters y setters 
        get
        {
            if (HttpContext.Current.Request.Cookies["socialMedia"] == null || HttpContext.Current.Request.Cookies["socialMedia"]["codigo"] == null)
            {
                //return 1000;
                return 0;
            }
            else
            {
                //return 1000;
                return Convert.ToInt32(C_FuncionesGenerales.DesencriptarCodigos(HttpContext.Current.Request.Cookies["socialMedia"]["codigo"].ToString()));

            }
        }

        set
        {
            HttpContext.Current.Response.Cookies["socialMedia"]["codigo"] = C_FuncionesGenerales.EncriptarCodigos(value.ToString());


        }
    }

    public static string nombre
    {
        get
        {
            if (HttpContext.Current.Request.Cookies["socialMedia"] == null || HttpContext.Current.Request.Cookies["socialMedia"]["nombre"] == null)
            {
                //return 1000;
                return "";
            }
            else
            {
                //return 1000;
                return HttpContext.Current.Request.Cookies["socialMedia"]["nombre"].ToString();

            }
        }

        set
        {
            HttpContext.Current.Response.Cookies["socialMedia"]["nombre"] = value.ToString();


        }
    }


	public C_administradores()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    public DataTable getAdministradores(string cadenaBusqueda)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_administradoresGet";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@cadenaBusqueda", cadenaBusqueda);
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
    public int adminstradoresSave(string usuario, string password, string nombre,string permisos)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        permisos = permisos + "#";
        int resultado = -10;
        try
        {
            string query = "sp_administradoresSave";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@password", password);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@procesos", permisos);
            comando.Parameters.Add("@comprobacion", SqlDbType.Int);
            comando.Parameters["@comprobacion"].Direction = ParameterDirection.InputOutput;

            //SqlDataAdapter da = new SqlDataAdapter(comando);
            conexion.Open();
            comando.ExecuteNonQuery();
            resultado = Convert.ToInt32(comando.Parameters["@comprobacion"].Value);
            
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }

    public int adminstradoresUpdate(int codigo,string usuario, string password, string nombre, string permisos)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        permisos = permisos + "#";
        SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {
            string query = "sp_administradoresSave";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@codigo", codigo);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@password", password);
            comando.Parameters.AddWithValue("@nombre", nombre);
            comando.Parameters.AddWithValue("@procesos", permisos);
            comando.Parameters.Add("@comprobacion", SqlDbType.Int);
            comando.Parameters["@comprobacion"].Direction = ParameterDirection.InputOutput;

            //SqlDataAdapter da = new SqlDataAdapter(comando);
            conexion.Open();
            comando.ExecuteNonQuery();
            resultado = Convert.ToInt32(comando.Parameters["@comprobacion"].Value);
        }
        finally
        {
            conexion.Close();
            comando.Dispose();
        }

        return resultado;
    }
    public int adminstradoresDelete(int codigo)
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        int resultado = -10;
        try
        {
            string query = "sp_administradoresDelete";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@codAdmin", codigo);
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


    public DataTable login ( string login, string password )
    {
        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        DataTable datos = new DataTable();
        try
        {
            string query = "sp_login";
            comando.CommandText = query;
            comando.Connection = conexion;
            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@login", login);
            comando.Parameters.AddWithValue("@password", password);
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

    public static void desconectar ( bool redirigirLogin )
    {
        HttpContext.Current.Response.Cookies["socialMedia"]["codigo"] ="0";
        HttpContext.Current.Response.Cookies["socialMedia"].Expires = DateTime.Now.AddDays(-1);
        if (redirigirLogin)
        {
            HttpContext.Current.Response.Redirect("~/login.aspx");
        }
    }




    public bool tengoPermiso(int? procesoRequerido)
    {
        bool retorno1 = false;
        DataTable result = loginById(C_administradores.codigo);
        string proceso = "#" + procesoRequerido + "#";
        foreach (DataRow fila in result.Rows)
        {

            if (fila["procesosPermitidos"].ToString().Contains(proceso))
            {
                retorno1 = true;
            }

        }
    
    
        if (retorno1 == true )
        {
            return true;
        }
        else
        {
            return false;

        }
    }

    public static string procesosPermitidos()
    {
        //devolver los procesos permitidos para este usuario

        C_administradores admin = new C_administradores();
        DataTable procesos = admin.loginById(C_administradores.codigo);
        return procesos.Rows[0]["procesosPermitidos"].ToString();
    }


    public DataTable loginById(int codigoAdministrador)
    {

        SqlConnection conexion = new SqlConnection(C_FuncionesGenerales.strConexion);
        SqlCommand comando = new SqlCommand();
        string query = "SELECT * FROM administradores WHERE codigo=@codigo";
        comando.CommandText = query;
        comando.Connection = conexion;
        comando.Parameters.AddWithValue("@codigo", codigoAdministrador);
        SqlDataAdapter da = new SqlDataAdapter(comando);
        DataTable datos = new DataTable();
        da.Fill(datos);
        return datos;
    }
}