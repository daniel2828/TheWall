var app = app || {};
var _masnry;


app.main = {};
app.navegacion = {};
app.utils = {};
app.ajax = {};
app.visual = {};
//Espacio de nombres
app.main = {
    site_URL: "ws.svc/",
    pidiendoPagina: false,
    paginaActual: 0,
    totalPaginas: 0,
    boxPostTwitter: null,
    boxPostFacebook: null,
    boxPostInstagram: null,
    boxPostYoutube: null,
    numPostsPagina: 20,
    numPartes: 3,
    tercerParte: 33,
    dDate: "",
    hDate: "",
    pais: "",
    cuenta: "",
    fb: 1,
    tw: 1,
    criterio: 0,
    ascendente: false,
    favorito: false,
    instagram: 1,
    yt: 1,
    filtrando: false,
    room: 0,
    
    init: function () {
      
        this.programarBottones();
        this.mostrarMultiSelect(function () {
            $('#multipleSelect').multipleSelect();
        });
        
        //this.mostrarMultiSelectCuentas(function () {
        //    $('#multipleSelectCuentas').multipleSelect();
        //});
        this.mostrarRooms();
        app.main.room = $("#selectRooms option:selected").val();
        app.ajax.getCMByRoom(app.main.room);
        app.ajax.damePosts_v2(app.main.dDate, app.main.hDate, app.main.pais,app.main.room , app.main.cuenta, app.main.fb, app.main.tw, app.main.instagram, app.main.yt, app.main.criterio, app.main.ascendente, app.main.favorito, app.main.paginaActual);
    },
  
    mostrarMultiSelect: function (callback) {
        var $elemento = '<optgroup></optgroup>';
        var option = '<option ></option>';
        $("#multipleSelect").html("");
        //la variable global bindPaises contiene toda la informacion sobre paises que hay en bd
        for (var i = 0; i < bindPaises.length; i++) {
            var existe = $("#multipleSelect optgroup[label='" + bindPaises[i].agrupacion + "']");
            if (existe.length > 0) {
                //buscamos el label y metemos dentro el options
                var op = $(option).html(bindPaises[i].nombre).val(bindPaises[i].codigo);
                $(existe).append(op);
            } else {

                var bl = $($elemento).attr("label", bindPaises[i].agrupacion);
                bl = $(bl).html($(option).html(bindPaises[i].nombre).val(bindPaises[i].codigo));
                $("#multipleSelect").append(bl);
            }
        }
        if (!!callback) {
            callback();
        }
    },
    //mostrarMultiSelectCuentas: function (callback) {

    //    var option = '<option ></option>';
    //    $("#multipleSelectCuentas").html("");
    //    //la variable global bindPaises contiene toda la informacion sobre paises que hay en bd
    //    for (var i = 0; i < bindCuentas.length; i++) {
    //        $("#multipleSelectCuentas").append($(option).html(bindCuentas[i].nickname).val(bindCuentas[i].id));
    //    }
    //    if (!!callback) {
    //        callback();
    //    }
    //},

    // Al igual que obtenemos los datos de los correspondientes paises y de las correspondientes cuentas también tendremos que obtener los datos de las homes disponibles por el usuario
    // TODO
    mostrarRooms: function () {

        var option = '<option ></option>';
        $("#selectRooms").html("");
      
            for (var i = 0; i < bindRooms.length; i++) {
                $("#selectRooms").append($(option).html(bindRooms[i].nombre).val(bindRooms[i].id_room).attr('id', bindRooms[i].id_room));
            }       
    },

    // Funcion que programa la utilidad de los botones 
    programarBottones: function () {
        // Clone de los items de RRSS ?¿
        app.main.boxPostTwitter = $(".twitter-item").clone();
        app.main.boxPostFacebook = $(".fb-item").clone();
        app.main.boxPostInstagram = $(".insta-item").clone();
        app.main.boxPostYoutube = $(".yt-item").clone();

        // Scroll página
        $(window).scroll(function () {

            if ((($(window).scrollTop() + $(window).height()) / $(document).height()) > 0.95) {
                if (!app.main.pidiendoPagina) {
                    app.main.paginaActual += 1;
                    app.ajax.damePosts_v2(app.main.dDate, app.main.hDate, app.main.pais,app.main.room, app.main.cuenta, app.main.fb, app.main.tw, app.main.instagram, app.main.yt, app.main.criterio, app.main.ascendente, app.main.favorito, app.main.paginaActual);
                } else {

                }
            }
        });
        //Se le añade algo de css al label del sort
        $(".sortArrows label").css("cursor", "default");
        //Borro todos los eventos que tuviera el id sort, y añado el evento on change
        $("#sort").off().on("change", function () {
           // Desactivo las flechas
            $(".sortArrows").removeClass("active");
            // Le digo que no sea ascendente
            app.main.ascendente = false;
            // Añado si el valor no es 0 (none selected) la actividad a las flechas
            if ($("#sort option:selected").val() != "0") {
                $($(".sortArrows")[0]).addClass("active");
                $(".sortArrows label").css("cursor", "pointer");
            } else {
                $(".sortArrows label").css("cursor", "default");
            }
        });
    
        // Funcionalidad de las flechas de ordenacion
       
        $(".sortArrows").on("click", function (e) {
            $(".sortArrows").removeClass("active");
            if($( "#sort option:selected" ).val() != "0"){
               $(this).addClass("active");
                var asc = $(this).attr("data-sort");
                app.main.ascendente = ( asc ==1 ); 
            }
        });
        
        // Aquí haremos lo nuevo que tengamos que hacer -> TODO
        // Opcion : Llamar a algo como lo que hacemos en el goButton
        $("#selectRooms").change(function(){
            app.main.room = $("#selectRooms option:selected").val();
            app.ajax.damePosts_v2(app.main.dDate, app.main.hDate, app.main.pais, app.main.room, app.main.cuenta, app.main.fb, app.main.tw, app.main.instagram, app.main.yt, app.main.criterio, app.main.ascendente, app.main.favorito, app.main.paginaActual);
            app.ajax.getCMByRoom(app.main.room);
        });

        // Funcionalidad de los botones de las redes sociales
        $(".rrssFilters button").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            // SI le clicamos y estaba apagado lo enciende y viceversa.
            if (!$(this).hasClass("off")) {
                $(this).addClass("off");
                var index = $(".rrssFilters button").index(this);
                $(this).attr("data-rs", index + 1);
            } else {
                $(this).removeClass("off");
            }

        });
        //Nose que es lo que modifica
        $("#language li").on("click", function (e) {
            $("#language li").removeClass("active");
            $(this).addClass("active");
            var text = $(this).html();
            $(".btnFiltro").html("");
            $(".btnFiltro").html('<i class="fa fa-sort-desc animation"></i>' + text);
            $("#language").collapse('hide');
        });

        // Cuando clicamos en el favorito ponemos el corazon y lo activamos
        $(document).on("click", ".fav", function (e) {
            e.preventDefault();
            e.stopPropagation();
            var favorito;
            if ($(this).hasClass("activeFavorito")) {
                $(this).removeClass("activeFavorito");
                favorito = false;
            } else {
                $(this).addClass("activeFavorito");
                favorito = true;
            }
            var idPost = $(this).parent().parent().parent().parent().attr("data-codpost");
            var idCM = $(this).parent().parent().parent().parent().attr("data-codcm");
            var RS = $(this).parent().parent().parent().parent().attr("data-codrs");

            app.ajax.guardarFavorito(idPost, RS, idCM, favorito);
        });

        // El boton que nos lleva a nuestros favoritos
        $(".btnMisFavoritos").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            if ($(this).hasClass("favActivos")) {
                $(this).removeClass("favActivos");
                app.main.favorito = false;
            } else {
                $(this).addClass("favActivos");
                app.main.favorito = true;
            }
            app.main.paginaActual = 0;
            app.ajax.damePosts_v2(app.main.dDate, app.main.hDate, app.main.pais,app.main.room, app.main.cuenta, app.main.fb, app.main.tw, app.main.instagram, app.main.yt, app.main.criterio, app.main.ascendente, app.main.favorito, app.main.paginaActual);
        });
        // El boton buscar, con la funcionalidad de que nos hace una llamada para obtener los posts
        $(".goButton").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();


            var hasta = $(".inputHasta").val();
            var desde = $(".inputDesde").val();
            if ($(".fbFilter").hasClass("off")) {
                app.main.fb = 0;
            } else {
                app.main.fb = 1;
            }

            if ($(".twFilter").hasClass("off")) {
                app.main.tw = 0;
            } else {
                app.main.tw = 1;
            }

            if ($(".insFilter").hasClass("off")) {
                app.main.instagram = 0;
            } else {
                app.main.instagram = 1;
            }

            if ($(".ytFilter").hasClass("off")) {
                app.main.yt = 0;
            } else {
                app.main.yt = 1;
            }
            var hayPais = $(".filtroPais").find(".selected");

            app.main.pais = "";
            if (hayPais.length > 0) {
                $(".filtroPais").find(".selected").each(function () {
                    var index = $(this).find("input").val();

                    app.main.pais = app.main.pais + "#" + index;
                    console.log("app.main.pais " + app.main.pais);
                });


            }

            var hayCuenta = $(".filtroCuentas").find(".selected");

            app.main.cuenta = "";
            if (hayCuenta.length > 0) {
                $(".filtroCuentas").find(".selected").each(function () {
                    var index = $(this).find("input").val();

                    app.main.cuenta = app.main.cuenta + "#" + index;
                    console.log("app.main.cuenta " + app.main.cuenta);
                });


            }

            var d = desde.split("/");
            var h = hasta.split("/");

            if (desde != "") {
                app.main.dDate = new Date(d[2], d[1] - 1, d[0]);
            }

            if (hasta != "") {
                app.main.hDate = new Date(h[2], h[1] - 1, h[0]);
            }


            if (app.main.dDate == "" && app.main.hDate == "" && app.main.pais == "" && app.main.fb == 1 && app.main.tw == 1 && app.main.instagram == 1) {
                app.main.filtrando = false;
            } else {
                app.main.filtrando = true;
            }
           
            app.main.criterio = parseInt($("#sort option:selected").val());
            app.main.paginaActual = 0;
            //app.ajax.guardarIdRoom(app.main.room);
            app.ajax.damePosts_v2(app.main.dDate, app.main.hDate, app.main.pais, app.main.room, app.main.cuenta, app.main.fb, app.main.tw, app.main.instagram, app.main.yt, app.main.criterio, app.main.ascendente, app.main.favorito, app.main.paginaActual);

        });

    }

};
app.navegacion = {};
app.utils = {
    trace: function () {
        try {
            for (var i = 0; i < arguments.length; i++)
                console.log(arguments[i]);
        } catch (err) {
        }
    },
    isValidDate: function (dateString) {
        // First check for the pattern
        if (!/^\d{1,2}\/\d{1,2}\/\d{4}$/.test(dateString))
            return false;

        // Parse the date parts to integers
        var parts = dateString.split("/");
        var day = parseInt(parts[0], 10);
        var month = parseInt(parts[1], 10);
        var year = parseInt(parts[2], 10);

        // Check the ranges of month and year
        if (year < 1000 || year > 3000 || month == 0 || month > 12)
            return false;

        var monthLength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

        // Adjust for leap years
        if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
            monthLength[1] = 29;

        // Check the range of the day
        return day > 0 && day <= monthLength[month - 1];
    },
    numberWithCommas: function (x) {
        return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".");
    }

};
app.ajax = {
    webService: function (methodWS, datos, success, error) {

        $.ajax({
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify(datos),
            url: app.main.site_URL + methodWS,
            success: function (data) {
                if (data.d.resultado.toLowerCase() == "ok") {
                    if (!!success) {
                        success(data);
                    }
                } else {
                    if (!!error) {
                        error(data);
                    }
                }
            },
            error: function (e) {
            }
        });
    },
    damePosts: function (fechaInicio, fechaFin, pais, tipoRS, pagina) {
        app.main.pidiendoPagina = true;
        $(".loadingBottom").show();
        var datos = { "fechaInicio": fechaInicio, "fechaFin": fechaFin, "pais": pais, "tipoRS": tipoRS, "pagina": pagina };
        app.ajax.webService("damePosts", datos,
                function (data) {
                    $(".loadingBottom").hide();
                    if (data.d.data.length == app.main.numPostsPagina) {
                        app.main.pidiendoPagina = false;
                    }

                    app.visual.mostrarPost(data.d.data);
                    if (pagina == 0 && data.d.data.length == 0) {
                        $('#grid').html("<p class='txtGrid'>Sorry, for the moment there are no posts!</p>");
                    }
                },
                function (data) {
                    //error
                    $('#grid').html("<p class='txtGrid'>Sorry,an error has occurred ... Try again</p>");
                    $(".loadingBottom").hide();
                });
    },
    guardarFavorito: function (codigoPost, codigoRS, codigoCM, favorito) {
        var datos = { "codigoPost": codigoPost, "codigoRS": codigoRS, "codigoCM": codigoCM, "favorito": favorito };
        app.ajax.webService("guardarFavorito", datos,
                function (data) {

                },
                function (data) {
                    //error

                });
    },
    getCMByRoom: function (id_room) {
        var datos = { "id_room": id_room };
        app.ajax.webService("getCMByRoom", datos,
                function (data) {
                    console.log(data.d.resultado);
                    var option = '<option ></option>';
                    //$('#multipleSelectCuentas').multipleSelect();
                        $("#multipleSelectCuentas").html("");
                        //la variable global bindPaises contiene toda la informacion sobre paises que hay en bd
                        for (var i = 0; i < data.d.data.length; i++) {
                            $("#multipleSelectCuentas").append($(option).html(data.d.data[i].nickname).val(data.d.data[i].id));
                        }
                        
                            $('#multipleSelectCuentas').multipleSelect();
                        
                },
                function (data) {
                    //error

                });
    }, getCMByRoomComplete: function (id_room) {
        var datos = { "id_room": id_room };
        app.ajax.webService("getCMByRoomComplete", datos,
                function (data) {
                    console.log(data.d.resultado);

                    //var option = '<option ></option>';
                    ////$('#multipleSelectCuentas').multipleSelect();
                    //$("#multipleSelectCuentas").html("");
                    ////la variable global bindPaises contiene toda la informacion sobre paises que hay en bd
                    //for (var i = 0; i < data.d.data.length; i++) {
                    //    $("#multipleSelectCuentas").append($(option).html(data.d.data[i].nickname).val(data.d.data[i].id));
                    //}

                    //$('#multipleSelectCuentas').multipleSelect();

                },
                function (data) {
                    //error

                });
    },


    damePosts_v2: function (fechaInicio, fechaFin, pais,room, cuenta, fb, tw, instagram, yt, criterioOrden, ascendente, favorito, pagina) {
        app.main.pidiendoPagina = true;
        $(".loadingBottom").show();
        var datos = { "fechaInicio": fechaInicio, "fechaFin": fechaFin, "pais": pais,"room":room, "cuenta": cuenta, "fb": fb, "tw": tw, "instagram": instagram, "yt": yt, "criterioOrden": criterioOrden, "ascendente": ascendente, "favorito": favorito, "pagina": pagina };
        app.ajax.webService("damePosts_v2", datos,
                function (data) {

                    $(".loadingBottom").hide();
                    if (data.d.data.length == app.main.numPostsPagina) {
                        app.main.pidiendoPagina = false;
                    }

                    app.visual.mostrarPost(data.d.data);
                    if (pagina == 0 && data.d.data.length == 0) {
                        $('#grid').html("<p class='txtGrid'>Sorry, for the moment there are no posts!</p>");
                    }
                },
                function (data) {
                    //error
                    $('#grid').html("<p class='txtGrid'>" + data.d.data.mensaje + "</p>");
                    $(".loadingBottom").hide();
                });
    }

};
app.visual = {
    mostrarPost: function (arrayPosts) {
        var arrElem = [];
        if (app.main.paginaActual == 0) {
            $('#grid').html("");
            $(".grid").masonry("destroy");
        }

        var len = arrayPosts.length;
        var $elemento = "";
        var containerTemp = $("<span></span>");
        var max = relevanciaMax;
        var min = relevanciaMin;
        var diferencia = max - min;
        var parte = diferencia / app.main.numPartes;
        var p1 = parseInt(min + parte);
        var p2 = parseInt(p1 + parte);
        if ($(".filtroPais").find(".open").length > 0) {
            $(".ms-choice").click();
        }

        if (len > 0) {


            for (var i = 0; i < len; i++) {
                var relevancia = arrayPosts[i].relevancia;
                var eficaciaRelevancia = arrayPosts[i].eficaciaRelevancia;
                if (arrayPosts[i].codigoRS == 1) {
                    $elemento = $(app.main.boxPostFacebook).clone();
                    if (arrayPosts[i].tipoPost == "video") {
                        $($elemento).find(".containerVideo img").attr("src", arrayPosts[i].multimedia);
                        $($elemento).find(".containerVideo a").attr("href", arrayPosts[i].urlDirecta);
                        $($elemento).find(".containerVideo").show();
                        $($elemento).find(".containerImg").hide();
                        $($elemento).find(".containerLink").hide();
                    } else if (arrayPosts[i].tipoPost == "link") {
                        $($elemento).find(".containerLink a").attr("href", arrayPosts[i].urlDirecta).show();
                        $($elemento).find(".containerLink i").after("<span>" + arrayPosts[i].urlDirecta + "</span>").show();
                        $($elemento).find(".containerVideo").hide();
                        $($elemento).find(".containerImg").hide();
                    } else if (arrayPosts[i].tipoPost == "photo") {
                        $($elemento).find(".containerImg img").attr("src", arrayPosts[i].multimedia);
                        $($elemento).find(".containerImg").show();
                        $($elemento).find(".containerVideo").hide();
                        $($elemento).find(".containerLink").hide();
                        $($elemento).find(".open-image").attr("href", arrayPosts[i].multimedia);
                    } else {
                        $($elemento).find(".containerVideo").hide();
                        $($elemento).find(".containerLink").hide();
                        $($elemento).find(".containerImg").hide();
                    }

                } else if (arrayPosts[i].codigoRS == 2) {
                    $elemento = $(app.main.boxPostTwitter).clone();
                    if (arrayPosts[i].tipoPost == "video") {
                        $($elemento).find(".containerVideo a").attr("href", arrayPosts[i].urlDirecta);
                        $($elemento).find(".containerVideo").show();
                        $($elemento).find(".containerImg").hide();
                        $($elemento).find(".containerLink").hide();
                    } else if (arrayPosts[i].tipoPost == "photo" || arrayPosts[i].tipoPost == "image") {
                        $($elemento).find(".containerVideo").hide();
                        $($elemento).find(".containerLink").hide();
                        $($elemento).find(".containerImg img").attr("src", arrayPosts[i].multimedia);
                        $($elemento).find(".containerImg").show();
                        $($elemento).find(".open-image").attr("href", arrayPosts[i].multimedia);
                    } else {
                        $($elemento).find(".containerVideo").hide();
                        $($elemento).find(".containerLink").hide();
                        $($elemento).find(".containerImg").hide();
                    }

                } else if ((arrayPosts[i].codigoRS == 3)) {
                    $elemento = $(app.main.boxPostInstagram).clone();
                    if (arrayPosts[i].tipoPost == "video") {
                        $($elemento).find(".containerVideo img").attr("src", arrayPosts[i].multimedia);
                        $($elemento).find(".containerVideo a").attr("href", arrayPosts[i].urlDirecta);
                        $($elemento).find(".containerVideo").show();
                        $($elemento).find(".containerImg").hide();
                        $($elemento).find(".containerLink").hide();

                    } else if (arrayPosts[i].tipoPost == "photo" || arrayPosts[i].tipoPost == "image") {
                        $($elemento).find(".containerImg img").attr("src", arrayPosts[i].multimedia);
                        $($elemento).find(".containerImg").show();
                        $($elemento).find(".containerVideo").hide();
                        $($elemento).find(".containerLink").hide();
                        $($elemento).find(".open-image").attr("href", arrayPosts[i].multimedia);
                    } else {
                        $($elemento).find(".containerVideo").hide();
                        $($elemento).find(".containerLink").hide();
                        $($elemento).find(".containerImg").hide();
                    }

                } else {

                    $elemento = $(app.main.boxPostYoutube).clone();
                    $($elemento).find(".containerVideo img").attr("src", arrayPosts[i].multimedia);
                    $($elemento).find(".containerVideo a").attr("href", arrayPosts[i].urlDirecta);
                    $($elemento).find(".containerVideo").show();
                    $($elemento).find(".containerImg").hide();
                    $($elemento).find(".containerLink").hide();

                }
                if (arrayPosts[i].urlDirecta != "" && arrayPosts[i].urlDirecta != null) {
                    $($elemento).find(".gotoSource").attr("href", arrayPosts[i].urlDirecta).show();
                } else {
                    $($elemento).find(".gotoSource").hide();
                }


                if (arrayPosts[i].favorito) {
                    $($elemento).find(".fav").addClass("activeFavorito");
                }

                $($elemento).attr("data-codPost", arrayPosts[i].codigoPost);
                $($elemento).attr("data-codCM", arrayPosts[i].codigoCM);
                $($elemento).attr("data-codRS", arrayPosts[i].codigoRS);
                $($elemento).find(".userImage").css("background-image", arrayPosts[i].imageCM);
                $($elemento).find(".userName").html(arrayPosts[i].nombreCM);
                $($elemento).find(".userNick").html(arrayPosts[i].localizacion);
                if (arrayPosts[i].texto != "" || arrayPosts[i].texto != null) {
                    var txt = arrayPosts[i].texto.parseURL().parseUsernameTW().parseHashtagTW();
                    $($elemento).find(".containerTexto").html(txt).show();
                } else {
                    $($elemento).find(".containerTexto").hide();
                }

                if (arrayPosts[i].fecha != "" || arrayPosts[i].fecha != null) {
                    $($elemento).find(".fechaPost").html(arrayPosts[i].fecha).show();
                } else {
                    $($elemento).find(".fechaPost").hide();
                }


                var numA = arrayPosts[i].amigosCM.toString();
                var numAmigSeparado = app.utils.numberWithCommas(numA);

                if (arrayPosts[i].likes == "") {
                    $($elemento).find(".numLike span").html("0");
                } else {
                    $($elemento).find(".numLike span").html(arrayPosts[i].likes);
                }

                if (arrayPosts[i].comentarios == "") {
                    $($elemento).find(".numComment span").html("0");
                } else {
                    $($elemento).find(".numComment span").html(arrayPosts[i].comentarios);
                }


                if (arrayPosts[i].sharer == "") {
                    $($elemento).find(".numShare span").html("0");
                } else {
                    $($elemento).find(".numShare span").html(arrayPosts[i].sharer);
                }


                if (relevancia == "" || relevancia == null) {
                    relevancia = 0;
                    $($elemento).find(".ranking span").html(relevancia);

                } else {
                    $($elemento).find(".ranking span").html(relevancia);
                }

                $($elemento).find(".totalUsers span").html(numAmigSeparado);

                var strRelev = relevancia.toString();
                var relevanciaString = strRelev.replace(".", "");
                var relevanciaNew = parseInt(relevanciaString);
                if (relevanciaNew <= p1) {
                    //mas bajo gris
                    $($elemento).find(".ranking").addClass("score1");
                    $($elemento).find(".efectivity").addClass("score1");
                }
                else if (relevanciaNew < p2 && relevanciaNew > p1 || relevanciaNew === p2) {
                    console.log("medio");
                    //medio amarillo
                    $($elemento).find(".ranking").addClass("score2");
                    $($elemento).find(".efectivity").addClass("score2");
                } else {
                    console.log("mas");
                    //mas alto rojo
                    $($elemento).find(".ranking").addClass("score3");
                    $($elemento).find(".efectivity").addClass("score3");
                }
                if (arrayPosts[i].eficaciaRelevancia != "" && arrayPosts[i].eficaciaRelevancia != "-") {
                    $($elemento).find(".efectivity span").html(arrayPosts[i].eficaciaRelevancia + "%");
                }

                containerTemp.append($elemento);

            }

            var arrObjs = containerTemp.children();

            $('.grid').masonry()
                    .append(arrObjs)
                    .masonry('appended', arrObjs)
                    .masonry();

            $('.grid').imagesLoaded().progress(function () {
                $('.grid').masonry();
            });

            $('.grid').imagesLoaded(function () {
                $('.grid').masonry();
            });

        } else {
            //no hay productos para mostrar
        }





    }
};


// las funciones para parsear el texto 
String.prototype.parseURL = function () {
    return this.replace(/[A-Za-z]+:\/\/[A-Za-z0-9-_]+\.[A-Za-z0-9-_:%&~\?\/.=]+/g, function (url) {
        return $("<div>").append($("<a>").attr("href", url).attr('target', '_blank').addClass("linkPost").html(url)).html();
    });
};
String.prototype.parseUsernameTW = function () {
    return this.replace(/[@]+[A-Za-z0-9-_]+/g, function (u) {
        var username = u.replace("@", "");
        return $("<div>").append($("<a>").attr("href", "http://twitter.com/" + username).attr('target', '_blank').addClass("userPost").html(u)).html();

    });
};
String.prototype.parseHashtagTW = function () {
    return this.replace(/[#]+[A-Za-z0-9-_]+/g, function (t) {
        var tag = t.replace("#", "%23")
        return $("<div>").append($("<a>").attr("href", "https://twitter.com/search?q=" + tag).attr('target', '_blank').addClass("hashtagPost").html(t)).html();
    });
};
TEXTOS = {};