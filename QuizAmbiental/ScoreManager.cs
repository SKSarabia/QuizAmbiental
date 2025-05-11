using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using QuizAmbiental.Models;

namespace QuizAmbiental.Helpers
{
    public static class ScoreManager
    {
        private static List<ScoreEntry> scoreEntries = new List<ScoreEntry>();

        public static void AddScore(ScoreEntry entry)
        {
            scoreEntries.Add(entry);
        }

        public static List<ScoreEntry> GetScores()
        {
            return scoreEntries.OrderByDescending(s => s.Score).ToList();
        }
    }
}