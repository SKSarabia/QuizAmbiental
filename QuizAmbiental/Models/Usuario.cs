using SQLite;

namespace QuizAmbiental.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [Unique]
        public string Username { get; set; }
    }
}