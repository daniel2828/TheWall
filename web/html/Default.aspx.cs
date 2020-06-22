using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public string nombreUsuario="";

    //protected void Page_Init ( object sender, EventArgs e )
    //{
    //    if (C_administradores.codigo <= 0)
    //    {
    //        C_administradores.desconectar(true);
    //    }


    //}
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!Page.IsPostBack)
        {
            if (C_administradores.codigo <= 0)
            {
                C_administradores.desconectar(true);
                Response.Redirect("~/login.aspx", false);
            }
            //bindCuentas();
            bindPaises();
            bindLimitesRelevancia();
            bindRooms();
        }
         nombreUsuario = C_administradores.nombre;
      
        C_administradores proxyAdmin = new C_administradores();
        if (!proxyAdmin.tengoPermiso(2))
        {
            C_administradores.desconectar(true);

        }
        if (!proxyAdmin.tengoPermiso(1))
        {
            phGotoManagers.Visible = false;
        }
        else
        {
            phGotoManagers.Visible = true;
        }
    }


    private void bindPaises()
    {
      DataTable tbPaises=C_FuncionesGenerales.getPaises();
   
      string jsonRertorno= DataTableToJSON(tbPaises);
      ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "bindPaises","var bindPaises= "+jsonRertorno+";", true);

    }
 
    private void bindRooms()
    {
        DataTable tbRooms = C_FuncionesGenerales.getRooms();
        // Tengo que obtener los datos de la room dependiendo del administrador , por lo que tengo que llamar al objeto y al WS correspondiente  
       string jsonRetorno = DataTableToJSON(tbRooms);//DataTableToJSON(datos);
       ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "bindRooms", "var bindRooms=" + jsonRetorno + ";", true);

    }

    private void bindLimitesRelevancia()
    {
        int limiteInf;
        int limiteMax;
        long numPost;
        C_FuncionesGenerales.getLimitesRelevancia(out limiteInf, out limiteMax, out numPost);
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(),"limitesRelevancia"," var numPost="+numPost.ToString()+"; var relevanciaMin="+limiteInf.ToString()+"; var relevanciaMax="+limiteMax.ToString()+";",true);

    }
    protected void btDesconectar_Click ( object sender, EventArgs e )
    {
        try
        {
            C_administradores.desconectar(true);
            Response.Redirect("~/login.aspx", false);

        }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
        //    throw new Exception("Error de conexión. Cód Error:" + ex.Message);

        }
    }

    public  string DataTableToJSON ( DataTable table )
    {
        var list = new List<Dictionary<string, object>>();

        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>();

            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col];
            }
            list.Add(dict);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(list);
    }



   
}