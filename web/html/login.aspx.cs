using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btLogin_Click ( object sender, EventArgs e )
    {
        try
        {
            C_administradores proxy = new C_administradores();
            DataTable datos=proxy.login(email.Value, password.Value);
            if (datos.Rows.Count>0) {
                C_administradores.codigo = Convert.ToInt32(datos.Rows[0]["codigo"]);
                C_administradores.nombre = datos.Rows[0]["nombre"].ToString();
                Response.Redirect("~/Default.aspx", false);
            
            }
        }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
            throw new Exception("Error de conexión. Cód Error:" + ex.Message);

        }
    }
}