using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper; 
using Microsoft.Data.Sqlite;

namespace MauiAppExamen
{
    public class Vehiculos
    {
        private string connectionString;
        private SqliteConnection connection;

        public Vehiculos()
        {
            connectionString = "Data Source=vehiculos.db";
            connection = new SqliteConnection(connectionString);
            connection.Open();

            // Crear la tabla de vehiculos si no existe
            connection.Execute(@"CREATE TABLE IF NOT EXISTS Vehiculos (
                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                Marca TEXT NOT NULL,
                                Modelo TEXT NOT NULL,
                                Anio INTEGER NOT NULL )");
        }

        // Create
        public Vehiculo CrearVehiculo(int id, string marca, string modelo, int anio)
        {
            var nuevoVehiculo = new Vehiculo
            {
                Id = id, // Usually ID is ignored by auto-increment on insert but maintained in object representation for consistency
                Marca = marca,
                Modelo = modelo,
                Anio = anio
            };

            var recordsAfected = connection.Execute("INSERT INTO Vehiculos (Marca, Modelo, Anio) VALUES (@Marca, @Modelo, @Anio)", nuevoVehiculo);

            if (recordsAfected == 0)
            {
                throw new Exception("No se pudo insertar el vehiculo en la base de datos.");
            }
            else
            {
                // Getting the last inserted Id
                var lastId = connection.QuerySingle<int>("SELECT last_insert_rowid()");
                nuevoVehiculo.Id = lastId;
                return nuevoVehiculo;
            }
        }

        // Read
        public Vehiculo ReadByID(int id)
        {
            var data = connection.Query<Vehiculo>("SELECT * FROM Vehiculos WHERE Id = @Id", new { Id = id }).ToList();

            if (data.Count == 0)
            {
                return null;
            }
            else
            {
                return data[0];
            }
        }

        // Read-All
        public List<Vehiculo> ReadAll()
        {
            var data = connection.Query<Vehiculo>("SELECT * FROM Vehiculos").ToList();
            return data;
        }

        // Update
        public void Update(int id, string marca, string modelo, int anio)
        {
            var recordsAfected = connection.Execute("UPDATE Vehiculos SET Marca = @Marca, Modelo = @Modelo, Anio = @Anio WHERE Id = @Id", new { Id = id, Marca = marca, Modelo = modelo, Anio = anio });
            if (recordsAfected == 0)
            {
                throw new Exception("No se pudo actualizar el vehiculo en la base de datos.");
            }
        }

        // Delete
        public void Delete(int id)
        {
            var recordsAfected = connection.Execute("DELETE FROM Vehiculos WHERE Id = @Id", new { Id = id });
            if (recordsAfected == 0)
            {
                throw new Exception("No se pudo eliminar el vehiculo de la base de datos.");
            }
        }
    }
}
