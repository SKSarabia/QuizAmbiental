using System.Collections.Generic;
using System.Threading.Tasks;
namespace QuizAmbiental;

public partial class QuizMedioPage : ContentPage
{
    private List<QuizQuestion> questions;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int elapsedTime = 0;
    private IDispatcherTimer timer;

    public QuizMedioPage()
    {
        InitializeComponent();

        // Lista de 10 preguntas normales
        questions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    QuestionText = "¿Por qué la deforestación es un problema grave?",
                    Answers = new string[] {
                        "Menos espacio para casas",
                        "Árboles crecen rápido",
                        "Destruye hábitats",
                        "Más espacio para ganado"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo afecta la contaminación del aire a la salud?",
                    Answers = new string[] {
                        "Sin efecto en salud",
                        "Enfermedades respiratorias",
                        "Aire huele mejor",
                        "Menos oxígeno en pulmones"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Problema del uso excesivo de fertilizantes?",
                    Answers = new string[] {
                        "Contaminan suelo y agua",
                        "Plantas más resistentes",
                        "Reducen contaminación",
                        "No afectan ambiente"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo afectan los plásticos a los océanos?",
                    Answers = new string[] {
                        "Dañan vida marina, tardan años",
                        "Esconden peces",
                        "Mejoran agua",
                        "Desaparecen rápido"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Impacto de los gases de efecto invernadero?",
                    Answers = new string[] {
                        "Enfrían el clima",
                        "Aumentan temperatura",
                        "Purifican aire",
                        "Generan oxígeno"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Qué pasa si un ecosistema pierde especies?",
                    Answers = new string[] {
                        "Mejora su sostenibilidad",
                        "Se vuelve más fuerte",
                        "Aumenta su biodiversidad",
                        "Se desequilibra"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Por qué proteger los arrecifes de coral?",
                    Answers = new string[] {
                        "Inútiles para el ecosistema",
                        "Se reemplazan fácil",
                        "Sin función en océano",
                        "Hogar de especies marinas"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo reducir nuestra huella ecológica?",
                    Answers = new string[] {
                        "Menos agua y energía",
                        "Talando bosques",
                        "No preocuparse por ambiente",
                        "Más productos plásticos"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Derretimiento de glaciares y nivel del mar?",
                    Answers = new string[] {
                        "No tiene impacto en los océanos",
                        "Aumenta el nivel del mar",
                        "Genera más hielo",
                        "Hace que el mar se reduzca"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo restaurar un ecosistema dañado?",
                    Answers = new string[] {
                        "Reforestar y proteger especies",
                        "Construir fábricas",
                        "Cazar más animales",
                        "Usar químicos en ríos"
                    },
                    CorrectAnswerIndex = 0
                }
            };

        // Inicializar el cronómetro
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += OnTimerTick;
        timer.Start();

        DisplayCurrentQuestion();
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        elapsedTime++;
        lblTimer.Text = $"Tiempo: {elapsedTime} s";
    }

    private void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            timer.Stop();
            Navigation.PushAsync(new ScorePage(correctAnswers, elapsedTime, "Medio"));
            return;
        }

        var currentQuestion = questions[currentQuestionIndex];
        lblQuestion.Text = currentQuestion.QuestionText;
        btnAnswer0.Text = currentQuestion.Answers[0];
        btnAnswer1.Text = currentQuestion.Answers[1];
        btnAnswer2.Text = currentQuestion.Answers[2];
        btnAnswer3.Text = currentQuestion.Answers[3];

        ResetButtonStyles();
    }

    private void ResetButtonStyles()
    {
        btnAnswer0.BackgroundColor = Colors.LightGray;
        btnAnswer1.BackgroundColor = Colors.LightGray;
        btnAnswer2.BackgroundColor = Colors.LightGray;
        btnAnswer3.BackgroundColor = Colors.LightGray;
    }

    private async void OnAnswerClicked(object sender, EventArgs e)
    {
        if (!(sender is Button btnClicked))
            return;

        int selectedIndex = btnClicked == btnAnswer0 ? 0 :
                            btnClicked == btnAnswer1 ? 1 :
                            btnClicked == btnAnswer2 ? 2 :
                            btnClicked == btnAnswer3 ? 3 : -1;

        var currentQuestion = questions[currentQuestionIndex];

        if (selectedIndex == currentQuestion.CorrectAnswerIndex)
        {
            btnClicked.BackgroundColor = Colors.Green;
            correctAnswers++;
        }
        else
        {
            btnClicked.BackgroundColor = Colors.Red;
            HighlightCorrectAnswer(currentQuestion.CorrectAnswerIndex);
        }

        await Task.Delay(1000);
        currentQuestionIndex++;
        DisplayCurrentQuestion();
    }

    private void HighlightCorrectAnswer(int correctIndex)
    {
        switch (correctIndex)
        {
            case 0: btnAnswer0.BackgroundColor = Colors.Green; break;
            case 1: btnAnswer1.BackgroundColor = Colors.Green; break;
            case 2: btnAnswer2.BackgroundColor = Colors.Green; break;
            case 3: btnAnswer3.BackgroundColor = Colors.Green; break;
        }
    }
    protected override bool OnBackButtonPressed() => true;

}