<%@ Page Language="C#" AutoEventWireup="true" CodeFile="comunityManagers.aspx.cs" Inherits="comunityManagers" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>BrandWall</title>

    <!-- Bootstrap -->
    <link href="css/bootstrap.css" rel="stylesheet" />
    <link href="css/admin.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700|Open+Sans:400,300,700,600' rel='stylesheet' type='text/css' />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!-- [if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
          <![endif]-->

    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <nav class="navbar navbar-default">
            <div class="container-fluid">
                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1" aria-expanded="false">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a class="navbar-brand" href="#">
                        <img src="img/coke-logo.png">
                    </a>
                </div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <asp:LinkButton ID="LinkButton1" class="button-nav-icon" OnClick="btDesconectar_Click" runat="server">
                <i class="fa fa-power-off"></i></asp:LinkButton>
                    <asp:PlaceHolder runat="server" ID="phGotoMuro"><a href="default.aspx" title="ir al muro" class="button-nav-icon"><i class="fa fa-th"></i></a></asp:PlaceHolder>

                    <div class="user-logged"><%=nombreUsuario %></div>
                </div>
                <!-- /.navbar-collapse -->
            </div>
            <!-- /.container-fluid -->
        </nav>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="container">
                    <div class="panel panel-default">
                        <div class="panel-heading">Comunity Managers</div>
                        

                        <div class="panel-body">
                            <div class="pastilla-info">
                                Registros totales:
                                <asp:Label ID="tbTotales" Text="" runat="server" />
                                <asp:LinkButton ID="btnExcel" CssClass="excel-btn" Text="<i class='fa fa-file-excel-o'></i>Exportar fichero Excel" runat="server" OnClick="btnExcel_Click" />
                            </div>
       
                            
                            <!-- Button trigger modal -->
                            <button type="button" id="modalBorrarRoom" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#modalBorrar" runat="server">
                                Eliminar room
                            </button>

                            <!-- Modal Para BORRAR-->
                            <div class="modal fade" id="modalBorrar" tabindex="-1" role="dialog" aria-labelledby="myModalBorrar2">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close _closeModal" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title" id="myModalLabel3">¿Está seguro de que desea eliminar la room?</h4>
                                        </div>
                                        <div class="modal-body">
                                           
                                        </div>
                                        <div class="modal-footer">

                                            <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar</button>
                                            <asp:LinkButton ID="linkButtonBorrarRoom" runat="server"  Text="Aceptar"  OnClick="linkButtonBorrarRoom_Click" data-backdrop="false" AutoPostBack="true"/>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:DropDownList runat="server" AutoPostBack="true" ID="dpRooms" OnSelectedIndexChanged="dpRooms_SelectedIndexChanged" CssClass="form-control">
                             </asp:DropDownList>
                             <asp:DropDownList runat="server" ID="dpHashtagOrCm" CssClass="form-control" OnSelectedIndexChanged="dpHashtagOrCm_SelectedIndexChanged" AutoPostBack="true">
                                 <asp:ListItem>Cuenta</asp:ListItem>
                                 <asp:ListItem>Hashtag</asp:ListItem>
                               </asp:DropDownList>
                             <!-- Button trigger modal -->
                            <button type="button" id="buttonModalModificar" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#modalModificar" runat="server">
                                Modificar nombre room
                            </button>

                            <!-- Modal Para la modificacion-->
                            <div class="modal fade" id="modalModificar" tabindex="-1" role="dialog" aria-labelledby="myModalModificar2">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close _closeModal" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title" id="myModalLabel2">Modificar una Room</h4>
                                        </div>
                                        <div class="modal-body">
                                            <input type="text" placeholder="Nuevo nombre de la room" runat="server" id="dpTextoModificarRoom" maxlength="100" />
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                            <asp:LinkButton ID="LinkBotonModificar" runat="server"  Text="Modificar" OnClick="LinkBotonModifica_Click" data-backdrop="false" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <!-- Button trigger modal -->
                            <button type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#modalGuardar">
                                Añadir room
                            </button>

                            <!-- Modal -->
                            <div class="modal fade" id="modalGuardar" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <button type="button" class="close _closeModal" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                            <h4 class="modal-title" id="myModalLabel">Añadir una Room</h4>
                                        </div>
                                        <div class="modal-body">
                                            <input type="text" placeholder="Nombre de la room" runat="server" id="dpNombreRoom" maxlength="100" />
                                        </div>
                                        <div class="modal-footer">
                                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                            <asp:LinkButton ID="linkGuardarRoom" runat="server" OnClick="linkGuardarRoom_Click" Text="Guardar" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="navbar-form navbar-search navbar-right">
                                <%-- I will hid this fields when change the hashag or CM --%>
                                <div class="form-group">
                                    <asp:DropDownList runat="server" ID="dpSearchRS" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="dpSearchPais" CssClass="form-control">
                                    </asp:DropDownList>
                                    
                                    <input type="text" class="form-control" placeholder="Search" runat="server" id="searchBt" maxlength="100" />
                                </div>
                                <asp:LinkButton ID="lnkSearch" CssClass="excel-btn" Text="<i class='fa fa-search'></i>" runat="server" OnClick="lnkSearch_Click" />
                            </div>
                            <div class="table-responsive">
                                 <!-- <input type="hidden" id="hidCodPais" runat="server" />-->
                                <asp:GridView ID="gvPost" runat="server" CellPadding="2" CellSpacing="2" AutoGenerateColumns="False"
                                    EmptyDataText="No hay datos disponibles" BorderWidth="0px" GridLines="Horizontal"
                                    EnableModelValidation="True" AllowPaging="True" PageSize="25" BorderColor="#e5e5e5"
                                    ForeColor="Black" OnPageIndexChanging="gvPost_PageIndexChanging" OnRowEditing="gvPost_RowEditing"
                                    OnRowCancelingEdit="gvPost_RowCancelingEdit" OnRowCommand="gvPost_RowCommand" ShowFooter="True" CssClass="table table-bordered" OnRowDataBound="gvPost_RowDataBound">
                                    <Columns>
                                  
                                        <asp:TemplateField >
                                            <HeaderStyle CssClass="" Width="5px" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoCM" Text="" MaxLength="50" placeholder="Código CM" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Código CM</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbCM" Text='<%# Bind("codigoCM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="hidCM" Text='<%# Eval("codigoCM") %>' Visible="false" />
                                                <asp:Label runat="server" ID="tbCM"  Text='<%# Bind("codigoCM") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:DropDownList runat="server" ID="dpNuevoRS" >
                                                    <asp:ListItem Text="Facebook" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Twitter" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Instagram" Value="3"></asp:ListItem>
                                                     <asp:ListItem Text="YouTube" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Red Social</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal runat="server" ID="dplbRS" Text='<%# rs(Eval("codigoRS")) %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                  <asp:Label runat="server" ID="codRSMostrar"  Text='<%# rs( Eval("codigoRS")) %>'></asp:Label>
                                                <asp:DropDownList runat="server" ID="dpRS" Enabled="false" Visible="false" SelectedValue='<%# Bind("codigoRS") %>'>
                                                    <asp:ListItem Text="Facebook" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Twitter" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Instagram" Value="3"></asp:ListItem>
                                                     <asp:ListItem Text="YouTube" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" ID="hidRS" Text='<%# Eval("codigoRS") %>' Visible="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoNombre" Text="" MaxLength="500" placeholder="Nombre" CssClass="" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Nombre</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbNombre" Text='<%# Bind("nombre") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbNombre" Text='<%# Bind("nombre") %>' MaxLength="500" placeholder="Nombre" CssClass="" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoNickName" Text="" MaxLength="500" placeholder="Nick Name" CssClass="" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Nick Name</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbNickName" Text='<%# Bind("nickname") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label runat="server" ID="tbNickName"  Text='<%# Bind("nickname") %>' CssClass="" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoNAmigos" Text="" MaxLength="15" placeholder="Nº Amigos" CssClass="" onkeypress="return onlyNumber(event, this)" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Nº Amigos</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbNAmigos" Text='<%# Bind("numAmigos") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Label runat="server" ID="tbNAmigos" Text='<%# Bind("numAmigos") %>' CssClass=""  />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:DropDownList runat="server" ID="dpNuevoAgrupacion" CssClass="dpPais" >
                                     
                                                   
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Agrupación</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal runat="server" ID="dplbAgrupacion" Text='<%# Eval("agrupacion") %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList runat="server" ID="dpAgrupacion" CssClass="dpPais" >

                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" ID="hidAgrupacion" Text='<%# Eval("idAgrupacion") %>' Visible="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoPais" Text="" MaxLength="500" placeholder="País" CssClass="tbPais" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">País</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbPais" Text='<%# Bind("pais") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbPais" Text='<%# Bind("pais") %>' MaxLength="500" placeholder="País" CssClass="tbPais" />
                                                    <asp:TextBox runat="server" ID="hidPais" Text='<%# Eval("idLocalizacion") %>' Visible="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoContacto" Text="" MaxLength="500" placeholder="Email" CssClass="tbContacto" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Contacto</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbContacto" Text='<%# Bind("contacto") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbContacto" Text='<%# Bind("contacto") %>' MaxLength="500" placeholder="Contacto" CssClass="tbContacto" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:CheckBox ID="chkNoAccesibleNuevo" Enabled="false" Text="" runat="server" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">No Accesible</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkNoAccesible" Text="" runat="server" Checked='<%# Bind("noAccesible") %>' Enabled="false" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkNoAccesibleEdit" Text="" runat="server" Enabled="false" Checked='<%# Bind("noAccesible") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:CheckBox ID="chkNuevoAsignado" Text="" runat="server" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Asignado</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chklbAsignado" Text="" runat="server" Checked='<%# Bind("asignado") %>' Enabled="false" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:CheckBox ID="chkAsignado" Text="" runat="server" Checked='<%# Bind("asignado") %>' />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkGuardarNuevo" CssClass="btn btn-info" runat="server" CommandArgument='<%# Eval("codigoCM")+","+ Eval("CodigoRS") %>' CommandName="GuardarNuevo" data-toggle="tooltip" title="Guardar Nuevo"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditar" CssClass="btn btn-info" CommandArgument='<%# Eval("codigoCM")+","+ Eval("CodigoRS") %>' runat="server" CommandName="Edit" Visible='<%# verBtn == true %>' data-toggle="tooltip" title="Editar"><i class="fa fa-pencil"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkGuardar" CssClass="btn" CommandArgument='<%# Eval("codigoCM")+","+ Eval("CodigoRS") %>' runat="server" CommandName="Guardar" data-toggle="tooltip" title="Guardar"><i class="fa fa-floppy-o"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" CssClass="btn btn-info" CommandArgument='<%# Eval("idAdminComunity") %>' runat="server" CommandName="Borrar" Visible='<%# verBtn == true %>' data-toggle="tooltip" title="Eliminiar" OnClientClick="return confirm('Se va a eliminar el Community Manager.\n¿Desea continuar?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkCancelar" CssClass="btn btn-info" runat="server" CommandName="Cancel" data-toggle="tooltip" title="Cancelar"><i class="fa fa-undo"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="enlaces ultima pagination" BackColor="White" />
                                </asp:GridView>
<%--                                In this place we will make the second panel so we need to duplicate the gridview and complete it with new fields and id--%> 
                                <asp:GridView ID="GridViewHash" runat="server" CellPadding="2" CellSpacing="2" AutoGenerateColumns="False"
                                    EmptyDataText="No hay datos disponibles" BorderWidth="0px" GridLines="Horizontal"
                                    EnableModelValidation="True" AllowPaging="True" PageSize="25" BorderColor="#e5e5e5"
                                    ForeColor="Black" OnPageIndexChanging="GridViewHash_PageIndexChanging" OnRowEditing="GridViewHash_RowEditing"
                                    OnRowCancelingEdit="GridViewHash_RowCancelingEdit" OnRowCommand="GridViewHash_RowCommand" ShowFooter="True" CssClass="table table-bordered" OnRowDataBound="GridViewHash_RowDataBound" Visible="false">
                                    <Columns>
                                        <%-- Hashtag--%>
                                        <asp:TemplateField >
                                            <HeaderStyle CssClass="" Width="5px" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbHashIdNuevo" Text="" MaxLength="50" placeholder="Hashtag" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">HashTag</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbHashtagId" Text='<%# Bind("codigoCM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="hidHashTagId" Text='<%# Eval("codigoCM") %>' Visible="false" />
                                                <asp:Label runat="server" ID="tbHashTagId"  Text='<%# Bind("codigoCM") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                      
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:DropDownList runat="server" ID="dpRsHashNew" >
                                                    <asp:ListItem Text="Twitter" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Instagram" Value="3"></asp:ListItem>
                                                    <asp:ListItem Text="YouTube" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Red Social</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Literal runat="server" ID="dplblHashRs" Text='<%# rs(Eval("codigoRS")) %>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                  <asp:Label runat="server" ID="codRSMostrar"  Text='<%# rs( Eval("codigoRS")) %>'></asp:Label>
                                                <asp:DropDownList runat="server" ID="dpRsHashEdit" Enabled="false" Visible="false" SelectedValue='<%# Bind("codigoRS") %>'>
                                                    
                                                    <asp:ListItem Text="Twitter" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="Instagram" Value="3"></asp:ListItem>
                                                     <asp:ListItem Text="YouTube" Value="4"></asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox runat="server" ID="hidHashRs" Text='<%# Eval("codigoRS") %>' Visible="false" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                   
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkGuardarNuevoHash" CssClass="btn btn-info" runat="server" CommandArgument='<%# Eval("codigoCM")+","+ Eval("CodigoRS") %>' CommandName="GuardarNuevoHash" data-toggle="tooltip" title="Guardar Nuevo"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                            </FooterTemplate>
                                           <%-- <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditarHash" CssClass="btn btn-info" CommandArgument='<%# Eval("codigoCM")+","+ Eval("CodigoRS") %>' runat="server" CommandName="EditHash" Visible='<%# verBtn == true %>' data-toggle="tooltip" title="Editar"><i class="fa fa-pencil"></i></asp:LinkButton>
                                            </ItemTemplate>--%>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkGuardarHash" CssClass="btn" CommandArgument='<%# Eval("codigoCM")+","+ Eval("CodigoRS") %>' runat="server" CommandName="GuardarHash" data-toggle="tooltip" title="Guardar"><i class="fa fa-floppy-o"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminarHash" CssClass="btn btn-info" CommandArgument='<%# Eval("idAdminComunity") %>' runat="server" CommandName="BorrarHash" Visible='<%# verBtn == true %>' data-toggle="tooltip" title="Eliminiar" OnClientClick="return confirm('Se va a eliminar el Hashtag.\n¿Desea continuar?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkCancelarHash" CssClass="btn btn-info" runat="server" CommandName="CancelHash" data-toggle="tooltip" title="Cancelar"><i class="fa fa-undo"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="enlaces ultima pagination" BackColor="White" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>

            </ContentTemplate>

        </asp:UpdatePanel>
       <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
        
        <script src="js/main.js"></script>
        <script src="js/validaciones.js"></script>
        <script src="js/jquery-ui.min.js"></script>
        <script>
            $(document).ready(function () {
                //app.main.mostrarRooms();

                autocompletePaises();
            });
           $(document).on("click", "#LinkBotonModificar", function (e) {

               $('._closeModal').click();

           });

           $(document).on("click", "#linkGuardarRoom", function (e) {

               $('._closeModal').click();

           });
           $(document).on("click", "#linkButtonBorrarRoom", function (e) {

               $('._closeModal').click();

           });          
            function autocompletePaises() {
                $(document).on("focus", ".tbPais", function (event) {
                    $(event.target).autocomplete({
                        source: function (request, response) {
                            var datos = { "searchString": request.term, "agrupacion": $(event.target).parent().parent().find(".dpPais").val() };
                            $.ajax({
                                type: "POST",
                                contentType: "application/json",
                                url: "/ws.svc/getListPaisesByAgrupacion",
                                data: JSON.stringify(datos),
                                success: function (data) {
                                    response($.map(data.d.data, function (item) {
                                        return {
                                            code: item.code,
                                            value: item.value
                                        }
                                    }))
                                },
                                error: function (data) {
                                    alert('a');
                                
                                }
                            });
                        },
                        minLength: 2,
                        select: function (event, ui) {
                            $(event.target).val(ui.item.value);
                            $("#hidCodPais").val(ui.item.code);

                        },
                     
                        open: function () {
                            $(this).removeClass("ui-corner-all").addClass("ui-corner-top");
                        },
                        close: function () {
                            $(this).removeClass("ui-corner-top").addClass("ui-corner-all");
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus);
                        }
                    });
                });

            }
         
        



    </script>

    </form>
</body>
</html>
