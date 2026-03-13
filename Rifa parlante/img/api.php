<?php
$host = "sql302.infinityfree.com";       // Ej: sql100.infinityfree.com
$user = "if0_41382641";           // Tu usuario real
$pass = "rU9npAoQoaD";     // La contraseña que viste en Overview
$db   = "if0_41382641_Rifa";      // El nombre completo de tu base de datos
// Configuración de tu base de datos (Cámbialos según tu servidor o XAMPP)


$conn = new mysqli($host, $user, $pass, $db);
if ($conn->connect_error) { die("Conexión fallida: " . $conn->connect_error); }

// Si la página pide los números (Al abrir la página)
if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    $result = $conn->query("SELECT * FROM puestos");
    $datos = array();
    while($row = $result->fetch_assoc()) {
        // Aseguramos que el número tenga dos dígitos (ej: "05")
        $numStr = sprintf("%02d", $row['numero']);
        $datos[$numStr] = $row['comprador'];
    }
    echo json_encode($datos);
}

// Si alguien hace clic para separar un número
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $numero = intval($_POST['numero']);
    $comprador = $conn->real_escape_string($_POST['comprador']);
    
    // Verificamos que no lo hayan comprado un segundo antes
    $check = $conn->query("SELECT * FROM puestos WHERE numero = $numero");
    if ($check->num_rows == 0) {
        $conn->query("INSERT INTO puestos (numero, comprador) VALUES ($numero, '$comprador')");
        echo "exito";
    } else {
        echo "ocupado";
    }
}
$conn->close();
?>