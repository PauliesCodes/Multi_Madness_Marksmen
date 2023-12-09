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

$name = $_GET['name'];

$query = $dbh->prepare('SELECT score FROM players WHERE name = :name');
$query->bindParam(':name', $name, PDO::PARAM_STR);
$query->execute();

$result = $query->fetch(PDO::FETCH_ASSOC);

if ($result !== false) {
    echo 'Skóre hráče ' . $name . ' je: ' . $result['score'];
} else {
    echo 'Hráč s jménem ' . $name . ' nebyl nalezen.';
}
?>
