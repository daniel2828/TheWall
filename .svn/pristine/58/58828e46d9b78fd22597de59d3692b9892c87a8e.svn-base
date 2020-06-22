<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" EnableEventValidation="false" %>

<!DOCTYPE html>

  <head>
        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
        <title>BrandWall</title>

        <!-- Bootstrap -->
        <link href="css/bootstrap.css" rel="stylesheet">
        <link href="css/style.css" rel="stylesheet">
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
        <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700|Open+Sans:400,300,700,600' rel='stylesheet' type='text/css'>

        <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
        <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
        <!--[if lt IE 9]>
          <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
          <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
        <![endif]-->
    </head>
    <body class="loginBg">
        <form id="form1" runat="server">
     <div class="container">
    <div class="row vertical-offset-100">
        <div class="col-md-4 col-md-offset-4 login-panel">
            <div class="panel panel-default">
                <div class="panel-heading">
                    <img src="img/coke-logo.png">
                    <h3 class="panel-title">Please, login to continue</h3>
                </div>
                <div class="panel-body">
                    <form accept-charset="UTF-8" role="form">
                    <fieldset>
                        <div class="form-group">
                            <input class="form-control" placeholder="E-mail" name="email" id="email" type="text" runat="server">
                        </div>
                        <div class="form-group">
                            <input class="form-control" placeholder="Contraseña" name="password" id="password" type="password" value="" runat="server">
                        </div>
                       <%-- <div class="checkbox">
                            <label>
                                <input name="remember" type="checkbox" value="Remember Me"> Remember me
                            </label>
                        </div>--%>
                        <asp:Button ID="btLogin" runat="server" Text="Enter" OnClick="btLogin_Click" class="btn btn-lg btn-red btn-block animation"  />
                       <%-- <input class="btn btn-lg btn-red btn-block animation" type="submit" value="Enter">--%>
                    </fieldset>
                    </form>
                </div>

                   <div class="panel-footer">
                    <span>Having any issue? Contact us or ask for free access:</span> <br>
                    <i class="fa fa-envelope" aria-hidden="true"></i><a href="mailto:brandwall@funcionasi.es">brandwall@funcionasi.es</a> <br>
                    <i class="fa fa-phone" aria-hidden="true"></i> +34 915 503 955
                </div>
            </div>
        </div>
    </div>
</div>

<div class="copyright">
    <span>Code and idea by:</span><a href="http://www.funcionasi.es" target="_blank"><img src="img/logoNegro.svg"></a>
</div>
        



        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
        <script src="js/main.js"></script>
        <script src="http://mymaplist.com/js/vendor/TweenLite.min.js"></script>
     
        <script type="text/javascript">
            $(document).ready(function () {
                $(document).mousemove(function (e) {
                    TweenLite.to($('body'),
                       .5,
                       {
                           css:
                             {
                                 backgroundPosition: "" + parseInt(event.pageX / 8) + "px " + parseInt(event.pageY / '12') + "px, " + parseInt(event.pageX / '15') + "px " + parseInt(event.pageY / '15') + "px, " + parseInt(event.pageX / '30') + "px " + parseInt(event.pageY / '30') + "px"
                             }
                       });
                });
            });
        </script>
            </form>
    </body>
</html>
