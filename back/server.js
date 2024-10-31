require('dotenv').config();
const express = require('express');
const mysql = require('mysql2');
const bodyParser = require('body-parser');
const cors = require('cors');

const app = express();
const port = process.env.PORT || 3000;

// Middleware
app.use(cors({
  origin: 'https://snazzy-mooncake-161ed9.netlify.app', // Cambia esto por tu dominio de Netlify
}));
app.use(bodyParser.json());

// Configuración de la base de datos
const db = mysql.createConnection({
  host: process.env.DB_HOST,
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  database: process.env.DB_NAME,
  port: process.env.DB_PORT,
});

// Conexión a la base de datos
db.connect((err) => {
  if (err) {
    console.error('Error connecting to the database:', err);
    return;
  }
  console.log('Connected to the database.');
});

// Registro de usuarios
app.post('/api/register', (req, res) => {
  const { email, password } = req.body;

  db.query('SELECT * FROM users WHERE email = ?', [email], (err, results) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    if (results.length > 0) {
      return res.status(400).json({ message: 'Este correo electrónico ya está registrado.' });
    }

    db.query('INSERT INTO users (email, password) VALUES (?, ?)', [email, password], (err) => {
      if (err) {
        return res.status(500).json({ error: err.message });
      }
      res.status(201).json({ message: 'Registro exitoso.' });
    });
  });
});

// Inicio de sesión
app.post('/api/login', (req, res) => {
  const { email, password } = req.body;

  db.query('SELECT * FROM users WHERE email = ?', [email], (err, results) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }

    if (results.length === 0 || results[0].password !== password) {
      return res.status(401).json({ message: 'Correo electrónico o contraseña incorrectos.' });
    }

    res.json({ user: results[0] });
  });
});

// Añadir un auto a la galería de alquileres
app.post('/api/rentals', (req, res) => {
  const { name, price, url } = req.body;

  if (!name || !price || !url) {
    return res.status(400).json({ message: 'Todos los campos son obligatorios.' });
  }

  db.query('INSERT INTO rentals (name, price, url) VALUES (?, ?, ?)', [name, price, url], (err) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.status(201).json({ message: 'Auto añadido exitosamente.' });
  });
});

// Proceso de alquiler de autos
app.post('/api/rentals/checkout', (req, res) => {
  const { name } = req.body;

  if (!name) {
    return res.status(400).json({ message: 'El nombre del auto es obligatorio.' });
  }

  db.beginTransaction((err) => {
    if (err) {
      return res.status(500).json({ error: 'Error al iniciar la transacción.' });
    }

    db.query('SELECT * FROM rentals WHERE name = ?', [name], (selectErr, results) => {
      if (selectErr) {
        return db.rollback(() => {
          res.status(500).json({ error: 'Error al seleccionar el auto.' });
        });
      }

      if (results.length === 0) {
        return db.rollback(() => {
          res.status(404).json({ message: 'Auto no encontrado.' });
        });
      }

      const { price, url } = results[0];

      db.query('INSERT INTO rented_cars (name, price, url) VALUES (?, ?, ?)', [name, price, url], (insertErr) => {
        if (insertErr) {
          return db.rollback(() => {
            res.status(500).json({ error: 'Error al insertar el auto en rented_cars.' });
          });
        }

        db.query('DELETE FROM rentals WHERE name = ?', [name], (deleteErr) => {
          if (deleteErr) {
            return db.rollback(() => {
              res.status(500).json({ error: 'Error al eliminar el auto de rentals.' });
            });
          }

          db.commit((commitErr) => {
            if (commitErr) {
              return db.rollback(() => {
                res.status(500).json({ error: 'Error al confirmar la transacción.' });
              });
            }
            res.status(201).json({ message: 'Alquiler registrado exitosamente.' });
          });
        });
      });
    });
  });
});

// Obtener todos los autos disponibles para alquiler
app.get('/api/rentals', (req, res) => {
  db.query('SELECT * FROM rentals', (err, results) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.json(results);
  });
});

// Obtener todos los autos alquilados
app.get('/api/rented-cars', (req, res) => {
  db.query('SELECT * FROM rented_cars', (err, results) => {
    if (err) {
      return res.status(500).json({ error: err.message });
    }
    res.json(results);
  });
});

// Cancelar un alquiler
app.delete('/api/rentals/checkout', (req, res) => {
  const { name } = req.body;

  if (!name) {
    return res.status(400).json({ message: 'El nombre del auto es obligatorio.' });
  }

  db.beginTransaction((err) => {
    if (err) {
      return res.status(500).json({ error: 'Error al iniciar la transacción.' });
    }

    db.query('SELECT * FROM rented_cars WHERE name = ?', [name], (getErr, rentedResults) => {
      if (getErr) {
        return db.rollback(() => {
          res.status(500).json({ error: 'Error al obtener el auto de rented_cars.' });
        });
      }

      if (rentedResults.length === 0) {
        return db.rollback(() => {
          res.status(404).json({ message: 'Auto no encontrado en rented_cars.' });
        });
      }

      const { price, url } = rentedResults[0]; // Suponiendo que existe

      db.query('DELETE FROM rented_cars WHERE name = ?', [name], (deleteErr) => {
        if (deleteErr) {
          return db.rollback(() => {
            res.status(500).json({ error: 'Error al eliminar el auto de rented_cars.' });
          });
        }

        db.query('INSERT INTO rentals (name, price, url) VALUES (?, ?, ?)', [name, price, url], (insertErr) => {
          if (insertErr) {
            return db.rollback(() => {
              res.status(500).json({ error: 'Error al añadir el auto de nuevo a rentals.' });
            });
          }

          db.commit((commitErr) => {
            if (commitErr) {
              return db.rollback(() => {
                res.status(500).json({ error: 'Error al confirmar la transacción.' });
              });
            }
            res.status(200).json({ message: 'Alquiler cancelado y auto devuelto exitosamente.' });
          });
        });
      });
    });
  });
});

// Iniciar el servidor
app.listen(port, () => {
  console.log(`Server is running on http://localhost:${port}`);
});
