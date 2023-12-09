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

$query = $dbh->prepare('INSERT INTO players (name, password) VALUES (:name, :password)');
$query->bindParam(':name', $name, PDO::PARAM_STR);
$query->bindParam(':password', $password, PDO::PARAM_STR);

try {
    $query->execute();
    echo 'Registrace úspěšná.';
} catch (Exception $e) {
    echo '<h1>Došlo k chybě při registraci.</h1><pre>', $e->getMessage(),'</pre>';
}
?>
