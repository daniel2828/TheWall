using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
public partial class comunityManagers : System.Web.UI.Page
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
                bindRooms();
                bindRS();
                bindPais();
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
    private void bindRooms()
    {
        
        DataTable tbRooms = C_FuncionesGenerales.getRooms();
        dpRooms.DataSource = tbRooms;
        dpRooms.DataValueField= "id_room";
        ViewState["id_room_home"] = tbRooms.Rows[0]["id_room"];
        dpRooms.DataTextField = "nombre";
        dpRooms.DataBind();
    }

    protected void bindDatos()
    {

        try
        {

            bool isHashtag;
            string hasOrCm = dpHashtagOrCm.SelectedItem.Value.ToString();
            if (hasOrCm.Equals("Hashtag"))
            {
                isHashtag = true;
            }
            else
            {
                isHashtag = false;
            }
            C_comunityManager proxy = new C_comunityManager();
          
            int id_room = (Convert.ToInt32(dpRooms.SelectedItem.Value.ToString()));
            if (id_room == Convert.ToInt32(ViewState["id_room_home"]))
            {
                buttonModalModificar.Visible = false;
                modalBorrarRoom.Visible = false;
            }else
            {
                buttonModalModificar.Visible = true;
                modalBorrarRoom.Visible = true;
            }
            DataTable datos;
            if (searchBt.Value.Equals(""))
            {
                 datos = proxy.getCM(null, Convert.ToInt32(dpSearchRS.SelectedValue), null, dpSearchPais.SelectedValue,"", id_room, isHashtag);
            }else
            {
                datos = proxy.getCM(searchBt.Value, Convert.ToInt32(dpSearchRS.SelectedValue), null, dpSearchPais.SelectedValue, searchBt.Value, id_room, isHashtag);
            }


          
            // Si los datos devueltos son con hashtag 
            if (isHashtag)
            {
                gvPost.Visible = false;
                GridViewHash.Visible = true;
                dpSearchPais.Visible = false;
                dpSearchRS.Items.Remove(dpSearchRS.Items.FindByValue("1"));
                if (datos.Rows.Count == 0)
                {
                    DataRow filaAux = datos.NewRow();
                    filaAux["codigoCM"] = "";
                    filaAux["codigoRS"] = 0;
                 
                    
                    datos.Rows.Add(filaAux);
                    verBtn = false;
                }
                //
                tbTotales.Text = datos.Rows.Count.ToString();
                GridViewHash.DataSource = datos;
                GridViewHash.DataBind();
            }
            else
            {
                if (dpSearchRS.Items.FindByValue("1") == null)
                {
                    ListItem fbItem = new ListItem("Facebook", "1");
                    dpSearchRS.Items.Insert(0, fbItem);
                }
               
                GridViewHash.Visible = false;
                dpSearchPais.Visible = true;
                gvPost.Visible = true;
                //Sino            
                if (datos.Rows.Count == 0)
                {
                    DataRow filaAux = datos.NewRow();
                    filaAux["codigoCM"] = "";
                    filaAux["codigoRS"] = 0;
                    filaAux["nombre"] = "";
                    filaAux["nickname"] = "";
                    filaAux["numAmigos"] = 0;
                    filaAux["localizacion"] = "";
                    filaAux["activo"] = 0;
                    filaAux["asignado"] = 0;
                    filaAux["contacto"] = "";
                    datos.Rows.Add(filaAux);
                    verBtn = false;
                }
                //
                tbTotales.Text = datos.Rows.Count.ToString();
                gvPost.DataSource = datos;
                gvPost.DataBind();
            }
          
              
            
        }
        catch (Exception e) {
            throw e;
        
        
        }
    }

    protected void bindRS()
    {
        dpSearchRS.DataSource = C_FuncionesGenerales.getRRSS();
        dpSearchRS.DataTextField = "descripcion";
        dpSearchRS.DataValueField = "codigo";
        dpSearchRS.DataBind();
        ListItem item = new ListItem("Elige una red social", "0");
        dpSearchRS.Items.Add(item);
        dpSearchRS.SelectedIndex = dpSearchRS.Items.Count - 1;
    }

    protected void bindPais()
    {
        dpSearchPais.DataSource = C_FuncionesGenerales.getPaises();
        dpSearchPais.DataTextField = "nombre";
        dpSearchPais.DataValueField = "codigo";
        dpSearchPais.DataBind();
        ListItem item = new ListItem("Elige un país", "");
        dpSearchPais.Items.Add(item);
        dpSearchPais.SelectedIndex = dpSearchPais.Items.Count - 1;
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
        C_comunityManager objCM = new C_comunityManager();
        string codigoCM;
        string codigoRS;
        string[] commandArgs;
        //
        switch (e.CommandName)
        {
            case "Guardar":
                //
                codigoCM = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("hidCM")).Text;
                codigoRS = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("hidRS")).Text;
                //string codigoCMNew = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("tbCM")).Text;
                //string codigoRSNew = ((DropDownList)gvPost.Rows[gvPost.EditIndex].FindControl("dpRS")).SelectedValue;
                string nombre = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("tbNombre")).Text;
                string nickname = ((Label)gvPost.Rows[gvPost.EditIndex].FindControl("tbNickName")).Text;
                string nAmigos = ((Label)gvPost.Rows[gvPost.EditIndex].FindControl("tbNAmigos")).Text;
                string agrupacion = ((DropDownList)gvPost.Rows[gvPost.EditIndex].FindControl("dpAgrupacion")).SelectedValue.ToString();
                string idpais = hidCodPais.Value;
                string pais = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("tbPais")).Text;
                // DUDA
                string contacto = ((TextBox)gvPost.Rows[gvPost.EditIndex].FindControl("tbContacto")).Text;
                //bool activo = ((CheckBox)gvPost.Rows[gvPost.EditIndex].FindControl("chkActivo")).Checked;
                bool asignado = ((CheckBox)gvPost.Rows[gvPost.EditIndex].FindControl("chkAsignado")).Checked;
                bool hashtag = false;
                int id_room = Convert.ToInt32(dpRooms.SelectedItem.Value.ToString());
                if (nAmigos.Trim()=="")
                {
                    nAmigos="0";
                }
                if (nombre.Trim()=="" ||nickname.Trim()=="" ||nAmigos.Trim()=="" ||agrupacion.Trim()=="" ||pais.Trim()=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "validacionEditar", "alert('Por favor, debes rellenar todos los campos');", true);

                }
                else
                {
                    int resultado = objCM.guardarCM(Convert.ToInt32(codigoRS), codigoCM, nombre, nickname, /*activo,*/ Convert.ToInt32(nAmigos), agrupacion, pais, false, idpais, contacto, id_room, false);
                    gvPost.EditIndex = -1;
                    bindDatos();
                }

                break;
            case "Borrar":
                //
                codigoCM = e.CommandArgument.ToString();
                int id_room_borrar = Convert.ToInt32(dpRooms.SelectedItem.Value.ToString());
                objCM.deleteCM(codigoCM, id_room_borrar);
                gvPost.EditIndex = -1;
                bindDatos();
                break;
            case "GuardarNuevo":
                //
                string CMNuevo = ((TextBox)gvPost.FooterRow.FindControl("tbNuevoCM")).Text;
                string nombreNuevo = ((TextBox)gvPost.FooterRow.FindControl("tbNuevoNombre")).Text;
                string nicknameNuevo = ((TextBox)gvPost.FooterRow.FindControl("tbNuevoNickName")).Text;
                string RSNuevo = ((DropDownList)gvPost.FooterRow.FindControl("dpNuevoRS")).SelectedValue.ToString();
                string nAmigosNuevo = ((TextBox)gvPost.FooterRow.FindControl("tbNuevoNAmigos")).Text;
                string agrupacionNuevo = ((DropDownList)gvPost.FooterRow.FindControl("dpNuevoAgrupacion")).SelectedValue.ToString();
                string idpaisNuevo = hidCodPais.Value;
                string paisNuevo = ((TextBox)gvPost.FooterRow.FindControl("tbNuevoPais")).Text;
                int id_room_nuevo = Convert.ToInt32(dpRooms.SelectedItem.Value.ToString());
               
                // DUDA
                string contactoNuevo = ((TextBox)gvPost.FooterRow.FindControl("tbNuevoContacto")).Text;
                //bool activoNuevo = ((CheckBox)gvPost.FooterRow.FindControl("chkNuevoActivo")).Checked;
                //bool asignadoNuevo = ((CheckBox)gvPost.FooterRow.FindControl("chkNuevoAsignado")).Checked;
                if (nAmigosNuevo.Trim()=="") {
                    nAmigosNuevo="0";
                }
                if (CMNuevo.Trim()=="" ||nombreNuevo.Trim()=="" ||nicknameNuevo.Trim()=="" ||RSNuevo.Trim()=="" ||nAmigosNuevo.Trim()=="" ||agrupacionNuevo.Trim()=="" ||paisNuevo.Trim()=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "validacionNuevo", "alert('Por favor, debes rellenar todos los campos');", true);

                }
                else
                {
                    int result = objCM.guardarCM(Convert.ToInt32(RSNuevo), CMNuevo, nombreNuevo, nicknameNuevo, Convert.ToInt32(nAmigosNuevo), agrupacionNuevo, paisNuevo, false, idpaisNuevo, contactoNuevo, id_room_nuevo,false);
                    gvPost.EditIndex = -1;
                    bindDatos();
                }
                break;
        }
    }

    protected void btnExcel_Click(object sender, EventArgs e)
    {
        C_comunityManager proxy = new C_comunityManager();
        DataTable datos = new DataTable();
        int id_room= Convert.ToInt32(dpRooms.SelectedItem.Value.ToString());
        datos = proxy.getExcel(Convert.ToInt32(dpSearchRS.SelectedValue), dpSearchPais.SelectedValue, searchBt.Value, id_room);

        string strTipoMIME = "application/ms-excel";

        Page.Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        this.EnableViewState = false;

        Page.Response.ClearHeaders();
        Page.Response.ClearContent();
        Page.Response.ContentType = strTipoMIME;
        Page.Response.AddHeader("Content-Disposition", "attachment;filename=listado_ComunnityManager.xls");
        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("iso-8859-3");
        HttpContext.Current.Response.Write("<Table border='1' > <TR style='background-color: #EA4141'>");
        //am getting my grid's column headers
        int columnscount = datos.Columns.Count;

        for (int j = 0; j < columnscount; j++)
        {   //write in new column
            HttpContext.Current.Response.Write("<Td>");
            //Get column headers  and make it as bold in excel columns
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(datos.Columns[j].ColumnName);
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");
        }
        HttpContext.Current.Response.Write("</TR>");
        int cont = 0;
        foreach (DataRow row in datos.Rows)
        {//write in new row
            if (cont % 2 == 0)
                HttpContext.Current.Response.Write("<TR>");
            else
                HttpContext.Current.Response.Write("<TR style='background:#CCC;'>");
            cont++;
            for (int i = 0; i < datos.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
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

    protected void gvPost_RowDataBound ( object sender, GridViewRowEventArgs e )
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                hidCodPais.Value = ((TextBox)e.Row.FindControl("hidPais")).Text;
                TextBox hidAgrupacion = (TextBox)e.Row.FindControl("hidAgrupacion");
                DropDownList dpAgrupacion = (DropDownList)e.Row.FindControl("dpAgrupacion");
                // bind DropDown manually
                dpAgrupacion.DataSource = C_FuncionesGenerales.getAgrupacionesPaises();
                dpAgrupacion.DataTextField = "agrupacion";
                dpAgrupacion.DataValueField = "codigo";
                dpAgrupacion.DataBind();
                dpAgrupacion.SelectedValue = hidAgrupacion.Text; // you can use e.Row.DataItem to get the value
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {

            DropDownList dpAgrupacion = (DropDownList)e.Row.FindControl("dpNuevoAgrupacion");
            // bind DropDown manually
            dpAgrupacion.DataSource = C_FuncionesGenerales.getAgrupacionesPaises();
            dpAgrupacion.DataTextField = "agrupacion";
            dpAgrupacion.DataValueField = "codigo";
            dpAgrupacion.DataBind();
        
        
        
        }

    }
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

    protected void linkGuardarRoom_Click(object sender, EventArgs e)
    {
        
        C_rooms proxy = new C_rooms();
        proxy.guardarRoomAdministrador(dpNombreRoom.Value);
        bindRooms();
        bindDatos();

    }

    protected void linkButtonBorrarRoom_Click(object sender, EventArgs e)
    {
        C_rooms proxy = new C_rooms();
        int id_room = (Convert.ToInt32(dpRooms.SelectedItem.Value.ToString()));
        proxy.borrarRoomAdministrador(id_room);
        bindRooms();
        bindDatos();

    }

    protected void LinkBotonModifica_Click(object sender, EventArgs e)
    {
        C_rooms proxy = new C_rooms();
        int id_room = (Convert.ToInt32(dpRooms.SelectedItem.Value.ToString()));
       proxy.modificarRoomAdministrador(id_room,dpTextoModificarRoom.Value.ToString());
        bindRooms();
        bindDatos();

       // ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "closeModal", "$('._closeModal').click();", true);

    }



    //protected void hashtagEdit_CheckedChanged(object sender, EventArgs e)
    //{
    //    GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
    //    int index = row.RowIndex;
    //    CheckBox checkHash = (CheckBox)gvPost.Rows[index].FindControl("hashtagEdit");
    //    TextBox textoNombre = (TextBox)gvPost.Rows[index].FindControl("tbNombre");
    //    DropDownList dropAgrupacion = (DropDownList)gvPost.Rows[index].FindControl("dpAgrupacion");
    //    TextBox textoPais = (TextBox)gvPost.Rows[index].FindControl("tbPais");
    //    TextBox textoContacto = (TextBox)gvPost.Rows[index].FindControl("tbContacto");
    //    Label numAmigos = (Label)gvPost.Rows[index].FindControl("tbNickName");
    //    Label nickname = (Label)gvPost.Rows[index].FindControl("tbNAmigos");
    //    if (checkHash.Checked == true)
    //    {
    //        dropAgrupacion.SelectedValue = null;
    //        textoPais.Text = "";
    //        textoContacto.Text = "";
    //        numAmigos.Text = "";
    //        nickname.Text = "";
    //        dropAgrupacion.Enabled = false;
    //        textoPais.Enabled = false;
    //        textoContacto.Enabled = false;


    //    }else
    //    {
    //        dropAgrupacion.Enabled = true;
    //        textoPais.Enabled = true;
    //        textoContacto.Enabled = true;


    //    }
    //    //string yourvalue = cb1.Text;
    //    //CheckBox check1;
    //    //check1 = (CheckBox)e.Row.FindControl("permiso1Edit");
    //}

    protected void GridViewHash_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewHash.EditIndex = -1;
        GridViewHash.PageIndex = e.NewPageIndex;
        bindDatos();
    }

    protected void GridViewHash_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewHash.EditIndex = e.NewEditIndex;
        bindDatos();
    }

    protected void GridViewHash_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridViewHash.EditIndex = -1;
        bindDatos();
    }
    //"http://localhost:63029/ws.svc/",
    protected void GridViewHash_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        C_comunityManager objCM = new C_comunityManager();
        string codigoCM;
        string codigoRS;
        string[] commandArgs;
        //
        switch (e.CommandName)
        {
            case "GuardarHash":
                //
                //string CMNuevo = ((TextBox)GridViewHash.FooterRow.FindControl("tbHashIdNuevo")).Text;
                //// public int guardarCM(int codigoRS, string codigoCM, string nombre, string nickname,/* bool activo,*/ int numAmigos, string agrupacion, string localizacion, bool asignado, string idLocalizacion, string contacto, int id_room, bool hashtag)
                //string RSNuevo = ((DropDownList)GridViewHash.FooterRow.FindControl("dpRsHashNew")).SelectedValue.ToString();
                //int id_room_nuevo = Convert.ToInt32(dpRooms.SelectedItem.Value.ToString());
                //int result = objCM.guardarCM(Convert.ToInt32(RSNuevo), CMNuevo, "", "", 0, null, "", true, "", "", id_room_nuevo, true);
                //gvPost.EditIndex = -1;
                //bindDatos();
                //int resultado = objCM.guardarCM(Convert.ToInt32(codigoRS), codigoCM, nombre, nickname, /*activo,*/ Convert.ToInt32(nAmigos), agrupacion, pais, asignado, idpais, contacto, id_room, hashtag);
                //gvPost.EditIndex = -1;
                //bindDatos();


                break;
            case "BorrarHash":
                codigoCM = e.CommandArgument.ToString();
                int id_room_borrar = Convert.ToInt32(dpRooms.SelectedItem.Value.ToString());
                objCM.deleteCM(codigoCM, id_room_borrar);
                gvPost.EditIndex = -1;
                bindDatos();
                break;
            case "GuardarNuevoHash":
                //
                string CMNuevo = ((TextBox)GridViewHash.FooterRow.FindControl("tbHashIdNuevo")).Text;
             // public int guardarCM(int codigoRS, string codigoCM, string nombre, string nickname,/* bool activo,*/ int numAmigos, string agrupacion, string localizacion, bool asignado, string idLocalizacion, string contacto, int id_room, bool hashtag)
                string RSNuevo = ((DropDownList)GridViewHash.FooterRow.FindControl("dpRsHashNew")).SelectedValue.ToString();
                int id_room_nuevo = Convert.ToInt32(dpRooms.SelectedItem.Value.ToString());
                string nickname = "#" + CMNuevo;
               
                int result = objCM.guardarCM(Convert.ToInt32(RSNuevo), CMNuevo, "", nickname, 0, null,"", false, "","", id_room_nuevo,true);
                    gvPost.EditIndex = -1;
                    bindDatos();
                break;
        }
    }

    protected void GridViewHash_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void dpHashtagOrCm_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        bindDatos();
    }
}