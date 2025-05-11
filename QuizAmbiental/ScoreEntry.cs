using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizAmbiental.Models
{
    public class ScoreEntry
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public DateTime Date { get; set; }
    }
}
