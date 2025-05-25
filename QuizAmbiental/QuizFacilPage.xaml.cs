using System.Collections.Generic;
using System.Threading.Tasks;
namespace QuizAmbiental;

public partial class QuizFacilPage : ContentPage
{
    private List<QuizQuestion> questions;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int elapsedTime = 0;
    private IDispatcherTimer timer;

    public QuizFacilPage()
    {
        InitializeComponent();

        // Lista de 10 preguntas fáciles
        questions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    QuestionText = "¿Por qué es importante reciclar?",
                    Answers = new string[] {
                        "Aumenta costos",
                        "Reduce residuos",
                        "Genera contaminación",
                        "Menos resistencia"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Qué sucede si desperdiciamos mucha agua?",
                    Answers = new string[] {
                        "Más lluvia",
                        "Sin impacto, agua infinita",
                        "Recurso limitado afecta especies",
                        "Ríos mejoran calidad"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo ayudan los árboles al planeta?",
                    Answers = new string[] {
                        "Bloquean luz solar",
                        "Absorben CO₂, producen oxígeno",
                        "Reducen temperatura sin afectar aire",
                        "Solo dan sombra"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo evitar contaminación del aire?",
                    Answers = new string[] {
                        "Bicicleta o transporte público",
                        "Ventiladores mejoran aire",
                        "Motor encendido siempre",
                        "Quemar basura lejos"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Qué pasa al tirar basura en ríos?",
                    Answers = new string[] {
                        "Peces la usan de refugio",
                        "Agua contaminada, afecta animales",
                        "Basura desaparece con el tiempo",
                        "Agua más saludable"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo cuidar animales en peligro?",
                    Answers = new string[] {
                        "No comprar derivados",
                        "Cazarlos para protegerlos",
                        "Alimentarlos con comida industrial",
                        "Sacarlos de su hábitat"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo reducir plásticos contaminantes?",
                    Answers = new string[] {
                        "Enterrarlos en el suelo",
                        "Guardar los plásticos",
                        "Quemarlos",
                        "Usar bolsas de tela y no de plástico"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Impacto del desperdicio de alimentos?",
                    Answers = new string[] {
                        "Más comida para animales",
                        "Mejora suelo",
                        "Más desechos y contaminación",
                        "Sin impacto, se descomponen"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Acción sostenible en casa?",
                    Answers = new string[] {
                        "Más productos plásticos",
                        "Menos energía y agua",
                        "Más electricidad en noches",
                        "Basura en espacios abiertos"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo ayudar al calentamiento global?",
                    Answers = new string[] {
                        "Plantando más árboles",
                        "Usando más gasolina",
                        "Deforestando bosques",
                        "Fabricando más plásticos"
                    },
                    CorrectAnswerIndex = 0
                }
            };

        // Inicializar el cronómetro: se actualiza cada segundo
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
            Navigation.PushAsync(new ScorePage(correctAnswers, elapsedTime, "Fácil"));
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
        btnAnswer0.IsEnabled = false;
        btnAnswer1.IsEnabled = false;
        btnAnswer2.IsEnabled = false;
        btnAnswer3.IsEnabled = false;

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
        // Activar los botones para la siguiente pregunta
        btnAnswer0.IsEnabled = true;
        btnAnswer1.IsEnabled = true;
        btnAnswer2.IsEnabled = true;
        btnAnswer3.IsEnabled = true;
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