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
                    QuestionText = "¿Por qué la deforestación es un problema grave para el medio ambiente?",
                    Answers = new string[] {
                        "Porque reduce el espacio para construir casas",
                        "Porque los árboles crecen demasiado rápido",
                        "Porque destruye el hábitat de animales y afecta el ciclo del agua",
                        "Porque deja más espacio para el ganado"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo afecta la contaminación del aire a la salud de las personas?",
                    Answers = new string[] {
                        "No tiene efecto en la salud",
                        "Puede causar enfermedades respiratorias",
                        "Hace que el aire huela mejor",
                        "Reduce la cantidad de oxígeno en los pulmones"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es el problema del uso excesivo de fertilizantes químicos?",
                    Answers = new string[] {
                        "Contaminan los suelos y el agua",
                        "Hacen que las plantas sean más resistentes",
                        "Ayudan a reducir la contaminación",
                        "No afectan el medio ambiente"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo afectan los plásticos a los océanos?",
                    Answers = new string[] {
                        "Dañan la vida marina y tardan años en degradarse",
                        "Ayudan a los peces a esconderse",
                        "Mejoran la calidad del agua",
                        "Desaparecen rápidamente"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es el impacto de los gases de efecto invernadero?",
                    Answers = new string[] {
                        "Enfrían el clima",
                        "Aumentan la temperatura del planeta",
                        "Purifican el aire",
                        "Generan más oxígeno"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Qué sucede cuando un ecosistema pierde muchas especies?",
                    Answers = new string[] {
                        "Mejora su sostenibilidad",
                        "Se vuelve más fuerte",
                        "Aumenta su biodiversidad",
                        "Se desequilibra"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Por qué es importante proteger los arrecifes de coral?",
                    Answers = new string[] {
                        "Son inútiles para el ecosistema",
                        "Se pueden reemplazar fácilmente",
                        "No tienen función en el océano",
                        "Son el hogar de muchas especies marinas"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Qué podemos hacer para reducir nuestra huella ecológica?",
                    Answers = new string[] {
                        "Usando menos agua y energía",
                        "Talando más bosques",
                        "No preocuparnos por el medio ambiente",
                        "Consumir más productos plásticos"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo influye el derretimiento de los glaciares en el nivel del mar?",
                    Answers = new string[] {
                        "No tiene impacto en los océanos",
                        "Aumenta el nivel del mar",
                        "Genera más hielo",
                        "Hace que el mar se reduzca"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo podemos restaurar un ecosistema dañado?",
                    Answers = new string[] {
                        "Reforestando y protegiendo especies",
                        "Construyendo fábricas",
                        "Cazando más animales",
                        "Usando productos químicos en los ríos"
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