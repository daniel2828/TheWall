using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
public partial class usuarios : System.Web.UI.Page
{
    public bool verBtn = true;
    public string nombreUsuario="";
    DataTable tbTiposRS;

    protected void Page_Init(object sender, EventArgs e)
    {
       tbTiposRS = C_FuncionesGenerales.getRRSS();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        nombreUsuario= C_administradores.nombre;
        //
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        //
        if (!Page.IsPostBack)
        {
            if (C_administradores.codigo <= 0)
            {
                C_administradores.desconectar(true);
                Response.Redirect("~/login.aspx", false);
            }
            else
            {
              
                bindDatos();
             
            }
        }

        C_administradores proxyAdmin = new C_administradores();
        if (!proxyAdmin.tengoPermiso(1))
        {
            C_administradores.desconectar(true);           
        }
        if (!proxyAdmin.tengoPermiso(2))
        {
            phGotoMuro.Visible = false;
        }
        else {
            phGotoMuro.Visible = true;
        }
    }


    protected void bindDatos()
    {
     
        try
        {
            C_administradores proxy = new C_administradores();
             DataTable datos = proxy.getAdministradores(searchBt.Value);
            if (datos.Rows.Count == 0)
            {
                DataRow filaAux = datos.NewRow();
                filaAux["user"] = "";
                filaAux["password"] = 0;
                filaAux["name"] = "";
                filaAux["procesosPermitidos"] = "";
                datos.Rows.Add(filaAux);
                verBtn = false;
            }

            tbTotales.Text = datos.Rows.Count.ToString();
            gvPost.DataSource = datos;
            gvPost.DataBind();
        }
        catch (Exception e) {
            throw e;
        }
    }

    protected void dpTipoRS_Load(object sender, EventArgs e)
    {
        DropDownList ctrlNRS = (DropDownList)sender;
        ctrlNRS.DataSource = tbTiposRS;
        ctrlNRS.DataTextField = "descripcion";
        ctrlNRS.DataValueField = "codigo";
        ctrlNRS.DataBind();
    }

    protected void gvPost_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvPost.EditIndex = -1;
        gvPost.PageIndex = e.NewPageIndex;
        bindDatos();
    }

    protected void gvPost_RowEditing(object sender, GridViewEditEventArgs e)
    {

        gvPost.EditIndex = e.NewEditIndex;
        bindDatos();
    }

    protected void gvPost_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvPost.EditIndex = -1;
        bindDatos();
    }

    protected void gvPost_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        C_administradores objAdministradores = new C_administradores();

        string[] commandArgs;
        //
        switch (e.CommandName)
        {
            case "Guardar":

                //int id_administrador_modificado = Convert.ToInt32(((TextBox)gvPost.FooterRow.FindControl("codigo")).Text);
                int id_administrador_modificado = Convert.ToInt32(e.CommandArgument.ToString());
                string user = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("tbUser")).Text;
                string password = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("tbPassword")).Text;
                string nombre = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("tbNombre")).Text;
                Boolean check1 = ((CheckBox)gvPost.Rows[gvPost.EditIndex].FindControl("permiso1Edit")).Checked;
                Boolean check2 = ((CheckBox)gvPost.Rows[gvPost.EditIndex].FindControl("permiso2Edit")).Checked;
                Boolean check3 = ((CheckBox)gvPost.Rows[gvPost.EditIndex].FindControl("permiso3Edit")).Checked;
                string permisos = "";
                if (check1)
                {
                    permisos += "#1";
                }
                if (check2)
                {
                    permisos += "#2";
                }
                if (check3)
                {
                    permisos += "#3";
                }

       
                int resultadoNuevo = objAdministradores.adminstradoresUpdate(id_administrador_modificado, user, password, nombre,permisos);
                if (resultadoNuevo == 10)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "alertMessage", @"alert('This login previously exists in the database. Introduce a new one.')", true);
                }

                //int resultado = objCM.guardarCM(Convert.ToInt32(codigoRS), codigoCM, nombre, nickname, /*activo,*/ Convert.ToInt32(nAmigos), agrupacion, pais, asignado, idpais, contacto);
                gvPost.EditIndex = -1;
                bindDatos();
                //}

                break;
            case "Borrar":
             
                int id_administrador_borrado= Convert.ToInt32(e.CommandArgument.ToString());
                objAdministradores.adminstradoresDelete(id_administrador_borrado);
                gvPost.EditIndex = -1;
                bindDatos();
                break;
            case "GuardarNuevo":
                string nuevoUsuario=((TextBox)gvPost.FooterRow.FindControl("tbNuevoUser")).Text;
                string nuevoPassword=((TextBox)gvPost.FooterRow.FindControl("tbNuevoPassword")).Text;
                string nuevoNombre=((TextBox)gvPost.FooterRow.FindControl("tbNuevoNombre")).Text;
                Boolean checkNuevo1 = ((CheckBox)gvPost.FooterRow.FindControl("permiso1Nuevo")).Checked;
                Boolean check2Nuevo = ((CheckBox)gvPost.FooterRow.FindControl("permiso2Nuevo")).Checked;
                Boolean check3Nuevo = ((CheckBox)gvPost.FooterRow.FindControl("permiso3Nuevo")).Checked;
                string permisosNuevo = "";
                if (checkNuevo1)
                {
                    permisosNuevo += "#1";
                }
                if (check2Nuevo)
                {
                    permisosNuevo += "#2";
                }
                if (check3Nuevo)
                {
                    permisosNuevo += "#3";
                }
                //// DUDA
                int resultado = objAdministradores.adminstradoresSave( nuevoUsuario, nuevoPassword, nuevoNombre, permisosNuevo);
                //Si el resultado es 10 quiere decir que el loguin ya existe, avisaremos al usuario de que no se pueden meter usuarios repetidos


                if (resultado == 10){
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),"alertMessage", @"alert('This login previously exists in the database. Introduce a new one.')", true);
                }
               
                gvPost.EditIndex = -1;
                bindDatos();
                break;
        }
    }


    protected string rs(object rrss)
    {
        string rs = "";
        switch (rrss.ToString())
        {
            case "1":
                rs = "Facebook";
                break;
            case "2":
                rs = "Twitter";
                break;
            case "3":
                rs = "Instagram";
                break;
            case "4":
                rs = "YouTube";
                break;
            default:
                rs = "";
                break;
        }
        return rs;
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

    protected void lnkSearch_Click(object sender, EventArgs e)
    {
        bindDatos();
    }
    protected void gvPost_RowDataBound(object sender, GridViewRowEventArgs e)
   {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox check1;
            CheckBox check2;
            CheckBox check3;
            if (((e.Row.RowState & DataControlRowState.Edit) > 0)){
                 check1 = (CheckBox)e.Row.FindControl("permiso1Edit");
                 check2 = (CheckBox)e.Row.FindControl("permiso2Edit");
                 check3 = (CheckBox)e.Row.FindControl("permiso3Edit");
            }
            else
            {
                 check1 = (CheckBox)e.Row.FindControl("permiso1");
                 check2 = (CheckBox)e.Row.FindControl("permiso2");
                 check3 = (CheckBox)e.Row.FindControl("permiso3");
              

            }

            string procesos = ((HiddenField)e.Row.FindControl("hdProcesos")).Value;
            string[] procesosArray = procesos.Split('#');
            for (int i = 0; i < procesosArray.Length; i++)
            {
                if (procesosArray[i].Equals("1"))
                {
                    check1.Checked = true;
                }
                else if (procesosArray[i].Equals("2"))
                {
                    check2.Checked = true;
                }
                else if (procesosArray[i].Equals("3"))
                {
                    check3.Checked = true;
                }
            }

        }

        

    }

    //protected void gvPost_RowDataBound ( object sender, GridViewRowEventArgs e )
    //{
    //                    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        if ((e.Row.RowState & DataControlRowState.Edit) > 0)
    //        {
    //            hidCodPais.Value = ((TextBox)e.Row.FindControl("hidPais")).Text;
    //            TextBox hidAgrupacion = (TextBox)e.Row.FindControl("hidAgrupacion");
    //            DropDownList dpAgrupacion = (DropDownList)e.Row.FindControl("dpAgrupacion");
    //            // bind DropDown manually
    //            dpAgrupacion.DataSource = C_FuncionesGenerales.getAgrupacionesPaises();
    //            dpAgrupacion.DataTextField = "agrupacion";
    //            dpAgrupacion.DataValueField = "codigo";
    //            dpAgrupacion.DataBind();
    //            dpAgrupacion.SelectedValue = hidAgrupacion.Text; // you can use e.Row.DataItem to get the value
    //        }
    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {

    //        DropDownList dpAgrupacion = (DropDownList)e.Row.FindControl("dpNuevoAgrupacion");
    //        // bind DropDown manually
    //        dpAgrupacion.DataSource = C_FuncionesGenerales.getAgrupacionesPaises();
    //        dpAgrupacion.DataTextField = "agrupacion";
    //        dpAgrupacion.DataValueField = "codigo";
    //        dpAgrupacion.DataBind();



    //    }

    //}
    public string DataTableToJSON(DataTable table)
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

    protected void dpRooms_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindDatos();
    }

}