<%@ Page Language="C#" AutoEventWireup="true" CodeFile="usuarios.aspx.cs" Inherits="usuarios" %>

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
                        <div class="navbar-form navbar-search navbar-right">
                            <div class="form-group">
                                Nombre:
                                    
                                    <input type="text" class="form-control" placeholder="Search" runat="server" id="searchBt" maxlength="100" />
                            </div>
                            <asp:LinkButton ID="lnkSearch" CssClass="excel-btn" Text="<i class='fa fa-search'></i>" runat="server" OnClick="lnkSearch_Click" />
                        </div>

                        <div class="panel-body">
                            <div class="pastilla-info">
                                Registros totales:
                                <asp:Label ID="tbTotales" Text="" runat="server" />

                            </div>


                            <div class="table-responsive">
                                <!-- <input type="hidden" id="hidCodPais" runat="server" />-->
                                <asp:GridView ID="gvPost" runat="server" CellPadding="2" CellSpacing="2" AutoGenerateColumns="False"
                                    EmptyDataText="No hay datos disponibles" BorderWidth="0px" GridLines="Horizontal"
                                    EnableModelValidation="True" AllowPaging="True" PageSize="15" BorderColor="#e5e5e5"
                                    ForeColor="Black" OnPageIndexChanging="gvPost_PageIndexChanging" OnRowEditing="gvPost_RowEditing"
                                    OnRowCancelingEdit="gvPost_RowCancelingEdit" OnRowCommand="gvPost_RowCommand" ShowFooter="True" CssClass="table table-bordered" OnRowDataBound="gvPost_RowDataBound">
                                    <%--OnRowDataBound="gvPost_RowDataBound"--%>
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <HeaderStyle CssClass="" Width="5px" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbIdAdmin" Text="" MaxLength="50" placeholder="IdAdmin" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">IdAdmin</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbCM" Text='<%# Bind("codigo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="hidIdAdmin" Text='<%# Eval("codigo") %>' Visible="false" />
                                                <asp:Label runat="server" ID="tbIdAdmin" Text='<%# Bind("codigo") %>'></asp:Label>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5px" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoUser" Text="" MaxLength="50" placeholder="User" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">User</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbUser" Text='<%# Bind("login") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbUser" Text='<%# Bind("login") %>' MaxLength="500" placeholder="Nombre" CssClass="" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>

                                        <%--   <asp:TemplateField>
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
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:TextBox runat="server" ID="tbNuevoPassword" Text="" MaxLength="500" placeholder="Password" CssClass="" />
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Password</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbPassword" Text='<%# Bind("password") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox runat="server" ID="tbPassword" Text='<%# Bind("password") %>' MaxLength="500" placeholder="Password" CssClass="" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <%--  Nombre del administrador--%>
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
                                                <asp:TextBox runat="server" ID="tbNombre" Text='<%# Bind("nombre") %>' MaxLength="500" placeholder="Password" CssClass="" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                 
                                                 <asp:CheckBox ID="permiso1Nuevo" runat="server" ></asp:CheckBox>Premiso1
                                                <asp:CheckBox ID="permiso2Nuevo" runat="server" ></asp:CheckBox>Premiso2
                                                <asp:CheckBox ID="permiso3Nuevo" runat="server" ></asp:CheckBox>Premiso3
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <span class="table-header" style="font-weight: normal;">Permisos</span>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdProcesos" runat="server" Value='<%# Bind("procesosPermitidos") %>'/>
                                                <asp:CheckBox ID="permiso1" runat="server"  Enabled="false"></asp:CheckBox>Premiso1
                                                <asp:CheckBox ID="permiso2" runat="server"  Enabled="false"></asp:CheckBox>Premiso2
                                                <asp:CheckBox ID="permiso3" runat="server" Enabled="false"></asp:CheckBox>Premiso3
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:HiddenField ID="hdProcesos" runat="server" Value='<%# Bind("procesosPermitidos") %>'/>
                                                 <asp:CheckBox ID="permiso1Edit" runat="server" ></asp:CheckBox>Premiso1
                                                <asp:CheckBox ID="permiso2Edit" runat="server" ></asp:CheckBox>Premiso2
                                                <asp:CheckBox ID="permiso3Edit" runat="server" ></asp:CheckBox>Premiso3
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkGuardarNuevo" CssClass="btn btn-info" runat="server" CommandName="GuardarNuevo" data-toggle="tooltip" title="Guardar Nuevo"><i class="fa fa-plus-circle"></i></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEditar" CssClass="btn btn-info" runat="server" CommandName="Edit" Visible='<%# verBtn == true %>' data-toggle="tooltip" title="Editar"><i class="fa fa-pencil"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkGuardar" CssClass="btn" runat="server" CommandArgument='<%# Eval("codigo")%>' CommandName="Guardar" data-toggle="tooltip" title="Guardar"><i class="fa fa-floppy-o"></i></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <HeaderStyle CssClass="" Width="5%" />
                                            <ItemStyle CssClass="" />
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkEliminar" CssClass="btn btn-info" runat="server" CommandArgument='<%# Eval("codigo")%>' CommandName="Borrar" Visible='<%# verBtn == true %>' data-toggle="tooltip" title="Eliminiar" OnClientClick="return confirm('Se va a eliminar el Administrador.\n¿Desea continuar?');"><i class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="lnkCancelar" CssClass="btn btn-info" runat="server" CommandName="Cancel" data-toggle="tooltip" title="Cancelar"><i class="fa fa-undo"></i></asp:LinkButton>
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


            });









        </script>

    </form>
</body>
</html>
