using SQLite;
using System;

namespace QuizAmbiental.Models
{
    public class Puntaje
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int UsuarioID { get; set; }
        public int Score { get; set; }
        public string Dificultad { get; set; }
        public DateTime Fecha { get; set; }
    }
}