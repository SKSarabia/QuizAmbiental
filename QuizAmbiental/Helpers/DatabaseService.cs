using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using QuizAmbiental.Models;

namespace QuizAmbiental.Helpers
{
    public class DatabaseService
    {
        private readonly string connectionString;

        public DatabaseService()
        {
            // Cadena definida en AppSettings
            connectionString = AppSettings.ConnectionString;
        }

        public int GetOrCreateUser(string username)
        {
            int userId = 0;
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // Verifica si ya existe el usuario
                string queryCheck = "SELECT ID FROM Usuarios WHERE Username = @username;";
                using (var cmd = new MySqlCommand(queryCheck, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                        return userId;
                    }
                }
                // Si no existe, inserta el nuevo usuario
                string queryInsert = "INSERT INTO Usuarios (Username) VALUES (@username); SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(queryInsert, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
            }
            return userId;
        }

        public void AddScore(int userId, int score, string dificultad)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Puntajes (UsuarioID, Score, Dificultad) VALUES (@userId, @score, @dificultad);";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@score", score);
                    cmd.Parameters.AddWithValue("@dificultad", dificultad);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Método para obtener ranking
        public List<ScoreEntry> GetTopScores(string dificultad = null)
        {
            var scores = new List<ScoreEntry>();
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                // Si se especifica dificultad, filtramos; si no, traemos todo.
                string query = dificultad == null
                    ? @"SELECT u.Username, p.Score, p.Fecha, p.Dificultad 
                FROM Puntajes p 
                JOIN Usuarios u ON p.UsuarioID = u.ID 
                ORDER BY p.Score DESC;"
                    : @"SELECT u.Username, p.Score, p.Fecha, p.Dificultad 
                FROM Puntajes p 
                JOIN Usuarios u ON p.UsuarioID = u.ID 
                WHERE p.Dificultad = @dificultad 
                ORDER BY p.Score DESC;";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    if (dificultad != null)
                        cmd.Parameters.AddWithValue("@dificultad", dificultad);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(new ScoreEntry
                            {
                                Username = reader.GetString("Username"),
                                Score = reader.GetInt32("Score"),
                                Date = reader.GetDateTime("Fecha")
                            });
                        }
                    }
                }
            }
            return scores;
        }
    }
}
