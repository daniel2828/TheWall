﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <title>BrandWall</title>

    <!-- Bootstrap -->
    <link href="css/bootstrap.css" rel="stylesheet">
    <link href="css/style.css" rel="stylesheet">
    <link href="css/bootstrap-datetimepicker.css" rel="stylesheet">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css">
    <link href='https://fonts.googleapis.com/css?family=Montserrat:400,700|Open+Sans:400,300,700,600' rel='stylesheet' type='text/css'>
    <link rel="stylesheet" href="css/multiple-select.css" />

    <!-- HTML5 shim and Respond.js for IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!-- [if lt IE 9]>
                  <script src="https://oss.maxcdn.com/html5shiv/3.7.2/html5shiv.min.js"></script>
                  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
                  <![endif]-->
</head>
<body>




    <!-- Modal -->


    <!-- Boton de ? que aparece en el muro para obtener ayuda sobre los iconos  -->
    <div class="modal fade" id="helpModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <!-- <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button> -->
                    <h4 class="modal-title" id="myModalLabel">Help</h4>
                </div>
                <div class="modal-body">

                    <div class="help-section">
                        <h3>Icons meaning</h3>

                        <div class="help-item">
                            <i class="fa fa-tachometer"></i>
                            <span>Effectivity</span>
                        </div>

                        <div class="help-item">
                            <i class="fa fa-bolt"></i>
                            <span>Score</span>
                        </div>

                        <div class="help-item">
                            <i class="fa fa-male"></i>
                            <span>Total Users</span>
                        </div>

                        <div class="help-item">
                            <i class="fa fa-thumbs-up"></i>
                            <i class="fa fa-star"></i>
                            <i class="fa fa-heart"></i>
                            <span>Like</span>
                        </div>

                        <div class="help-item">
                            <i class="fa fa-comment-o"></i>
                            <i class="fa fa-reply"></i>
                            <span>Comments</span>
                        </div>

                        <div class="help-item">
                            <i class="fa fa-retweet"></i>
                            <i class="fa fa-share-square-o"></i>
                            <span>Shared</span>
                        </div>
                    </div>






                    <div class="help-section scoreSection">
                        <h3>Colors meaning</h3>



                        <div class="help-item score3">
                            <i class="fa fa-tachometer"></i>
                            <span>High rating</span>
                        </div>

                        <div class="help-item score2">
                            <i class="fa fa-tachometer"></i>
                            <span>Medium rating</span>
                        </div>

                        <div class="help-item score0">
                            <i class="fa fa-tachometer"></i>
                            <span>Low rating</span>
                        </div>

                        <div class="help-item noscore">
                            <i class="fa fa-tachometer"></i>
                            <span>No rating</span>
                        </div>


                    </div>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close help</button>
                </div>
            </div>
        </div>
    </div>






    <form id="form1" runat="server">
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
                        <img src="img/coke-logo-small.png">
                    </a>

                    <div class="claim">Inspire your post</div>
                </div>


                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <asp:LinkButton ID="LinkButton1" class="button-nav-icon" OnClick="btDesconectar_Click" runat="server" alt="Disconect">
                  <i class="fa fa-power-off animation"></i></asp:LinkButton>
                    <asp:PlaceHolder runat="server" ID="phGotoManagers"><a href="comunityManagers.aspx" title="Go to administration page" class="button-nav-icon"><i class="fa fa-cog animation"></i></a></asp:PlaceHolder>

                    <a class="button-nav-icon animation btnMisFavoritos" alt="See all favorites"><i class="fa fa-heart animation"></i></a>

                    <a class="button-nav-icon animation" alt="Show help" data-toggle="modal" data-target="#helpModal"><i class="fa fa-question animation"></i></a>



                    <div class="user-logged"><%=nombreUsuario %></div>

                    <!-- Filtros, incluido la room -->
                    <div class="filters">

                        <div class="filterBox">
                            <h2>ROOM :</h2>
                            <div class="filtro">
                                <select id="selectRooms">
                                    <%--<option value="home">home</option>
                                    <option value="room1">room1</option>--%>

                                </select>
                            </div>

                            <h2>Filter by: </h2>


                            <!--Filtros de la pagina por red social -->
                            <div class="rrssFilters">
                                <button class="filter-button fbFilter "><i class="fa fa-facebook-official"></i></button>
                                <button class="filter-button twFilter "><i class="fa fa-twitter-square"></i></button>
                                <button class="filter-button insFilter"><i class="fa fa-instagram"></i></button>
                                <button class="filter-button ytFilter"><i class="fa fa-youtube-play"></i></button>
                            </div>
                            <!-- Filtro Pais -->
                            <div class="filtro filtroPais">
                                <select multiple="multiple" id="multipleSelect">
                                </select>
                            </div>
                            <div class="filtro filtroCuentas">
                                <select multiple="multiple" id="multipleSelectCuentas">
                                </select>
                            </div>

                            <!-- Filtro fecha -->
                            <div class="date-filters">
                                <div class="form-group">
                                    <div class='input-group date' id='datetimepicker1'>
                                        <input type='text' class="form-control inputDesde" placeholder="From" />
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class='input-group date' id='datetimepicker2'>
                                        <input type='text' class="form-control inputHasta" placeholder="to" />
                                        <span class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </span>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <!-- Filtro de ordenacion -->
                        <div class="filterBox">
                            <h2>Sort by: </h2>

                            <div class="filtro ">
                                <select id="sort">
                                    <option value="0" selected>None</option>
                                    <option value="5">Effectivity</option>
                                    <option value="4">Score</option>
                                    <option value="2">Likes</option>
                                    <option value="3">Comments</option>
                                    <option value="1">Total Users</option>
                                </select>
                            </div>

                            <div class="radio sortArrows" data-sort="0" alt="Highest First">
                                <label>
                                    <input type="radio" name="sort"/><i class="fa fa-arrow-down animation"></i></label>
                            </div>


                            <div class="radio sortArrows" data-sort="1" alt="Lowest First">
                                <label>
                                    <input type="radio" name="sort"/><i class="fa fa-arrow-up animation"></i></label>
                            </div>






                        </div>

                        <button class="goButton animation">Apply</button>



                    </div>


                </div>
            </div>
         </nav>
        <!--  WALL START  -->
        <div class="grid" id="grid">
        <!--  TWITTER ITEM   -->
            <div class="grid-item twitter-item" style="display: none">
                <div class="content">

                    <div class="item-header">

                        <div class="userInfo">

                            <div class="userImage"></div>
                            <div class="userName"></div>
                            <div class="userNick"></div>

                        </div>
                        <div class="color-band">

                            <div class="rrssIcon animation">
                                <i class="fa fa-facebook-square"></i>
                                <i class="fa fa-twitter-square"></i>
                                <i class="fa fa-instagram"></i>
                            </div>


                        </div>

                        <div class="item-header-buttons">
                            <a href="#" class="fav" data-toggle="tooltip" data-delay="500" data-placement="left" title="Pin to favorites">
                                <i class="fa fa-heart"></i>

                            </a>
                            <a href="#" target="_blank" class="gotoSource" data-toggle="tooltip" data-delay="500" data-placement="left" title="Go to original post">
                                <i class="fa fa-external-link"></i>

                            </a>
                            <a href="#" class="sprinklr"></a>
                        </div>

                    </div>

                    <div class="item-body">
                        <div class="media-container containerImg">
                            <a href="#" target="_blank" class="open-image animation" data-toggle="tooltip" data-placement="bottom" title="Open in a new tab"><i class="fa fa-external-link"></i></a>
                            <img src="">
                        </div>
                        <div class="media-container containerVideo">
                            <a href="#" class="playBtnIcon animation" target="_blank">
                                <i class="fa fa-play-circle animation"></i>
                            </a>
                            <img src="img/twitterVideo.jpg">
                        </div>
                        <div class="media-container containerLink">
                            <a href="#" class="animation" target="_blank"><<i class="fa fa-expand"></i>
                               
                            </a>
                        </div>
                        <div class="txtPost containerTexto">
                        </div>
                    </div>

                    <div class="item-footer">
                        <div class="date-item fechaPost"></div>
                        <div class="itemStats">
                            <div class="itemStat numShare" data-toggle="tooltip" data-placement="top" data-delay="500" title="Retweets">
                                <span></span>
                                <i class="fa fa-retweet"></i>
                            </div>
                            <div class="itemStat numLike" data-toggle="tooltip" data-placement="top" data-delay="500" title="Favorite">
                                <span></span>
                                <i class="fa fa-star"></i>
                            </div>
                            <div class="itemStat numComment" data-toggle="tooltip" data-placement="top" data-delay="500" title="Replys">
                                <span></span>
                                <i class="fa fa-reply"></i>
                            </div>
                        </div>

                        <div class="itemResults">



                            <div class="item-score  efectivity" data-toggle="tooltip" data-placement="top" data-delay="500" title="Effectivity">
                                <i class="fa fa-tachometer"></i>
                                <span class="score-number"></span>
                            </div>

                            <div class="item-score  ranking" data-toggle="tooltip" data-placement="top" data-delay="2500" title="Score">
                                <i class="fa fa-bolt"></i>
                                <span class="score-number"></span>
                            </div>


                            <div class="item-score  totalUsers" data-toggle="tooltip" data-placement="top" data-delay="500" title="Total Users">
                                <i class="fa fa-male"></i>
                                <span class="score-number"></span>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
            <!--  FACEBOOK ITEM   -->
            <div class="grid-item fb-item" style="display: none">
                <div class="content">

                    <div class="item-header">
                        <div class="userInfo">

                            <div class="userImage"></div>
                            <div class="userName"></div>
                            <div class="userNick"></div>

                        </div>

                        <div class="item-header-buttons">
                            <a href="#" class="fav" data-toggle="tooltip" data-delay="500" data-placement="left" title="Pin to favorites">
                                <i class="fa fa-heart"></i>

                            </a>
                            <a href="#" target="_blank" class="gotoSource" data-toggle="tooltip" data-delay="500" data-placement="left" title="Go to original post">
                                <i class="fa fa-external-link"></i>

                            </a>
                            <a href="#" class="sprinklr"></a>
                        </div>

                        <div class="color-band">

                            <div class="rrssIcon animation">
                                <i class="fa fa-facebook-square"></i>
                                <i class="fa fa-twitter-square"></i>
                                <i class="fa fa-instagram"></i>
                            </div>
                        </div>
                    </div>

                    <div class="item-body">
                        <div class="media-container containerLink">
                            <a href="#" class="animation" target="_blank">
                                <i class="fa fa-external-link-square"></i>
                               
                            </a>
                        </div>
                        <div class="media-container containerVideo">
                            <a href="#" class="playBtnIcon animation" target="_blank">
                                <i class="fa fa-play-circle animation"></i>
                            </a>
                            <img src="">
                        </div>
                        <div class="media-container containerImg">
                            <a href="#" target="_blank" class="open-image animation" data-toggle="tooltip" data-placement="bottom" title="Open in a new tab">
                                <i class="fa fa-expand"></i>
                            </a>
                            <img src="">
                        </div>
                        <div class="txtPost containerTexto">
                        </div>
                    </div>

                    <div class="item-footer">
                        <div class="date-item fechaPost"></div>
                        <div class="itemStats">
                            <div class="itemStat numLike" data-toggle="tooltip" data-placement="top" data-delay="500" title="likes">
                                <span></span>
                                <i class="fa fa-thumbs-up"></i>
                            </div>
                            <div class="itemStat numComment" data-toggle="tooltip" data-placement="top" data-delay="500" title="Comments">
                                <span></span>
                                <i class="fa fa-comment-o"></i>
                            </div>
                            <div class="itemStat numShare" data-toggle="tooltip" data-placement="top" data-delay="500" title="Shared">
                                <span></span>
                                <i class="fa fa-share-square-o"></i>
                            </div>

                        </div>
                        <div class="itemResults">



                            <div class="item-score  efectivity" data-toggle="tooltip" data-placement="top" data-delay="500" title="Effectivity">
                                <i class="fa fa-tachometer"></i>
                                <span class="score-number"></span>
                            </div>

                            <div class="item-score  ranking" data-toggle="tooltip" data-placement="top" data-delay="500" title="Score">
                                <i class="fa fa-bolt"></i>
                                <span class="score-number"></span>
                            </div>

                            <div class="item-score  totalUsers" data-toggle="tooltip" data-placement="top" data-delay="500" title="Total Users">
                                <i class="fa fa-male"></i>
                                <span class="score-number"></span>
                            </div>


                        </div>
                    </div>

                </div>
            </div>
            <!--  YOTUBE ITEM   -->
            <div class="grid-item yt-item" style="display: none">
                <div class="content">

                    <div class="item-header">
                        <div class="userInfo">

                            <div class="userImage"></div>
                            <div class="userName"></div>
                            <div class="userNick"></div>

                        </div>
                        <div class="color-band">

                            <div class="rrssIcon animation">
                                <i class="fa fa-facebook-square"></i>
                                <i class="fa fa-twitter-square"></i>
                                <i class="fa fa-instagram"></i>
                                <i class="fa fa-youtube"></i>

                            </div>
                        </div>

                        <div class="item-header-buttons">
                            <a href="#" class="fav" data-toggle="tooltip" data-delay="500" data-placement="left" title="Pin to favorites">
                                <i class="fa fa-heart"></i>

                            </a>
                            <a href="#" target="_blank" class="gotoSource" data-toggle="tooltip" data-delay="500" data-placement="left" title="Go to original post">
                                <i class="fa fa-external-link"></i>

                            </a>
                            <a href="#" class="sprinklr"></a>
                        </div>
                    </div>

                    <div class="item-body">
                        <div class="media-container containerLink">
                            <a href="#" class="animation" target="_blank">
                                <i class="fa fa-external-link-square"></i>
                     
                            </a>
                        </div>
                        <div class="media-container containerVideo">
                            <a href="#" class="playBtnIcon animation" target="_blank">
                                <i class="fa fa-play-circle animation"></i>
                            </a>
                            <img src="">
                        </div>
                        <div class="media-container containerImg">
                            <a href="#" target="_blank" class="open-image animation" data-toggle="tooltip" data-placement="bottom" title="Open in a new tab"><i class="fa fa-expand"></i></a>
                            <img src="">
                        </div>
                        <div class="txtPost containerTexto">
                        </div>
                    </div>

                    <div class="item-footer">
                        <div class="date-item fechaPost"></div>
                        <div class="itemStats">
                            <div class="itemStat numLike" data-toggle="tooltip" data-placement="top" data-delay="500" title="Likes">
                                <span></span>
                                <i class="fa fa-thumbs-up"></i>
                            </div>
                            <div class="itemStat numComment" data-toggle="tooltip" data-placement="top" data-delay="500" title="Comments">
                                <span></span>
                                <i class="fa fa-comment-o"></i>
                            </div>


                        </div>
                        <div class="itemResults">



                            <div class="item-score  efectivity" data-toggle="tooltip" data-placement="top" data-delay="500" title="Effectivity">
                                <i class="fa fa-tachometer"></i>
                                <span class="score-number"></span>
                            </div>

                            <div class="item-score  ranking" data-toggle="tooltip" data-placement="top" data-delay="500" title="Score">
                                <i class="fa fa-bolt"></i>
                                <span class="score-number"></span>
                            </div>

                            <div class="item-score  totalUsers" data-toggle="tooltip" data-placement="top" data-delay="500" title="Total Users">
                                <i class="fa fa-male"></i>
                                <span class="score-number"></span>
                            </div>


                        </div>
                    </div>

                </div>
            </div>
         <!--  INSTAGRAM ITEM   -->
            <div class="grid-item insta-item" style="display: none">
                <div class="content">

                    <div class="item-header">
                        <div class="userInfo">

                            <div class="userImage"></div>
                            <div class="userName"></div>
                            <div class="userNick"></div>

                        </div>
                        <div class="color-band">

                            <div class="rrssIcon animation">
                                <i class="fa fa-facebook-square"></i>
                                <i class="fa fa-twitter-square"></i>
                                <i class="fa fa-instagram"></i>
                            </div>


                        </div>

                        <div class="item-header-buttons">
                            <a href="#" class="fav" data-toggle="tooltip" data-delay="500" data-placement="left" title="Pin to favorites">
                                <i class="fa fa-heart"></i>

                            </a>
                            <a href="#" target="_blank" class="gotoSource" data-toggle="tooltip" data-delay="500" data-placement="left" title="Go to original post">
                                <i class="fa fa-external-link"></i>

                            </a>
                            <a href="#" class="sprinklr"></a>
                        </div>
                     </div>

                    <div class="item-body">
                        <div class="media-container containerLink">
                            <a href="#" class="animation" target="_blank">
                                <i class="fa fa-external-link-square"></i>
                                <!-- loremipsumfake.com/article/url/02345534655-->
                            </a>
                        </div>
                        <div class="media-container containerImg">
                            <a href="#" target="_blank" class="open-image animation" data-toggle="tooltip" data-placement="bottom" title="Open in a new tab"><i class="fa fa-expand"></i></a>
                            <img src="">
                        </div>
                        <div class="media-container containerVideo">
                            <a href="#" class="playBtnIcon animation" target="_blank">
                                <i class="fa fa-play-circle animation"></i>
                            </a>
                            <img src="">
                        </div>

                        <div class="txtPost containerTexto">
                        </div>
                    </div>

                    <div class="item-footer">

                        <div class="date-item fechaPost"></div>
                        <div class="itemStats">

                            <div class="itemStat numLike" data-toggle="tooltip" data-placement="top" title="Likes">
                                <span></span>
                                <i class="fa fa-heart-o"></i>
                            </div>

                            <div class="itemStat numComment" data-toggle="tooltip" data-placement="top" title="Comments">
                                <span></span>
                                <i class="fa fa-comment-o"></i>
                            </div>
                        </div>

                        <div class="itemResults">



                            <div class="item-score  efectivity" data-toggle="tooltip" data-placement="top" data-delay="500" title="Effectivity">
                                <i class="fa fa-tachometer"></i>
                                <span class="score-number"></span>
                            </div>

                            <div class="item-score  ranking" data-toggle="tooltip" data-placement="top" data-delay="500" title="Score">
                                <i class="fa fa-bolt"></i>
                                <span class="score-number"></span>
                            </div>

                            <div class="item-score  totalUsers" data-toggle="tooltip" data-placement="top" data-delay="500" title="Total Users">
                                <i class="fa fa-male"></i>
                                <span class="score-number"></span>
                            </div>


                        </div>
                    </div>

                </div>
            </div>
         </div>

        <div class="loadingBottom ocultar">
            <img src="img/loading3.gif">
        </div>
        <!--  WALL END  -->


        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>
        <script src="js/bootstrap.min.js"></script>
        <script src="js/masonry.pkgd.min.js"></script>
        <script src="js/imagesloaded.pkgd.min.js"></script>
        <script src="js/moment.js"></script>
        <script type="text/javascript" src="js/bootstrap-datetimepicker.min.js"></script>
        <script src="js/jquery.multiple.select.js"></script>
        <script src="js/main.js"></script>

        <script>
            app.main.init();
        </script>
        <script type="text/javascript">
            $(function () {
                $('[data-toggle="tooltip"]').tooltip()
            })
        </script>

        <script type="text/javascript">
            $(function () {
                $('#datetimepicker1').datetimepicker({ format: 'DD/MM/YYYY' });
                $('#datetimepicker2').datetimepicker({
                    format: 'DD/MM/YYYY',
                    useCurrent: false //Important! See issue #1075
                });
                $("#datetimepicker1").on("dp.change", function (e) {
                    $('#datetimepicker2').data("DateTimePicker").minDate(e.date);
                });
                $("#datetimepicker2").on("dp.change", function (e) {
                    $('#datetimepicker1').data("DateTimePicker").maxDate(e.date);
                });
            });
        </script>




    </form>

</body>

</html>
