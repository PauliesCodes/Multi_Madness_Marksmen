<!DOCTYPE html>
<html lang="cs">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="icon" href="icon.png" sizes="32x32" type="image/png">
    <title>MMM Staty</title>
    <style>
        body {
            background-color: #333; /* tmavě šedé pozadí */
            color: #fff; /* bílý text */
            font-family: 'Arial', sans-serif; /* vyberte pěkný font */
            margin: 0;
            padding: 0;
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        h2 {
            margin-top: 20px; /* odsazení nadpisu */
        }

        ul {
            list-style-type: none; /* odstranění výchozích odrážek */
            padding: 0;
        }

        li {
            border: 1px solid #555; /* ohraničení trochu tmavší šedou */
            margin: 10px 0; /* odsazení mezi jednotlivými záznamy */
            padding: 10px;
            width: 80%; /* šířka záznamu */
            display: flex;
            justify-content: space-between;
            align-items: center;
            background-color: #444; /* barva pozadí jednoho záznamu */
        }

        .rank-icon {
            width: 30px; /* šířka ikony */
            height: auto;
        }
    </style>
</head>
<body>

<?php
$hostname = 'localhost';
$username = 'jentej';
$password = 'Tomanec1+';
$database = 'jentejden';

try {
    $dbh = new PDO('mysql:host='. $hostname .';dbname='. $database, $username, $password);
    $dbh->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
} catch(PDOException $e) {
    echo '<h1>Došlo k chybě při připojení k databázi.</h1><pre>', $e->getMessage(),'</pre>';
}

// Získání všech uživatelů seřazených podle ranku a následně podle jména
$usersQuery = $dbh->prepare('SELECT name, score, RANK() OVER (ORDER BY score DESC, name ASC) AS position FROM players');
$usersQuery->execute();
$users = $usersQuery->fetchAll(PDO::FETCH_ASSOC);

if ($users) {
    // Zobrazení uživatelů na stránce
    echo '<h2>Seznam uživatelů podle ranku:</h2>';
    echo '<ul>';
    foreach ($users as $user) {
        echo '<li>';
        echo '<span>'.$user['name'].'</span>';
        echo '<img class="rank-icon" src="first.png" alt="1. místo">';
        echo '<span>Rank: '.$user['position'].'</span>';
        echo '<span>Score: '.$user['score'].'</span>';
        echo '</li>';
    }
    echo '</ul>';
} else {
    echo 'Nebyli nalezeni žádní uživatelé.';
}
?>
</body>
</html>
