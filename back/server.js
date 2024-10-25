// Importar módulos necesarios
const dotenv = require('dotenv');
dotenv.config();
const express = require('express');
const mysql = require('mysql2');
const cors = require('cors');

const app = express();

app.use(cors());
app.use(express.json());

const connection = mysql.createConnection(process.env.DATABASE_URL);

connection.connect((err) => {
  if (err) {
    console.error('Error al conectar a la base de datos:', err);
    return;
  }
  console.log('Conectado a la base de datos MySQL en Railway');
});

// Redirigir la raíz a /login
app.get('/', (req, res) => {
  res.redirect('/login'); // Redirigir a la ruta de inicio de sesión
});

// Ruta de registro
app.post('/api/registro', (req, res) => {
  const { correo, contrasena } = req.body;
  
  if (!correo || !contrasena) {
    return res.status(400).json({ message: 'El correo y la contraseña son obligatorios.' });
  }

  const query = 'INSERT INTO users (correo, contrasena) VALUES (?, ?)';
  connection.query(query, [correo, contrasena], (error, results) => {
    if (error) {
      console.error('Error al registrar el usuario:', error);
      return res.status(500).json({ message: 'Error al registrar el usuario.' });
    }
    res.status(201).json({ message: 'Usuario registrado exitosamente.' });
  });
});

// Ruta de inicio de sesión
app.post('/api/login', (req, res) => {
  const { correo, contrasena } = req.body;

  if (!correo || !contrasena) {
    return res.status(400).json({ message: 'El correo y la contraseña son obligatorios.' });
  }

  const query = 'SELECT * FROM users WHERE correo = ? AND contrasena = ?';
  connection.query(query, [correo, contrasena], (error, results) => {
    if (error) {
      console.error('Error al iniciar sesión:', error);
      return res.status(500).json({ message: 'Error al iniciar sesión.' });
    }

    if (results.length > 0) {
      res.status(200).json({ message: 'Inicio de sesión exitoso.', user: results[0] });
    } else {
      res.status(401).json({ message: 'Credenciales inválidas.' });
    }
  });
});

// Middleware para manejo de errores
app.use((err, req, res, next) => {
  console.error(err.stack);
  res.status(500).json({ message: 'Error del servidor.' });
});

// Middleware para manejo de rutas no encontradas
app.use((req, res) => {
  res.status(404).json({ message: 'Ruta no encontrada.' });
});

// Iniciar el servidor
const PORT = process.env.PORT || 5000;
app.listen(PORT, () => {
  console.log(`Servidor corriendo en http://localhost:${PORT}`);
});
