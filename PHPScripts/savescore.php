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
$score = $_POST['score'];

$query = $dbh->prepare('UPDATE players SET score = :score WHERE name = :name');
$query->bindParam(':name', $name, PDO::PARAM_STR);
$query->bindParam(':score', $score, PDO::PARAM_INT);

try {
    $query->execute();
    echo 'Skóre hráče ' . $name . ' bylo aktualizováno na ' . $score;
} catch (Exception $e) {
    echo '<h1>Došlo k chybě při aktualizaci skóre.</h1><pre>', $e->getMessage(),'</pre>';
}
?>
