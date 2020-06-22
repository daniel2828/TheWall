
var app = app || {};

//NameSpace
app.main = {};
app.navegacion = {};
app.utils = {};
app.ajax = {};
app.visual = {};


//Main definitions
app.main = {
    site_URL: "/ws.svc/",
    actualRSfacebook: 1,
    actualRStwitter: 2,
    actualRSinstagram: 3,
    actualRSyoutube: 4,
    IDfacebookActualCM: null,
    IDtwitterActualCM: null,
    IDinstagramActualCM: null,
    IDYoutubeActualCM: null,
    numSegidoresFB: 0,
    numSubscriptoresYT: 0,
    numSuscriptIN: 0,
    activebtnFB: false,
    activebtnTW: false,
    activebtnIN: false,
    activebtnYT: false,
    tempArrayPosts: new Array(),
    tempArrayTwitts: new Array(),
    tempArrayInstagram: new Array(),
    tempArrayYoutTube: new Array(),
    motivoFail: 0,
    motivoExito: 1,
    motivoAbort: 2,
    motivoMaxPeticion: 3,
    responseTwitter: null,
    responseInstagram: null,
    countFB: 0,
    countTW: 0,
    countIN: 0,
    countYT: 0,
    esUltimoInstagram: false,
    userInstagram:"",
    //Keys
    ytApikey: "AIzaSyCrvFQeILHPjtK4NDV1kjYb4E0mG5jRt1c",
    clientid: '6f30cc005dec434e8ca58128efb4e389',
    // Init function called when DOM is loaded 
    init: function () {
        setTimeout(app.main.initYT, 1000);
        this.programarBottones();
    },
    // Buttons of the 4 RRSS , which are their functionality
    programarBottones: function () {
        // jQuery Selector on Click
        $(".btnConnectFB").on("click", function (e) {
            //Prevent that other elements inherit the properyies
            e.preventDefault();
            e.stopPropagation();
            var bloque = $(".btnConnectFB").parent();
            // If is connected and you click in, disconnect it
            if ($(this).parent().hasClass("connected")) {
                app.visual.mostrarStatusBtn(bloque, "alert", "Abort to traking the data");
                app.main.activebtnFB = false;
                app.ajax.guardarActividad(app.main.IDfacebookActualCM, app.main.actualRSfacebook, app.main.tempArrayPosts, function () {
                    app.ajax.cerrarTarea(app.main.actualRSfacebook, app.main.IDfacebookActualCM, app.main.motivoAbort);
                });
            }
                // Else, connect it
            else {
                app.main.activebtnFB = true;
                app.visual.mostrarStatusBtn(bloque, "on", TEXTOS.T01);
                app.main.loginFacebook();
            }
        });
        // Same for twitter
        $(".btnConnectTW").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            var bloque = $(".btnConnectTW").parent();

            if ($(this).parent().hasClass("connected")) {
                app.visual.mostrarStatusBtn(bloque, "alert", "Abort to traking the data");
                app.main.activebtnTW = false;
                app.ajax.guardarActividad(app.main.IDtwitterActualCM, app.main.actualRStwitter, app.main.tempArrayTwitts, function () {
                    app.ajax.cerrarTarea(app.main.actualRStwitter, app.main.IDtwitterActualCM, app.main.motivoAbort);
                });
            } else {
                app.main.activebtnTW = true;
                app.visual.mostrarStatusBtn(bloque, "on", TEXTOS.T01);
                app.visual.pedirTareasRS(app.main.actualRStwitter);
            }
        });
        // Same for Instagram
        $(".btnConnectIN").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();
            var bloque = $(".btnConnectIN").parent();
            if ($(this).parent().hasClass("connected")) {
                app.visual.mostrarStatusBtn(bloque, "alert", "Abort to traking the data");
                app.main.activebtnIN = false;
                app.ajax.guardarActividad(app.main.IDinstagramActualCM, app.main.actualRSinstagram, app.main.tempArrayInstagram, function () {
                    app.ajax.cerrarTarea(app.main.actualRSinstagram, app.main.IDinstagramActualCM, app.main.motivoAbort);

                });
            } else {
                app.main.activebtnIN = true;
                app.visual.mostrarStatusBtn(bloque, "on", TEXTOS.T01);
                app.visual.pedirTareasRS(app.main.actualRSinstagram);
            }
        });
        // Same for Youtube
        $(".btnConnectYT").on("click", function (e) {
            e.preventDefault();
            e.stopPropagation();

            var bloque = $(".btnConnectYT").parent();
            if ($(this).parent().hasClass("connected")) {
                app.visual.mostrarStatusBtn(bloque, "alert", "Abort to traking the data");
                app.main.activebtnYT = false;
                app.ajax.guardarActividad(app.main.IDYoutubeActualCM, app.main.actualRSyoutube, app.main.tempArrayYoutTube, function () {
                    app.ajax.cerrarTarea(app.main.actualRSyoutube, app.main.IDYoutubeActualCM, app.main.motivoAbort);

                });
            } else {
                app.main.activebtnYT = true;
                app.visual.mostrarStatusBtn(bloque, "on", TEXTOS.T01);
                app.visual.pedirTareasRS(app.main.actualRSyoutube);
            }
        });
        //################ Triggers #############
        $(".fbStatusIcon").on("click", function () {
            $(".btnConnectFB").trigger("click");
        });
        $(".twStatusIcon").on("click", function () {
            $(".btnConnectTW").trigger("click");
        });
        $(".inStatusIcon").on("click", function () {
            $(".btnConnectIN").trigger("click");
        });
        $(".ytStatusIcon ").on("click", function () {
            $(".btnConnectYT").trigger("click");
        });

    },
    //######################################
    //~########  FACEBOOK  #################
    //#####################################
    // Facebook Login
    loginFacebook: function () {
        // We load Facebook in popup.html
        FB.login(function () {
            app.main.getStatusLogin();
        },// The scope is what allows facebook do 
        { scope: 'publish_actions, user_likes' });
    },
    getStatusLogin: function () {
        //Facebook calls
        FB.getLoginStatus(function (response) {
            if (response.status === 'connected') {
                app.visual.pedirTareasRS(app.main.actualRSfacebook);
            } else if (response.status === 'not_authorized') {
                app.visual.mostrarStatusBtn($(".btnConnectFB").parent(), "alert", "not authorized to perform operation...");
            } else {
                app.visual.mostrarStatusBtn($(".btnConnectFB").parent(), "alert", "An error has occurred");
            }
        });
    },
    // Get the FB CM number of followers
    facebookGetNumSegidores: function (idCM, callback) {
        FB.api('/' + idCM + '?fields=likes', function (response) {
            if (!!callback) {
                callback(response);
            }
        });
    },
    // Create the URL of Facebook
    crearURlFacebook: function (idCM, tipoRS, lastID) {
        var url;
        if (lastID == "" || lastID == null || lastID == 0) {
            url = '/' + idCM + '/posts?fields=updated_time,created_time,message,comments.summary(true),likes.summary(true),link,full_picture,source,type,shares&limit=100';
        } else {
            url = '/' + idCM + '/posts?fields=updated_time,created_time,message,comments.summary(true),likes.summary(true),link,full_picture,source,type,shares&limit=100&until=' + lastID;
        }
        if (app.main.activebtnFB) {
            app.main.getDatos(url, idCM, tipoRS, lastID, false);
        }
    },
    // Get the Facebook data
    getDatos: function (url, idCM, tipoRS, lastID, paginado) {

        if (!paginado) {
            app.main.facebookGetNumSegidores(idCM, function (data) {
                if (data.error) {
                    app.ajax.cerrarTarea(1, idCM, app.main.motivoFail);
                } else {
                    app.main.numSegidoresFB = data.likes;
                    app.main.arastrarDatosFacebook(url, idCM, tipoRS, lastID);
                }
            });


        } else {
            app.main.arastrarDatosFacebook(url, idCM, tipoRS, lastID);
        }
    },
    tryWithNewURL:function(idCM, tipoRS, lastID){
        var url;
        if (lastID == "" || lastID == null || lastID == 0) {
            url = '/' + idCM + '/posts?fields=updated_time,created_time,message,comments.summary(true),likes.summary(true),link,full_picture,source,type,shares&limit=100';
        } else {
            url = '/' + idCM + '/posts?fields=updated_time,created_time,message,comments.summary(true),likes.summary(true),link,full_picture,source,type,shares&limit=100&until=' + lastID;
        }
        if (app.main.activebtnFB) {
            app.main.getDatos(url, idCM, tipoRS, lastID, false);
        }
    },
    // Management of Facebook data posts
    arastrarDatosFacebook: function (url, idCM, tipoRS, lastID) {
        FB.api(url, function (response) {
            if (!response || response.error) {
                if (response.error.code == 4) {
                    app.ajax.cerrarTarea(tipoRS, idCM, app.main.motivoMaxPeticion);
                    setTimeout(function () {
                        app.visual.pedirTareasRS(app.main.actualRSfacebook);
                    }, 600000);
                } else if (response.error = "unknown error") {
                    app.main.tryWithNewURL(idCM, tipoRS, lastID);
                } else {
                    app.ajax.cerrarTarea(tipoRS, idCM, app.main.motivoFail);
                }
            } else {
                // Temporally array to get post data
                app.main.tempArrayPosts = [];
                if (app.main.activebtnFB) {
                    $(".btnConnectFB").parent().find(".txtStatus").text(TEXTOS.T02);
                }

                var len = response.data.length;
                for (var i = 0; i < len; i++) {


                    app.main.countFB = app.main.countFB + 1;
                    $(".countFB").text(app.main.countFB);


                    var objPost = {
                        codigoPost: "",
                        texto: "",
                        numSharer: 0,
                        numLikes: 0,
                        numComentarios: 0,
                        fecha: "",
                        numAmigosCM: app.main.numSegidoresFB,
                        postUrl: "",
                        multimedia: "",
                        tipoPost: "",
                        nextPageToken: ""

                    };
                    if (!!response.data[i].message) {
                        objPost.texto = response.data[i].message;
                    }

                    if (!!response.data[i].shares) {
                        objPost.numSharer = parseInt(response.data[i].shares.count);
                    }

                    if (!!response.data[i].likes) {
                        objPost.numLikes = parseInt(response.data[i].likes.summary.total_count);
                    }

                    if (!!response.data[i].comments) {
                        objPost.numComentarios = parseInt(response.data[i].comments.summary.total_count);
                    }
                    if (response.data[i].type == "video") {
                        objPost.multimedia = response.data[i].full_picture;
                    } else if (response.data[i].type == "photo") {
                        objPost.multimedia = response.data[i].full_picture;
                    }
                    objPost.tipoPost = response.data[i].type;
                    objPost.codigoPost = response.data[i].id;
                    objPost.fecha = app.utils.convertirFecha(response.data[i].created_time);
                    var ids = objPost.codigoPost.split("_");
                    objPost.postUrl = "https://www.facebook.com/permalink.php?story_fbid=" + ids[1] + "&id=" + ids[0];
                    app.main.tempArrayPosts.push(objPost);
                }
                if (len != 0 || len != "") {
                    app.ajax.guardarActividad(idCM, parseInt(tipoRS), app.main.tempArrayPosts, function () {
                        // We take the next page of posts of the FB CM
                        var urlNextFull = response.paging.next;
                        var urlNext = urlNextFull.replace("https://graph.facebook.com/v2.5", "");
                        if (app.main.activebtnFB) {
                            app.main.getDatos(urlNext, idCM, tipoRS, lastID, true);
                        }

                    });
                } else {
                    // When there are no posts to save, we close the task
                    app.ajax.cerrarTarea(tipoRS, idCM, app.main.motivoExito);
                }


            }
        });
    },
    /* 
    ##################
         Twitter 
    ################## 
    */
    // Twitter first call comes when we click the button. Then we get data with ajax call and call this function.
    twitterSubmit: function (response, idCM, tipoRS, isHashtag) {
        app.main.tempArrayTwitts = [];
        if (app.main.activebtnTW) {
            $(".btnConnectTW").parent().find(".txtStatus").text(TEXTOS.T03);
        }
        // We need to make two different data manages if the response is from a CM or a Hashtag
        if (isHashtag) {
            var len = response.statuses.length;
            var lastTwitt = null;
            for (var i = 0; i < len; i++) {

                app.main.countTW = app.main.countTW + 1;
                $(".countTW").text(app.main.countTW);


                var objPost = {
                    codigoPost: "",
                    texto: "",
                    numSharer: 0,
                    numLikes: 0,
                    numComentarios: 0,
                    fecha: "",
                    numAmigosCM: 0,
                    postUrl: "",
                    multimedia: "",
                    tipoPost: "",
                    nextPageToken: ""

                };
                objPost.codigoPost = response.statuses[i].id;
                objPost.texto = response.statuses[i].text;
                objPost.numSharer = parseInt(response.statuses[i].retweet_count);
                objPost.numLikes = parseInt(response.statuses[i].favorite_count);
                objPost.fecha = app.utils.convertirFecha(response.statuses[i].created_at);
                objPost.numAmigosCM = parseInt(response.statuses[i].user.friends_count);
                var idP = response.statuses[i].id_str;

                if (!!response.statuses[i].entities.media) {
                    if (response.statuses[i].entities.media.length != 0) {

                        objPost.tipoPost = "photo";
                        objPost.multimedia = response.statuses[i].entities.media[0].media_url;
                    }
                } else {
                    if (!!response.statuses[i].entities.urls) {
                        if (response.statuses[i].entities.urls.length != 0) {
                            objPost.tipoPost = "video";
                            objPost.multimedia = response.statuses[i].entities.urls[0].url;
                        }
                    }
                }


                lastTwitt = response.statuses[i].id;
                app.main.tempArrayTwitts.push(objPost);
            }
            if (len == 0 || len == 1 || len == "") {
                //ya no hay twitts
                app.ajax.cerrarTarea(tipoRS, idCM, app.main.motivoExito);
            } else {
                //hay twitts para gurdar y se puede seguir recuperar el resto
                app.ajax.guardarActividad(idCM, parseInt(tipoRS), app.main.tempArrayTwitts, function () {
                    if (app.main.activebtnTW) {
                        app.ajax.datosTwitter(idCM, tipoRS, lastTwitt, isHashtag);
                    }
                });
            }
        }// If is a CM
        else {
            var len = response.length;
            var lastTwitt = null;
            for (var i = 0; i < len; i++) {

                app.main.countTW = app.main.countTW + 1;
                $(".countTW").text(app.main.countTW);


                var objPost = {
                    codigoPost: "",
                    texto: "",
                    numSharer: 0,
                    numLikes: 0,
                    numComentarios: 0,
                    fecha: "",
                    numAmigosCM: 0,
                    postUrl: "",
                    multimedia: "",
                    tipoPost: "",
                    nextPageToken: ""

                };
                objPost.codigoPost = response[i].id;
                objPost.texto = response[i].text;
                objPost.numSharer = parseInt(response[i].retweet_count);
                objPost.numLikes = parseInt(response[i].favorite_count);
                objPost.fecha = app.utils.convertirFecha(response[i].created_at);
                objPost.numAmigosCM = parseInt(response[i].user.friends_count);
                var idP = response.id_str;

                if (!!response[i].entities.media) {
                    if (response[i].entities.media.length != 0) {

                        objPost.tipoPost = "photo";
                        objPost.multimedia = response[i].entities.media[0].media_url;
                    }
                } else {
                    if (!!response[i].entities.urls) {
                        if (response[i].entities.urls.length != 0) {
                            objPost.tipoPost = "video";
                            objPost.multimedia = response[i].entities.urls[0].url;
                        }
                    }
                }


                lastTwitt = response[i].id;
                app.main.tempArrayTwitts.push(objPost);
            }

            // We finish
            if (len == 0 || len == 1 || len == "") {
                // When there are no more posts we close the task 
                app.ajax.cerrarTarea(tipoRS, idCM, app.main.motivoExito);
            } else {
                // Save the current posts in database
                app.ajax.guardarActividad(idCM, parseInt(tipoRS), app.main.tempArrayTwitts, function () {
                    if (app.main.activebtnTW) {
                        // Then we call to the same function
                        app.ajax.datosTwitter(idCM, tipoRS, lastTwitt, isHashtag);
                    }
                });
            }
        }


    }
,
    /* 
   ##################
        Instagram 
   ################## 
   */

  
    // Se ejecuta cuando cogemos un nuevo CM de la BBDD
    firstCallCMInstagram : function(idCM,lastID,isHashtag){

        if (isNaN(idCM)&&isHashtag==false) {
            app.main.userInstagram = idCM;
            app.ajax.getIdCMfromUsernameInstagram(idCM, function (newId) {
                app.main.getNumberseguidoresInstagram(newId, true, lastID, isHashtag, true);
                
            });
            
        }
        else {
            app.main.userInstagram = "";
            app.main.getNumberseguidoresInstagram(idCM, true, lastID, isHashtag, false);

            
        }
    },
    getNumberseguidoresInstagram: function (idCM,primeraPagina,lastID,isHashtag,isUser)
    {
        app.ajax.getNumSeguidoresInstagram(idCM, function (response) {

            if (!!response.data) {
                app.main.numSuscriptIN = response.data.counts.followed_by;
                app.main.instagramSubmit(idCM, primeraPagina, lastID, isHashtag, isUser);
            } else {

                app.ajax.cerrarTarea(3, idCM, app.main.motivoFail);

            }
        });
    },
    instagramSubmit: function (idCM, primeraPagina, lastID, isHashtag, isUser) {
        app.main.tempArrayInstagram = [];
        // If is the first page we take the number of followers of the account.
        var dataArr={};
        var dataArrHash={};
        //Different connection if is a hashtag or a CM
        try {
            if (isHashtag) {

                if (lastID != 0) {
                    dataArrHash = { client_id: app.main.clientid, count: 100, max_tag_id: lastID }
                } else {
                    dataArrHash = { client_id: app.main.clientid, count: 100 }
                }

                app.ajax.getDataByHashtagInstagram(idCM, dataArrHash, function (retorno) {
                app.main.getTotalDataInstagram(retorno, idCM, lastID, isHashtag, isUser);
                })
            }// Is a CM
            else {
                if (lastID != 0) {
                    dataArr = { client_id: app.main.clientid, count: 100, max_id: lastID }
                } else {
                    dataArr = { client_id: app.main.clientid, count: 100 }
                }
                app.ajax.getDataByIdInstagram(idCM,dataArr,function(retorno){
                    app.main.getTotalDataInstagram(retorno, idCM, lastID,isHashtag, isUser);
                });
               
            }
        } catch (e) {
            app.visual.mostrarStatusBtn($(".btnConnectIN").parent(), "alert", e);
            app.main.activebtnIN = false;
            app.ajax.guardarActividad(app.main.IDinstagramActualCM, app.main.actualRSinstagram, app.main.tempArrayInstagram, function () {
                app.ajax.cerrarTarea(app.main.actualRSinstagram, app.main.IDinstagramActualCM, app.main.motivoAbort);

            });
        }
    },
    // Manage instagram data
    getTotalDataInstagram: function (response, idCM, lastID,isHashtag,isUser) {
      

        if (app.main.activebtnIN) {
            $(".btnConnectIN").parent().find(".txtStatus").text(TEXTOS.T04);
        }
        //Create an array with data
        var len = response.data.length;
        for (var i = 0; i < len; i++) {


            app.main.countIN = app.main.countIN + 1;
            $(".countIN").text(app.main.countIN);

            var objPost = {
                codigoPost: "",
                texto: "",
                numSharer: 0,
                numLikes: 0,
                numComentarios: 0,
                fecha: "",
                numAmigosCM: app.main.numSuscriptIN,
                postUrl: "",
                multimedia: "",
                tipoPost: "",
                nextPageToken: ""


            };

            objPost.codigoPost = response.data[i].id;
            objPost.postUrl = response.data[i].link;
            var d = new Date(response.data[i].created_time * 1000);
            objPost.fecha = app.utils.convertirFecha(d);
            objPost.numLikes = parseInt(response.data[i].likes.count);
            objPost.numComentarios = parseInt(response.data[i].comments.count);
            objPost.tipoPost = response.data[i].type;

            if (response.data[i].caption != null) {
                if (!!response.data[i].caption.text) {
                    objPost.texto = response.data[i].caption.text;
                }
            }

            if (response.data[i].type == "video") {
                objPost.multimedia = response.data[i].images.standard_resolution.url;
            } else if (response.data[i].type == "image") {
                objPost.multimedia = response.data[i].images.standard_resolution.url;
            } 

            app.main.tempArrayInstagram.push(objPost);
            if (isHashtag == false) {
                lastID = response.data[i].id;
            } else {
                
                lastID = response.pagination.next_max_id;
                objPost.nextPageToken = lastID;
            }
        }
      

        if (len == 0 || len == 1 || len == "") {
            //No more posts
            if (isUser) { app.ajax.cerrarTarea(3, app.main.userInstagram, app.main.motivoExito); }
            else { app.ajax.cerrarTarea(3, idCM, app.main.motivoExito);}
           
        } else {
            // Save the posts
            var id="";
            if (isUser) {
                id = app.main.userInstagram;
            }
            else {
                id = idCM;
            }
            app.ajax.guardarActividad(id,parseInt(3), app.main.tempArrayInstagram, function () {
                if (app.main.esUltimoInstagram == true) {
                    app.ajax.cerrarTarea(3, idCM, app.main.motivoExito);
                }
                else if (app.main.activebtnIN) {
                    
                    app.main.instagramSubmit(idCM, false, lastID, isHashtag, isUser);
                } else {
                    alert("Instagram dejo de funcionar por errores en el distribuidor.");
                }
            });

        }

    },
    error: function (data2) {


        console.log("fail instagram");
        console.log(err);
        if (err.responseText.indexOf('429') != -1) {
            //	The maximum number of requests per hour has been exceeded.
           
            setTimeout(function () {
                app.ajax.cerrarTarea(3, idCM, app.main.motivoMaxPeticion);

                //despues de 10 min llamamos la function 
            }, 3600000);

        } else {
            //otro error
            app.ajax.cerrarTarea(3, idCM, app.main.motivoFail);
           
        }
        console.log(data2);

    },

    /* 
   ##################
        Youtube 
   ################## 

   */
    
    // Init youtube keys
    initYT: function () {
        gapi.client.setApiKey(app.main.ytApikey);
        gapi.client.load('youtube', 'v3');
    },

    // Get youtube data 
    getListChannels: function (idCM, queRS, lastPost, isHashtag) {
        try {
            app.main.numSubscriptoresYT = 0;
            //  Diference between hashtag and CM
            if (isHashtag) {
                // We don't need a playlist because is a Hashtag
                app.main.getPlaylistItems(idCM, queRS, "", lastPost);
            }
            else {
                // We need a playlist
                gapi.client.youtube.channels.list({
                    part: "contentDetails,statistics",
                    forUsername: idCM

                }).execute(function (response) {
                    //If it doesn't exist probably will be an id
                    if (response.items[0] == undefined) {

                        app.main.tryWithId(idCM, queRS, lastPost);
                    }//Si me devuelve datos
                    else {

                        var idPlaylist = response.items[0].contentDetails.relatedPlaylists.uploads;
                        app.main.numSubscriptoresYT = response.items[0].statistics.subscriberCount;
                        app.main.getPlaylistItems(idCM, queRS, idPlaylist, lastPost);
                    }
                });
            }
        } catch (e) {
            app.main.getListChannels(idCM, queRS, lastPost, isHashtag)
        }


    },
    tryWithId: function (idCM, queRS, lastPost) {

        gapi.client.youtube.channels.list({
            part: "contentDetails,statistics",
            id: idCM

        }).execute(function (response) {
            
            if (response.items[0] == undefined) {
                app.ajax.cerrarTarea(queRS, idCM, app.main.motivoFail);
            }//Si me devuelve datos
            else {

                var idPlaylist = response.items[0].contentDetails.relatedPlaylists.uploads;
                app.main.numSubscriptoresYT = response.items[0].statistics.subscriberCount;
                app.main.getPlaylistItems(idCM, queRS, idPlaylist, lastPost);
            }
        });

    },
    //Here we get the video id which we need to get the details
    getPlaylistItems: function (idCM, queRS, idPlaylist, pagToken) {

        var options;
        // If playlist is "" it means that is a Hashtag
        if (idPlaylist == "") {
            if (!!pagToken && pagToken != 0 && pagToken != "") {
                options = {
                    part: "snippet",
                    q: idCM,
                    maxResults: 50,
                    pageToken: pagToken,
                    type: "video"
                };
            } else {
                options = {
                    part: "snippet",
                    q: idCM,
                    maxResults: 50,
                    type: "video"
                };
            }
            //We take the id of the videos
            gapi.client.youtube.search.list(options).execute(function (responseVideos) {


                var videos = responseVideos.items;

                app.main.tempArrayYoutTube = [];
                for (var i = 0; i < videos.length; i++) {

                    app.main.countYT = app.main.countYT + 1;
                    $(".countYT").text(app.main.countYT);


                    var idVideo = videos[i].id.videoId;
                    var esUltimoVideo = false;
                    var esFinal = false;

                    if (i + 1 == videos.length) {
                        // The last video
                        esUltimoVideo = true;
                    }
                    if (responseVideos.nextPageToken == null || responseVideos.nextPageToken == 'undefined') {
                        esFinal = true;
                    }
                    app.main.getVideoDetalles(idCM, queRS, idVideo, esUltimoVideo, esFinal, function () {

                        if (!!responseVideos.nextPageToken && responseVideos.nextPageToken != "") {


                            app.main.getPlaylistItems(idCM, queRS, "", responseVideos.nextPageToken);
                        }

                    });
                }


            })

        } // Get the video with playlist ID
        else {

            // If pagToken exists and is not empty we search the next page token
            if (!!pagToken && pagToken != 0 && pagToken != "") {
                options = {
                    part: "snippet",
                    playlistId: idPlaylist,
                    maxResults: 50,
                    pageToken: pagToken
                };
            } else {
                options = {
                    part: "snippet",
                    playlistId: idPlaylist,
                    maxResults: 50
                };
                //Execute the function to obtain video ids


            }
            gapi.client.youtube.playlistItems.list(options).execute(function (responseVideos) {

                var videos = responseVideos.items;
                app.main.tempArrayYoutTube = [];
                var idVideo;
                var esUltimoVideo;
                var esFinal;

                for (var i = 0; i < videos.length; i++) {

                    app.main.countYT = app.main.countYT + 1;
                    $(".countYT").text(app.main.countYT);
                    idVideo = videos[i].snippet.resourceId.videoId;
                    esUltimoVideo = false;
                    esFinal = false;
                    // Check if is the last video  
                    if (i + 1 == videos.length) {
                        esUltimoVideo = true;
                    }
                    if (responseVideos.nextPageToken == null || responseVideos.nextPageToken == 'undefined') {
                        esFinal = true;
                    }
                    //  For every video we call videoDetalles function 
                    app.main.getVideoDetalles(idCM, queRS, idVideo, esUltimoVideo, esFinal, responseVideos.nextPageToken, function () {
                        if (!!responseVideos.nextPageToken && responseVideos.nextPageToken != "") {
                            app.main.getPlaylistItems(idCM, queRS, idPlaylist, responseVideos.nextPageToken);
                        }

                    });
                }
            });

        }

    },
    //Get video details
    getVideoDetalles: function (idCM, queRS, idVideo, esUltimo, esFinal, pagToken, callback) {
        gapi.client.youtube.videos.list({
            part: "snippet,contentDetails,statistics",
            id: idVideo
        }).execute(function (responseDetails) {
            //    Response details are the details of the video
            if (responseDetails.items.length > 0) {

                var elem = responseDetails.items[0];
                if (app.main.activebtnYT) {
                    $(".btnConnectYT").parent().find(".txtStatus").text(TEXTOS.T05);
                }
                var objPost = {
                    codigoPost: "",
                    texto: "",
                    numSharer: 0,
                    numLikes: 0,
                    numComentarios: 0,
                    fecha: "",
                    numAmigosCM: app.main.numSubscriptoresYT,
                    postUrl: "",
                    multimedia: "",
                    tipoPost: "video",
                    nextPageToken: ""
                };

                objPost.texto = elem.snippet.title;
                objPost.numLikes = parseInt(elem.statistics.likeCount) + parseInt(elem.statistics.dislikeCount);
                objPost.numComentarios = parseInt(elem.statistics.commentCount);
                objPost.multimedia = elem.snippet.thumbnails.high.url;
                objPost.codigoPost = elem.id;
                objPost.fecha = app.utils.convertirFecha(elem.snippet.publishedAt);
                objPost.postUrl = "https://www.youtube.com/watch?v=" + objPost.codigoPost;
                //s   Where are we getting the token and saving it? 


                objPost.nextPageToken = pagToken;

                app.main.tempArrayYoutTube.push(objPost);

                if (esUltimo) {

                    app.ajax.guardarActividad(idCM, parseInt(queRS), app.main.tempArrayYoutTube, function () {
                        if (!!callback) {
                            callback();
                        }
                        if (esFinal) {
                            app.ajax.cerrarTarea(queRS, idCM, app.main.motivoExito);
                        }
                    });

                }
            }
        });
    }
};
/* 
   ##################
   Funciones que podremos utilizar a nivel global 
   ################## 
*/
app.navegacion = {};
app.utils = {
    trace: function () {
        try {
            for (var i = 0; i < arguments.length; i++)
                console.log(arguments[i]);
        } catch (err) {
        }
    }
    ,
    convertirFecha: function (fecha) {
        var mifecha = new Date(fecha);
        mifecha = mifecha.toLocaleDateString();
        return mifecha;
    }
};

/* 
   ##################
        Parte de llamadas ajax 
   ################## 
   */
app.ajax = {
    // Llamadas al webservice
    getIdCMfromUsernameInstagram: function(idCM,callback){
        var idRetorno=0;
        $.ajax({
            url: 'https://api.instagram.com/v1/users/search',
            dataType: 'jsonp',
            type: 'GET',
            data: { client_id: app.main.clientid, q: idCM },
            success: function (data) {
                if (!!data.data[0]) {
                    idRetorno = (data.data[0].id);
                    if (!!callback) {
                        callback(idRetorno);
                    }
                } else {
                    app.ajax.cerrarTarea(3, idCM, app.main.motivoFail);
                }
            },
            error: function (data) {
                app.ajax.cerrarTarea(3, idCM, app.main.motivoFail)
            }
        })
    },
    getNumSeguidoresInstagram: function (idCM, callback) {
        $.ajax({
            url: 'https://api.instagram.com/v1/users/' + idCM,
            dataType: 'jsonp',
            type: 'GET',
            data: { client_id: app.main.clientid, count: 100 },
            success: function (response) {
                if (!!callback) {
                    callback(response);
                }                
            },
            error: function (data) {
                alert("No se pudo saber el número de seguidores de " + idCM);
            }
        });
    },
    getDataByHashtagInstagram: function (idCM, dataArrHash, callback) {
        $.ajax({
            url: 'https://api.instagram.com/v1/tags/' + idCM + '/media/recent',
            dataType: 'jsonp',
            type: 'GET',
            data: dataArrHash,
            success: function (data) {
                // Get the data of instagram
                if (!!callback) {
                    callback(data);
                }
            },
            error: function (data) {
                console.log(data);
            }
        });
    },
    getDataByIdInstagram: function (idCM, dataArr, callback) {
        $.ajax({
            // Ponemos username, pero realmente sería UserID
            url: 'https://api.instagram.com/v1/users/' + idCM + '/media/recent',
            dataType: 'jsonp',
            type: 'GET',
            data: dataArr,
            success: function (response) {
                if (!!callback) { callback(response); };

            },
            error: function (data) {
                console.log(data);
            }
        });
    },
    webServiceStringify: function (methodWS, datos, success, error) {
        var dataParse = JSON.stringify(datos);
        var parsedData = JSON.stringify({ post: dataParse });
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: parsedData,
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
    webService: function (methodWS, datos, success, error) {
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
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
    //Pedimos tarea
    dameTarea: function (tipo, callback, error) {

        var datos = { "tipoRS": tipo };
        app.ajax.webService("dameTarea", datos,
                function (data) {
                    //success
                    if (!!callback) {
                        callback(data);
                    }

                    if (data.d.data.codigo == -1) {
                        //All tasks are asigned."
                        setTimeout(function () {
                            app.visual.pedirTareasRS(tipo);
                        }, 600000);
                    }
                },
                function (data) {
                    //error
                    if (!!error) {
                        error(data);
                    }
                });
    },
    //Get the twitter data
    datosTwitter: function (username, tipoRS, lastPost, isHashtag) {

        var datos = {
            "username": username,
            "lastTwitt": lastPost,
            "isHashtag": isHashtag
        };

        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(datos),
            url: app.main.site_URL + "getTwiterToken",
            success: function (data) {
                if (data.d.resultado.toLowerCase() == "ok") {
                    var objs = JSON.parse(data.d.data);
                    // Call the function to manage data
                    app.main.twitterSubmit(objs, username, tipoRS, isHashtag);
                } else {
                    app.ajax.cerrarTarea(2,username,app.main.motivoFail);
                    //app.visual.mostrarStatusBtn($(".btnConnectTW").parent(), "alert", data.d.data.mensaje);
                }
            },
            error: function (e) {

                app.ajax.cerrarTarea(2, username, app.main.motivoFail);
                //app.visual.mostrarStatusBtn($(".btnConnectTW").parent(), "alert", e);
            }
        });
    },
    // Save the activity  
    guardarActividad: function (IDperfilCM, tipoRS, arrPosts, callback) {
        var IDPerfilCModificado;
        if (!isNaN(IDperfilCM)) {
            IDPerfilCModificado = parseInt(IDperfilCM);
        } else {
            IDPerfilCModificado = IDperfilCM;
        }
        var datos = { "codigoCM": IDPerfilCModificado, "tipoRS": tipoRS, "posts": arrPosts };
        app.ajax.webServiceStringify("guardarActividad", datos,
                function (data) {
                    //success
                    app.utils.trace("ok guardar");


                    if (!!callback) {
                        callback();
                    }
                },
                function (data) {

                    app.ajax.cerrarTarea(tipoRS, IDperfilCM, app.main.motivoFail);
                    

                });
    },
  

    // Cambiar el motivoCierre
    cerrarTarea: function (tipo, codigoCM, motivoCiere) {
        var datos = { "tipoRS": tipo, "codigoCM": codigoCM, "motivoCierre": motivoCiere };
        app.ajax.webService("cerrarTarea", datos,function (data) {
                    var bloque;
                    switch (tipo) {
                        case 1:
                            bloque = $(".btnConnectFB").parent();
                            if (motivoCiere == 1||motivoCiere==0) {
                                app.visual.pedirTareasRS(app.main.actualRSfacebook);
                            }

                            break;
                        case 2:
                            bloque = $(".btnConnectTW").parent();
                            if (motivoCiere == 1 || motivoCiere == 0) {
                                app.visual.pedirTareasRS(app.main.actualRStwitter);
                            }
                            break;

                        case 3:
                            bloque = $(".btnConnectIN").parent();
                            if (motivoCiere == 1 || motivoCiere == 0) {
                                app.visual.pedirTareasRS(app.main.actualRSinstagram);
                            }
                            break;

                        case 4:
                            bloque = $(".btnConnectYT").parent();
                            if (motivoCiere == 1 || motivoCiere == 0) {
                                app.visual.pedirTareasRS(app.main.actualRSyoutube);
                            }
                            break;
                    }
                },
                function (data) {
                    alert("El Usuario " + codigoCM + " no pudo cerrarse");
                    console.log(data);
                });
    }

};
// Parte visual
app.visual = {
    mostrarStatusBtn: function (bloque, state, text) {
        switch (state) {
            // Estamos diciendole que al bloque le añada las clases de conectado o remueva las de conexion de alerta
            case "on":
                $(bloque).removeClass("connected alert");
                $(bloque).addClass("connected");
                break;
            case "off":
                $(bloque).removeClass("connected alert");
                break;

            case "alert":
                $(bloque).removeClass("connected alert");
                $(bloque).addClass("alert");
                break;
        }

        $(bloque).find(".txtStatus").text("");
        $(bloque).find(".txtStatus").text(text);

    },
    // We get the tasks of diferent RRSS here
    pedirTareasRS: function (queRS) {
        if (queRS == 1) {
            if (app.main.activebtnFB) {
                app.ajax.dameTarea(queRS,
                        function (data) {
                            //Get the facebook next CM and the last post.
                            app.main.IDfacebookActualCM = data.d.data.codigoCM;
                            var lastID = data.d.data.ultimoPostId;
                            app.main.crearURlFacebook(app.main.IDfacebookActualCM, queRS, lastID);


                        }, function (data) {
                            //ko del servidor
                            app.visual.mostrarStatusBtn($(".btnConnectFB").parent(), "alert", data.d.data.mensaje);
                        })
            }

        } else if (queRS == 2) {
            if (app.main.activebtnTW) {
                app.ajax.dameTarea(queRS,
                        function (data) {
                            // Take the CM data
                            app.main.IDtwitterActualCM = data.d.data.codigoCM;
                            var lastPost = data.d.data.ultimoPostId;
                            var isHashtag = data.d.data.isHashtag;

                            app.ajax.datosTwitter(app.main.IDtwitterActualCM, queRS, lastPost, isHashtag);
                        }, function (data) {
                            //Server error
                            app.visual.mostrarStatusBtn($(".btnConnectTW").parent(), "alert", data.d.data.mensaje);
                        }
                );
            }
        } else if (queRS == 3) {
            if (app.main.activebtnIN) {
                app.ajax.dameTarea(queRS, function (data) {
                    app.main.IDinstagramActualCM = data.d.data.codigoCM;
                    var lastID = data.d.data.ultimoPostId;
                    var isHashtag = data.d.data.isHashtag;
                  
                    app.main.firstCallCMInstagram(app.main.IDinstagramActualCM,lastID, isHashtag);

                }, function (data) {
                    //ko del servidor
                    app.visual.mostrarStatusBtn($(".btnConnectIN").parent(), "alert", data.d.data.mensaje);
                }
                );
            }
        } else {
            // Youtube task
            if (app.main.activebtnYT) {
                app.ajax.dameTarea(queRS, function (data) {
                    app.main.IDYoutubeActualCM = data.d.data.codigoCM;
                    var lastPost = data.d.data.ultimoPostId;
                    var isHashtag = data.d.data.isHashtag;
                    app.main.getListChannels(app.main.IDYoutubeActualCM, queRS, lastPost, isHashtag);

                }, function (data) {
                    //ko del servidor
                    app.visual.mostrarStatusBtn($(".btnConnectYT").parent(), "alert", data.d.data.mensaje);
                }
                );
            }
        }
    }
};


TEXTOS = {
    T01: "Connecting ... It may takes some time",
    T02: "Tracking data from facebook ...",
    T03: "Tracking data from twitter ...",
    T04: "Tracking data from instagram ...",
    T05: "Tracking data from youtube ..."

};
// Cuando carga el contenido del DOM , se llama a la funcion init()
document.addEventListener('DOMContentLoaded', function () {
    app.main.init();
});