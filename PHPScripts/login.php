<?php
$hostname = 'localhost';
$username = 'root';
$password = '';
$database = 'your_database_name';

try {
    $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
} catch(PDOException $e) {
    echo '<h1>Došlo k chybě při připojení k databázi.</h1><pre>', $e->getMessage(),'</pre>';
}

$name = $_POST['name'];
$password = $_POST['password'];

$query = $dbh->prepare('SELECT * FROM players WHERE name = :name AND password = :password');
$query->bindParam(':name', $name, PDO::PARAM_STR);
$query->bindParam(':password', $password, PDO::PARAM_STR);
$query->execute();

$result = $query->fetch(PDO::FETCH_ASSOC);

if ($result !== false) {
    echo 'Přihlášení úspěšné.';
} else {
    echo 'Chyba přihlášení. Zkontrolujte jméno a heslo.';
}
?>
