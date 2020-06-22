<?php


// Creamos un proxi para la autentificación oauth 
// Creamos la configuracion
$config = array(
	'oauth_access_token' => '735288805-kNnV9ywMKGpZ7wAnKi7sOrmdIkmo0sy1spqGk5BK',
	'oauth_access_token_secret' => '9wieAaKKsPKavZZG3h53HC8i2A20kpl0noknKzQsq41wh',
	'consumer_key' => '4V6BON6j4nzp5D1wvVf8NYeBv',
	'consumer_secret' => 'Douy2Yon01wwclLSvfA0r06CBJlSlcmQWNUPKYW7lsSlXLEW8s',
	'use_whitelist' => false, // If you want to only allow some requests to use this script.
	'base_url' => 'https://api.twitter.com/1.1/'
);

// El whitelist por lo que he entendido es por si quieres añadir seguridad 
// Se cambia en las configuraciones poniendolo en true-> de momento lo dejamos a false
$whitelist = array(
	'statuses/user_timeline.json?screen_name=MikeRogers0&count=10&include_rts=false&exclude_replies=true'=>true
);

// Codigo necesario para descartar errores (tambien whitelist)
if(!isset($_GET['url'])){
	die('No URL set');
}

$url = $_GET['url'];


if($config['use_whitelist'] && !isset($whitelist[$url])){
	die('URL is not authorised');
}

// Configurancion de la URL pasada como codígo
$url_parts = parse_url($url);
parse_str($url_parts['query'], $url_arguments);

$full_url = $config['base_url'].$url; // Url with the query on it.
$base_url = $config['base_url'].$url_parts['path']; // Url without the query.

// Metodos de autorizacion de oauth

function buildBaseString($baseURI, $method, $params) {
	$r = array();
	ksort($params);
	foreach($params as $key=>$value){
	$r[] = "$key=" . rawurlencode($value);
	}
	return $method."&" . rawurlencode($baseURI) . '&' . rawurlencode(implode('&', $r));
}

function buildAuthorizationHeader($oauth) {
	$r = 'Authorization: OAuth ';
	$values = array();
	foreach($oauth as $key=>$value)
	$values[] = "$key=\"" . rawurlencode($value) . "\"";
	$r .= implode(', ', $values);
	return $r;
}

// Autorizando
$oauth = array(
	'oauth_consumer_key' => $config['consumer_key'],
	'oauth_nonce' => time(),
	'oauth_signature_method' => 'HMAC-SHA1',
	'oauth_token' => $config['oauth_access_token'],
	'oauth_timestamp' => time(),
	'oauth_version' => '1.0'
);
	
$base_info = buildBaseString($base_url, 'GET', array_merge($oauth, $url_arguments));
$composite_key = rawurlencode($config['consumer_secret']) . '&' . rawurlencode($config['oauth_access_token_secret']);
$oauth_signature = base64_encode(hash_hmac('sha1', $base_info, $composite_key, true));
$oauth['oauth_signature'] = $oauth_signature;

// Make Requests
$header = array(
	buildAuthorizationHeader($oauth), 
	'Expect:'
);
$options = array(
	CURLOPT_HTTPHEADER => $header,
	//CURLOPT_POSTFIELDS => $postfields,
	CURLOPT_HEADER => false,
	CURLOPT_URL => $full_url,
	CURLOPT_RETURNTRANSFER => true,
	CURLOPT_SSL_VERIFYPEER => false
);

$feed = curl_init();
curl_setopt_array($feed, $options);
// Obtenemos los datos ejecutando la peticion
$result = curl_exec($feed);
$info = curl_getinfo($feed);
curl_close($feed);


if(isset($info['content_type']) && isset($info['size_download'])){
	header('Content-Type: '.$info['content_type']);
	header('Content-Length: '.$info['size_download']);

}
// Al final devolvemos el resultado
echo($result);
?>