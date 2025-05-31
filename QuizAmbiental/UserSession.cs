namespace QuizAmbiental.Models
{
    public static class UserSession
    {
        public static User? CurrentUser { get; set; }
        public static string? Dificultad { get; set; }
    }
}