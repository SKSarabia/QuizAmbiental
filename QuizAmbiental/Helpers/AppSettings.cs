using System.IO;
using Microsoft.Maui.Storage;

namespace QuizAmbiental.Helpers
{
    public static class AppSettings
    {
        public static string ConnectionString = Path.Combine(FileSystem.AppDataDirectory, "quizambiental.db3");
    }
}