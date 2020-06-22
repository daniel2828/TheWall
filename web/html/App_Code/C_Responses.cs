using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Descripción breve de C_Response
/// </summary>
/// 

[DataContract]
[KnownType(typeof(C_post))]
[KnownType(typeof(C_comunityManager))]
[KnownType(typeof(List<C_post>))]
[KnownType(typeof(List<C_comunityManager>))]

[KnownType(typeof(List<posts>))]
[KnownType(typeof(posts))]
[KnownType(typeof(objPosts))]
[KnownType(typeof(C_Tarea))]
[KnownType(typeof(error))]
[KnownType(typeof(List<AutoCompleteResult>))]
public class C_Responses
{
	public C_Responses()
	{
		//
		// TODO: Agregar aquí la lógica del constructor
		//
	}
    [DataMember]
    public string resultado { get; set; }
    [DataMember]
    public object data { get; set; }


 
}
public class AutoCompleteResult
{
    public string code { get; set; }
    public string value { get; set; }
}

public class C_Tarea
{    
    public string codigoCM { get; set; }
    public string ultimoPostId { get; set; }
    public bool isHashtag { get; set; }
   
}




[DataContract]
[KnownType(typeof(List<posts>))]
[KnownType(typeof(posts))]
public class objPosts
{
    [DataMember]
    public string codigoCM { get; set; }
[DataMember]
    public int tipoRS { get; set; }
 [DataMember]
    public List<posts> posts { get; set; }
}


public class posts
{
    public string codigoPost { get; set; }
    public string texto { get; set; }
    public int numSharer { get; set; }

    public int numLikes { get; set; }
    public int numComentarios { get; set; }
    public string fecha { get; set; }
    public int numAmigosCM { get; set; }
    public string postUrl { get; set; }
    public string multimedia { get; set; }
    public string tipoPost { get; set; }
    public string nextPageToken { get; set; }
   
}

public class error
{
    public string codigo { get; set; }
    public string mensaje { get; set; }
   

}

