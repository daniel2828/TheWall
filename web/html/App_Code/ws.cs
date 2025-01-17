﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using Tweetinvi;
using Tweetinvi.Core.Extensions;
using Tweetinvi.Core.Interfaces.DTO;

[ServiceContract(Namespace = "")]
[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
public class ws
{
    // Para usar HTTP GET, agregue el atributo [WebGet]. (El valor predeterminado de ResponseFormat es WebMessageFormat.Json)
    // Para crear una operación que devuelva XML,
    //     agregue [WebGet(ResponseFormat=WebMessageFormat.Xml)]
    //     e incluya la siguiente línea en el cuerpo de la operación:
    //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
    [OperationContract]
    public void DoWork ()
    {
        // Agregue aquí la implementación de la operación
        return;
    }

    //[OperationContract]
    //public C_Responses damePosts (string fechaInicio, string fechaFin, string pais, int tipoRS, int pagina )
    //{
    //    C_Responses retorno = new C_Responses();
       
    //    try
    //    {

    //        List<C_post> lista = new List<C_post>();
    //        C_post proxy= new C_post();
    //        DataTable datos;
    //        retorno.resultado = "OK";
    //        retorno.data = lista;
    //        DateTime dFechaInicio;
    //        DateTime? dFechaInicioNullable;
    //        DateTime dFechaFin;
    //        DateTime? dFechaFinNullable;
    //        DateTime.TryParse(fechaInicio, out dFechaInicio);
    //        int? tipoRSNullable = null;
    //        if (tipoRS > 0)
    //        {
    //            tipoRSNullable = tipoRS;
    //        }
    //        if (dFechaInicio == DateTime.MinValue)
    //        {
    //            dFechaInicioNullable = null;
    //        }
    //        else {
    //            dFechaInicioNullable = dFechaInicio;
    //        }

    //        DateTime.TryParse(fechaFin, out dFechaFin);
    //        if (dFechaFin == DateTime.MinValue)
    //        {
    //            dFechaFinNullable = null;
    //        }
    //        else
    //        {
    //            dFechaFinNullable = dFechaFin;
    //        }


    //        datos = proxy.getPosts(tipoRSNullable, null, null, dFechaInicioNullable, dFechaFinNullable, pais, Convert.ToInt32(pagina), 20);
    //        if (datos.Rows.Count>0)
    //        {
    //            foreach (DataRow fila in datos.Rows)
    //            {
    //                C_post post = new C_post();
    //                post.codigoCM= fila["codigoCM"].ToString();
    //                post.codigoRS= Convert.ToInt32(fila["codigoRS"].ToString());
    //                post.codigoPost= fila["codigoPost"].ToString();
    //                post.fecha = Convert.ToDateTime(fila["fecha"]).ToShortDateString();
    //                post.texto= fila["texto"].ToString();
    //                post.multimedia= fila["multimedia"].ToString();
    //                post.urlDirecta= fila["urlDirecta"].ToString();
    //                post.relevancia= Convert.ToInt32(fila["relevancia"]).ToString("##,#");
    //                post.sharer = Convert.ToInt32(fila["sharer"]).ToString("##,#");
    //                post.likes = Convert.ToInt32(fila["likes"]).ToString("##,#");
    //                post.comentarios = Convert.ToInt32(fila["comentarios"]).ToString("##,#");
    //                post.amigosCM= Convert.ToInt32(fila["amigosCM"].ToString());
    //                post.ultimaActualizacion= fila["ultimaActualizacion"].ToString();
    //                post.localizacion= fila["localizacion"].ToString();
    //                post.tipoPost= fila["tipoPost"].ToString().Trim();
    //                post.nombreCM= fila["nombreCM"].ToString();
    //                post.nicknameCM= fila["nicknameCM"].ToString().Trim();
    //                if (post.amigosCM == 0)
    //                {
    //                    post.eficaciaRelevancia = "-";
    //                }
    //                else
    //                {
    //                    post.eficaciaRelevancia = (Convert.ToInt32(fila["relevancia"]) * 100 / post.amigosCM).ToString();
    //                }
                    
    //                lista.Add(post);


    //            }
    //            retorno.resultado="OK";
    //            retorno.data=lista;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        retorno.resultado = "KO";
    //        error objError = new error();
    //        objError.codigo="-100";
    //        objError.mensaje="Error de conexión. Cód Error:" + ex.Message;
    //        retorno.data=objError;
    //    }

    //    return retorno;
    //}
    [OperationContract]
    public C_Responses getTwiterToken(String username, String lastTwitt,bool isHashtag)
    {
        if (isHashtag)
        {
            // I put %23 because in twitter means #, so we put the username like hashtag
            username = "%23"+username;
        }
        
       
        C_Responses retorno = new C_Responses();
        retorno.resultado = "OK";
        try {
            var oAuthConsumerKey = "4V6BON6j4nzp5D1wvVf8NYeBv";
            var oAuthConsumerSecret = "Douy2Yon01wwclLSvfA0r06CBJlSlcmQWNUPKYW7lsSlXLEW8s";
            var oAuthUrl = "https://api.twitter.com/oauth2/token";
            var screenname = username;

            // Do the Authenticate
            var authHeaderFormat = "Basic {0}";

            var authHeader = string.Format(authHeaderFormat,
                Convert.ToBase64String(Encoding.UTF8.GetBytes(Uri.EscapeDataString(oAuthConsumerKey) + ":" +
                Uri.EscapeDataString((oAuthConsumerSecret)))
            ));

            var postBody = "grant_type=client_credentials";

            HttpWebRequest authRequest = (HttpWebRequest)WebRequest.Create(oAuthUrl);
            authRequest.Headers.Add("Authorization", authHeader);
            authRequest.Method = "POST";
            authRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            authRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (System.IO.Stream stream = authRequest.GetRequestStream())
            {
                byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                stream.Write(content, 0, content.Length);
            }

            authRequest.Headers.Add("Accept-Encoding", "gzip");

            WebResponse authResponse = authRequest.GetResponse();
            // deserialize into an object
            TwitAuthenticateResponse twitAuthResponse;
            using (authResponse)
            {
                using (var reader = new StreamReader(authResponse.GetResponseStream()))
                {
                    JavaScriptSerializer js = new JavaScriptSerializer();
                    var objectText = reader.ReadToEnd();
                    twitAuthResponse = JsonConvert.DeserializeObject<TwitAuthenticateResponse>(objectText);
                }
            }
            var timelineFormat="";
            // Do the timeline
            // I need to create a structure to get two different queries depends on the Hashtag or CM given.
            // I will separate both because one is a timeline an other is a personalized query
            if (isHashtag)
            {
                if (lastTwitt.Equals("0")) { timelineFormat = "https://api.twitter.com/1.1/search/tweets.json?q=" + username + "&count=100"; }
                else { timelineFormat = "https://api.twitter.com/1.1/search/tweets.json?q=" + username + "&count=100&max_id=" + lastTwitt; }
                   
            }
            else {
                //Int32.TryParse(value, out number);
                long n;
                bool isNumeric = Int64.TryParse(username,out n);
                if (isNumeric == true)
                {
                    if (lastTwitt.Equals("0"))
                    {
                        timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?user_id={0}&include_rts=1&exclude_replies=1&count=100";
                    }
                    else
                    {
                        timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?user_id={0}&include_rts=1&exclude_replies=1&count=100&max_id=" + lastTwitt;
                    }
                }
                else
                {
                    if (lastTwitt.Equals("0"))
                    {
                        timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&include_rts=1&exclude_replies=1&count=100";
                    }
                    else
                    {
                        timelineFormat = "https://api.twitter.com/1.1/statuses/user_timeline.json?screen_name={0}&include_rts=1&exclude_replies=1&count=100&max_id=" + lastTwitt;
                    }
                }
            }
           
        
            // I am not sure if i need to change this depends on the parameter given into the ajax call
            // I believe that I need to change it -> TODO
            var timelineUrl = string.Format(timelineFormat, screenname);
            HttpWebRequest timeLineRequest = (HttpWebRequest)WebRequest.Create(timelineUrl);
            var timelineHeaderFormat = "{0} {1}";
            timeLineRequest.Headers.Add("Authorization", string.Format(timelineHeaderFormat, twitAuthResponse.token_type, twitAuthResponse.access_token));
            timeLineRequest.Method = "Get";
            WebResponse timeLineResponse = timeLineRequest.GetResponse();
            var timeLineJson = string.Empty;
            using (timeLineResponse)
            {
                using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
                {
                    timeLineJson = reader.ReadToEnd();
                }
            }
            retorno.data = timeLineJson;




    
}

        catch (Exception ex)
        {
            retorno.resultado = "KO";
            error objError = new error();
            objError.codigo = "-100";
            objError.mensaje = "Error de conexión. Cód Error:" + ex.Message;
            retorno.data = objError;
        }
        return retorno;

    }

    public class TwitAuthenticateResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
    }
    private string EncodeCharacters(string v)
    {
        throw new NotImplementedException();
    }

  
    // Dame Post lo que hace es devolverte los post en función del criterio de selección directamente
    // TO DO --> El filtrar por favoritos parece pasar a true
    [OperationContract]
    public C_Responses damePosts_v2 ( string fechaInicio, string fechaFin, string pais,int room,string cuenta, int fb, int tw, int instagram, int yt, int criterioOrden, bool ascendente, bool favorito, int pagina )
    {
        C_Responses retorno = new C_Responses();

        try
        {

            // Creo un datatable para los paises que se utilizará para mandarlo al procedimiento (SP)
            DataTable tbPaises = new DataTable();
            DataColumn column2 = new DataColumn("entero");
            column2.DataType = typeof(String);
            column2.AllowDBNull = true;
            tbPaises.Columns.Add(column2);
            DataRow filaAux;


            if (pais.Trim()!="")
            {
                string[] paises = pais.Split('#');
                foreach (string p in paises)
                {
                    if (p.Trim()!="")
                    {
                        filaAux = tbPaises.NewRow();
                        filaAux["entero"] = Convert.ToInt32(p);
                        tbPaises.Rows.Add(filaAux);
                    }
                }


            }
            // Imitamos el comportamiento de los paises para las cuentas
            DataTable tbCuentas = new DataTable();
            DataColumn column3 = new DataColumn("entero");
            column3.DataType = typeof(String);
            column3.AllowDBNull = true;
            tbCuentas.Columns.Add(column3);
            DataRow filaAux2;


            if (cuenta.Trim() != "")
            {
                string[] cuentas = cuenta.Split('#');
                foreach (string p in cuentas)
                {
                    if (p.Trim() != "")
                    {
                        filaAux2 = tbCuentas.NewRow();
                        filaAux2["entero"] = p;
                        tbCuentas.Rows.Add(filaAux2);
                    }
                }


            }

            List<C_post> lista = new List<C_post>();
            C_post proxy = new C_post();
            DataTable datos;
            retorno.resultado = "OK";
            retorno.data = lista;
            DateTime dFechaInicio;
            DateTime? dFechaInicioNullable;
            DateTime dFechaFin;
            DateTime? dFechaFinNullable;
            DateTime.TryParse(fechaInicio, out dFechaInicio);

            if (dFechaInicio == DateTime.MinValue)
            {
                dFechaInicioNullable = null;
            }
            else
            {
                dFechaInicioNullable = dFechaInicio;
            }

            DateTime.TryParse(fechaFin, out dFechaFin);
            if (dFechaFin == DateTime.MinValue)
            {
                dFechaFinNullable = null;
            }
            else
            {
                dFechaFinNullable = dFechaFin;
            }


            datos = proxy.getPosts_v2(fb, tw, instagram, yt, dFechaInicioNullable, dFechaFinNullable, tbPaises,room,tbCuentas, criterioOrden, ascendente,favorito, Convert.ToInt32(pagina), 20);
            if (datos.Rows.Count > 0)
            {   
                foreach (DataRow fila in datos.Rows)
                {
                    System.Diagnostics.Debug.WriteLine(datos.Rows.Count);
                    C_post post = new C_post();
                    post.codigoCM = fila["codigoCM"].ToString();
                    post.codigoRS = Convert.ToInt32(fila["codigoRS"].ToString());
                    post.codigoPost = fila["codigoPost"].ToString();
                    post.fecha = Convert.ToDateTime(fila["fecha"]).ToString("dd/MM/yyyy HH:mm:ss");
                    post.texto = fila["texto"].ToString();
                    post.multimedia = fila["multimedia"].ToString();
                    post.urlDirecta = fila["urlDirecta"].ToString();
                    post.relevancia = Convert.ToInt32(fila["relevancia"]).ToString("##,#");
                    post.sharer = Convert.ToInt32(fila["sharer"]).ToString("##,#");
                    post.likes = Convert.ToInt32(fila["likes"]).ToString("##,#");
                    post.comentarios = Convert.ToInt32(fila["comentarios"]).ToString("##,#");
                    post.ultimaActualizacion = fila["ultimaActualizacion"].ToString();
                    post.localizacion = fila["localizacion"].ToString();
                    post.tipoPost = fila["tipoPost"].ToString().Trim();
                    post.nombreCM = fila["nombreCM"].ToString();
                    post.nicknameCM = fila["localizacion"].ToString().Trim();
                    post.amigosCM =Convert.ToInt32(fila["amigosCM"]);
                    post.eficaciaRelevancia =fila["eficaciaRelevancia"].ToString();
                    //if (post.amigosCM == 0)
                    //{
                    //    post.eficaciaRelevancia = "-";
                    //}
                    //else
                    //{
                    //    post.eficaciaRelevancia = (Convert.ToInt32(fila["relevancia"]) * 100 / post.amigosCM).ToString();
                    //}


                    //Si pregunta por todos los posts
                    if (favorito==false)
                    {
                        if (Convert.ToInt32(fila["favorito"])>0)
                        {
                            post.favorito =true;
                        }
                        else {
                            post.favorito =false;
                        }
                    }//Si pregunta solo por los favoritos
                    else
                    {
                        post.favorito =true;
                    }

                    lista.Add(post);


                }
                retorno.resultado = "OK";
                retorno.data = lista;
            }
        }
        catch (Exception ex)
        {
            retorno.resultado = "KO";
            error objError = new error();
            objError.codigo = "-100";
            objError.mensaje = "Error de conexión. Cód Error:" + ex.Message;
            retorno.data = objError;
        }

        return retorno;
    }




    [OperationContract]
    //  [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
    public C_Responses guardarActividad ( string post )
    {
     
        C_Responses retorno = new C_Responses();
        retorno.resultado="KO";
        int filasAfectadas=0;
        bool errorGuardar=false;
        try
        {

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            objPosts view = serializer.Deserialize<objPosts>(post);

            if (view.posts.Count>0)
            {
                DataTable listaObjetos = new DataTable();
                listaObjetos.Columns.Add("codigoRS", typeof(int));
                listaObjetos.Columns.Add("codigoPost", typeof(string));
                listaObjetos.Columns.Add("codigoCM", typeof(string));
                listaObjetos.Columns.Add("fecha", typeof(DateTime));
                listaObjetos.Columns.Add("texto", typeof(string));
                listaObjetos.Columns.Add("multimedia", typeof(string));
                listaObjetos.Columns.Add("urlDirecta", typeof(string));
                listaObjetos.Columns.Add("relevancia", typeof(int));
                listaObjetos.Columns.Add("sharer", typeof(int));
                listaObjetos.Columns.Add("likes", typeof(int));
                listaObjetos.Columns.Add("comentarios", typeof(int));
                listaObjetos.Columns.Add("amigosCM", typeof(int));
                listaObjetos.Columns.Add("tipoPost", typeof(string));
                listaObjetos.Columns.Add("nextPageToken", typeof(string));
                listaObjetos.Columns.Add("eficaciRelevancia", typeof(string));
                
                C_post proxy = new C_post();
                foreach (posts postGuardar in view.posts)
                {
                    DataRow DR = listaObjetos.NewRow();

                    DR["codigoRS"] = view.tipoRS;
                    DR["codigoPost"] = postGuardar.codigoPost;
                    DR["codigoCM"] = view.codigoCM;
                    DR["fecha"] = Convert.ToDateTime(postGuardar.fecha);
                    DR["texto"] = postGuardar.texto;
                    DR["multimedia"] =postGuardar.multimedia;
                    DR["urlDirecta"] =  postGuardar.postUrl;
                    DR["relevancia"] = postGuardar.numSharer+postGuardar.numLikes+postGuardar.numComentarios;
                    DR["sharer"] = postGuardar.numSharer;
                    DR["likes"] = postGuardar.numLikes;
                    DR["comentarios"] = postGuardar.numComentarios;
                    DR["amigosCM"] = postGuardar.numAmigosCM;
                    DR["tipoPost"] = postGuardar.tipoPost;
                    DR["nextPageToken"] = postGuardar.nextPageToken;

                    //DR["eficaciRelevancia"] = postGuardar.nextPageToken;


                    if (postGuardar.numAmigosCM == 0)
                    {
                        DR["eficaciRelevancia"] = 0;
                    }
                    else
                    {
                        DR["eficaciRelevancia"] =(Convert.ToInt32(postGuardar.numSharer+postGuardar.numLikes+postGuardar.numComentarios) * 100 / postGuardar.numAmigosCM).ToString();
                    }


                    listaObjetos.Rows.Add(DR);

                   

                }
                filasAfectadas=proxy.guardarPost(listaObjetos);
                if (filasAfectadas<1)
                {
                    errorGuardar=true;

                }

                if (errorGuardar==true)
                {
                    error objError = new error();
                    objError.codigo="-1";
                    objError.mensaje="Ha ocurrido un error a la hora de guardar alguno de los post.";
                    retorno.data=objError;

                }
                else
                {
                    retorno.resultado="OK";
                }

            }//No se ha deserializado ningun objeto
            else
            {

                error objError = new error();
                objError.codigo="-1";
                objError.mensaje="No ha llegado ningún objeto.";
                retorno.data=objError;
            }



        }
        catch (Exception ex)
        {
            error objError = new error();
            objError.codigo="-100";

            objError.mensaje="Error de conexión. Cód Error:" + ex.Message;
            retorno.data=objError;

        }

        return retorno;
    }


    [OperationContract]
    public C_Responses dameTarea ( int tipoRS )
    {
        C_Responses retorno = new C_Responses();
        retorno.resultado="KO";
        try
        {

            C_comunityManager proxy= new C_comunityManager();
            string datos = proxy.dameTarea(tipoRS);

            if (datos.Trim() !="")
            {
                string[] commandArgs = datos.Split(new string[] { "###" }, StringSplitOptions.None);
              //  C_FuncionesGenerales.("ws.asmx -> datos -> " + datos).ToString();
                string codigoCM = commandArgs[0];
                string ultimoPost = commandArgs[1];
                bool isHastag = Convert.ToBoolean(commandArgs[2]);
                retorno.resultado="OK";
                C_Tarea tarea = new C_Tarea();
                tarea.codigoCM= codigoCM;
                tarea.ultimoPostId=ultimoPost.ToString();
                tarea.isHashtag = isHastag;
                retorno.data=tarea;
            }
            else //Si no hay nigun CM sin asignar en la red social solicitada
            {
                error objError = new error();
                objError.codigo="-1";
                objError.mensaje="All tasks are asigned.";
                retorno.data=objError;

            }
        }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
            //     throw new Exception("Error de conexión. Cód Error:" + ex.Message);
            error objError = new error();
            objError.codigo="-100";
            objError.mensaje="Error de conexión. Cód Error:" + ex.Message;
            retorno.data=objError;
        }

        return retorno;
    }

    [OperationContract]
    public C_Responses cerrarTarea ( int tipoRS, string codigoCM, int motivoCierre )
    {
        C_Responses retorno = new C_Responses();
        retorno.resultado="KO";
        try
        {
                                                                    
            C_comunityManager proxy= new C_comunityManager();
            int datos = proxy.cerrarTarea(tipoRS,codigoCM,motivoCierre);

            if (datos>0)
            {
                
                retorno.resultado="OK";
              
            }
            else //Si no ha cerrado la tarea
            {
                error objError = new error();
                objError.codigo="-1";
                objError.mensaje="No ha podido ser cerrada la tarea.";
                retorno.data=objError;

            }
        }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
            // throw new Exception("Error de conexión. Cód Error:" + ex.Message);
            error objError = new error();
            objError.codigo="-100";
            objError.mensaje="Error de conexión. Cód Error:" + ex.Message;
            retorno.data=objError;
        }

        return retorno;
    }

    [OperationContract]
    public C_Responses getListPaisesByAgrupacion ( string searchString, string agrupacion )
    {
        C_Responses retorno = new C_Responses();
        var lstData = new List<AutoCompleteResult>();
        retorno.resultado="KO";
        try
        {

            C_FuncionesGenerales proxy = new C_FuncionesGenerales();
            DataTable datos=proxy.getListPaisesByAgrupacion(searchString, Convert.ToInt32(agrupacion));
            if (datos.Rows.Count>0)
            {
                foreach (DataRow row in datos.Rows)
                {
                    //lstData.Add(new AutoCompleteResult { code = C_FuncionesGenerales.EncriptarCodigos(row["codigo"].ToString()), value = row["nombre"].ToString() });
                    lstData.Add(new AutoCompleteResult { code = row["codigo"].ToString(), value = row["nombre"].ToString() });
                }
                retorno.resultado="OK";
                retorno.data=lstData;
            }
          
        }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
            //     throw new Exception("Error de conexión. Cód Error:" + ex.Message);
            error objError = new error();
            objError.codigo="-100";
            objError.mensaje="Error de conexión. Cód Error:" + ex.Message;
            retorno.data=objError;
        }

        return retorno;
    }


    [OperationContract]
    public C_Responses guardarFavorito ( string codigoPost,int codigoRS,string codigoCM,bool favorito)
    {
        C_Responses retorno = new C_Responses();
        retorno.resultado="KO";
        try
        {

            C_post proxy= new C_post();
            int datos = proxy.guardarFavorito(codigoPost,codigoRS,codigoCM,favorito);

            if (datos>0)
            {
             retorno.resultado="OK";
            }
            else //Si no hay nigun CM sin asignar en la red social solicitada
            {
                error objError = new error();
                objError.codigo="-1";
                objError.mensaje="No se han podido guardar los cambios";
                retorno.data=objError;

            }
        }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
            //throw new Exception("Error de conexión. Cód Error:" + ex.Message);
            error objError = new error();
            objError.codigo="-100";
            objError.mensaje="Error de conexión. Cód Error:" + ex.Message;
            retorno.data=objError;
        }

        return retorno;
    }
    // Obtengo sólo el nombre y el id de los comunity managers para mostrarlos en el dropdawn list
    [OperationContract]
    public C_Responses getCMByRoom(int id_room)
    {
        C_Responses retorno = new C_Responses();
        retorno.resultado = "KO";
        try
        {
            retorno.resultado = "OK";
            C_comunityManager proxy = new C_comunityManager();
            DataTable datos = proxy.getCMByRoom(id_room);
          
            List<C_comunityManager> lista = new List<C_comunityManager>();
            
            if (datos.Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Rows)
                {
                    C_comunityManager cm = new C_comunityManager();
                    cm.id = Convert.ToInt32(fila["id"].ToString());
                    cm.nickname= fila["nickname"].ToString();
                    lista.Add(cm);

                }
            }
      

        retorno.resultado = "OK";
        retorno.data = lista;

    }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
            //     throw new Exception("Error de conexión. Cód Error:" + ex.Message);
            error objError = new error();
            objError.codigo = "-100";
            objError.mensaje = "Error de conexión. Cód Error:" + ex.Message;
            retorno.data = objError;
        }

        return retorno;
    }
    //Obtengo los datos que se pasaran en el panel de Admin
    [OperationContract]
    public C_Responses getCMByRoomComplete(int id_room)
    {
        C_Responses retorno = new C_Responses();
        retorno.resultado = "KO";
        try
        {
            retorno.resultado = "OK";
            C_comunityManager proxy = new C_comunityManager();
            DataTable datos = proxy.getCMByRoom(id_room);

            List<C_comunityManager> lista = new List<C_comunityManager>();

            if (datos.Rows.Count > 0)
            {
                foreach (DataRow fila in datos.Rows)
                {
                     // TODO ya está el WEBSERVICE PERO no se llama ni se tratan los datos en el front
    C_comunityManager cm = new C_comunityManager();
                    cm.id = Convert.ToInt32(fila["id"].ToString());
                    cm.nickname = fila["nickname"].ToString();
                    cm.codigoCM=fila["codigoCM"].ToString();
                    cm.numAmigos = fila["numAmigos"].ToString();
                    cm.codigoRS = Convert.ToInt32(fila["codigoRS"].ToString());
                    cm.nombre = fila["nombre"].ToString();
                    cm.localizacion = fila["localizacion"].ToString();
                    cm.activo = Convert.ToByte(fila["activo"].ToString());
                    cm.asignado = Convert.ToByte(fila["asignado"].ToString());
                    cm.contacto = fila["contacto"].ToString();
                    lista.Add(cm);

                }
            }


            retorno.resultado = "OK";
            retorno.data = lista;

        }
        catch (Exception ex)
        {
            //string codigoError = C_FuncionesGenerales.registroErrores("ws.asmx -> damePosts -> " + ex.Message).ToString();
            //     throw new Exception("Error de conexión. Cód Error:" + ex.Message);
            error objError = new error();
            objError.codigo = "-100";
            objError.mensaje = "Error de conexión. Cód Error:" + ex.Message;
            retorno.data = objError;
        }

        return retorno;
    }

    //[OperationContract]
    //public C_Responses getFavoritos ( string fechaInicio, string fechaFin, string pais, int fb, int tw, int instagram, int yt, int pagina )
    //{
    //    C_Responses retorno = new C_Responses();

    //    try
    //    {
    //        DataTable tbPaises = new DataTable();
    //        DataColumn column2 = new DataColumn("entero");
    //        column2.DataType = typeof(String);
    //        column2.AllowDBNull = true;
    //        tbPaises.Columns.Add(column2);
    //        DataRow filaAux;


    //        if (pais.Trim()!="")
    //        {
    //            string[] paises = pais.Split('#');
    //            foreach (string p in paises)
    //            {
    //                if (p.Trim()!="")
    //                {
    //                    filaAux = tbPaises.NewRow();
    //                    filaAux["entero"] = Convert.ToInt32(p);
    //                    tbPaises.Rows.Add(filaAux);
    //                }
    //            }


    //        }


    //        List<C_post> lista = new List<C_post>();
    //        C_post proxy = new C_post();
    //        DataTable datos;
    //        retorno.resultado = "OK";
    //        retorno.data = lista;
    //        DateTime dFechaInicio;
    //        DateTime? dFechaInicioNullable;
    //        DateTime dFechaFin;
    //        DateTime? dFechaFinNullable;
    //        DateTime.TryParse(fechaInicio, out dFechaInicio);

    //        if (dFechaInicio == DateTime.MinValue)
    //        {
    //            dFechaInicioNullable = null;
    //        }
    //        else
    //        {
    //            dFechaInicioNullable = dFechaInicio;
    //        }

    //        DateTime.TryParse(fechaFin, out dFechaFin);
    //        if (dFechaFin == DateTime.MinValue)
    //        {
    //            dFechaFinNullable = null;
    //        }
    //        else
    //        {
    //            dFechaFinNullable = dFechaFin;
    //        }


    //        datos = proxy.getPosts_v2(fb, tw, instagram, yt, dFechaInicioNullable, dFechaFinNullable, tbPaises, Convert.ToInt32(pagina), 20);
    //        if (datos.Rows.Count > 0)
    //        {
    //            foreach (DataRow fila in datos.Rows)
    //            {
    //                C_post post = new C_post();
    //                post.codigoCM = fila["codigoCM"].ToString();
    //                post.codigoRS = Convert.ToInt32(fila["codigoRS"].ToString());
    //                post.codigoPost = fila["codigoPost"].ToString();
    //                post.fecha = Convert.ToDateTime(fila["fecha"]).ToShortDateString();
    //                post.texto = fila["texto"].ToString();
    //                post.multimedia = fila["multimedia"].ToString();
    //                post.urlDirecta = fila["urlDirecta"].ToString();
    //                post.relevancia = Convert.ToInt32(fila["relevancia"]).ToString("##,#");
    //                post.sharer = Convert.ToInt32(fila["sharer"]).ToString("##,#");
    //                post.likes = Convert.ToInt32(fila["likes"]).ToString("##,#");
    //                post.comentarios = Convert.ToInt32(fila["comentarios"]).ToString("##,#");
    //                post.ultimaActualizacion = fila["ultimaActualizacion"].ToString();
    //                post.localizacion = fila["localizacion"].ToString();
    //                post.tipoPost = fila["tipoPost"].ToString().Trim();
    //                post.nombreCM = fila["nombreCM"].ToString();
    //                post.nicknameCM = fila["localizacion"].ToString().Trim();
    //                post.amigosCM =Convert.ToInt32(fila["amigosCM"]);
    //                if (post.amigosCM == 0)
    //                {
    //                    post.eficaciaRelevancia = "-";
    //                }
    //                else
    //                {
    //                    post.eficaciaRelevancia = (Convert.ToInt32(fila["relevancia"]) * 100 / post.amigosCM).ToString();
    //                }

    //                lista.Add(post);


    //            }
    //            retorno.resultado = "OK";
    //            retorno.data = lista;
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        retorno.resultado = "KO";
    //        error objError = new error();
    //        objError.codigo = "-100";
    //        objError.mensaje = "Error de conexión. Cód Error:" + ex.Message;
    //        retorno.data = objError;
    //    }

    //    return retorno;
    //}

}
