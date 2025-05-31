using System;
using System.Collections.Generic;
using System.Linq;
using QuizAmbiental.Models;
using SQLite;

namespace QuizAmbiental.Helpers
{
    public class DatabaseService
    {
        private readonly SQLiteConnection db;

        public DatabaseService()
        {
            db = new SQLiteConnection(AppSettings.ConnectionString);
            db.CreateTable<Usuario>();
            db.CreateTable<Puntaje>();
        }

        public int GetOrCreateUser(string username)
        {
            var user = db.Table<Usuario>().FirstOrDefault(u => u.Username == username);
            if (user != null)
                return user.ID;

            var newUser = new Usuario { Username = username };
            db.Insert(newUser);
            return newUser.ID;
        }

        public void AddScore(int userId, int score, string dificultad)
        {
            var puntaje = new Puntaje
            {
                UsuarioID = userId,
                Score = score,
                Dificultad = dificultad,
                Fecha = DateTime.Now
            };
            db.Insert(puntaje);
        }

        public List<ScoreEntry> GetTopScores(string dificultad = null)
        {
            var query = from p in db.Table<Puntaje>()
                        join u in db.Table<Usuario>() on p.UsuarioID equals u.ID
                        where dificultad == null || p.Dificultad == dificultad
                        orderby p.Score descending
                        select new ScoreEntry
                        {
                            Username = u.Username,
                            Score = p.Score,
                            Date = p.Fecha
                        };

            return query.ToList();
        }
        public List<Usuario> GetAllUsuarios()
        {
            return db.Table<Usuario>().ToList();
        }

        public Usuario? GetUsuarioByUsername(string username)
        {
            return db.Table<Usuario>().FirstOrDefault(u => u.Username == username);
        }

        public void UpdateUsuario(Usuario usuario)
        {
            db.Update(usuario);
        }

        public void DeleteUsuario(int id)
        {
            db.Delete<Usuario>(id);
        }
    }
}