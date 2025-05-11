using System.Collections.Generic;
using System.Threading.Tasks;
namespace QuizAmbiental;

public partial class QuizDificilPage : ContentPage
{
    private List<QuizQuestion> questions;
    private int currentQuestionIndex = 0;
    private int correctAnswers = 0;
    private int elapsedTime = 0;
    private IDispatcherTimer timer;

    public QuizDificilPage()
    {
        InitializeComponent();

        // Lista de 10 preguntas difíciles
        questions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    QuestionText = "¿Cómo afecta la deforestación al ciclo del agua?",
                    Answers = new string[] {
                        "Hace que haya más lluvias en la región",
                        "Mejora la calidad del agua potable",
                        "Reduce la capacidad del suelo para retener agua, afectando la disponibilidad de ríos y lagos",
                        "No tiene impacto en el ciclo del agua"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Por qué el calentamiento global afecta más a los polos?",
                    Answers = new string[] {
                        "Porque el agua se congela menos en invierno",
                        "Porque el clima se vuelve más seco",
                        "Porque los hielos se derriten y aumentan el nivel del mar",
                        "Porque los animales necesitan más calor para sobrevivir"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es el impacto de los residuos industriales en los ecosistemas acuáticos?",
                    Answers = new string[] {
                        "No tienen efecto porque el agua filtra las toxinas naturalmente",
                        "Contaminan el agua, afectando la flora y fauna acuática",
                        "Mejoran el crecimiento de los corales",
                        "Ayudan a purificar el agua en los ríos"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Qué papel juega la acidificación de los océanos en el equilibrio marino?",
                    Answers = new string[] {
                        "Debilita las conchas y esqueletos de organismos marinos como corales y moluscos",
                        "Permite que los peces sean más resistentes",
                        "Reduce la cantidad de oxígeno en los mares",
                        "No afecta a la biodiversidad marina"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Por qué las especies invasoras pueden dañar los ecosistemas?",
                    Answers = new string[] {
                        "Porque reemplazan a las especies más débiles, fortaleciendo el ecosistema",
                        "Porque mejoran la biodiversidad local",
                        "Porque ayudan a otras especies a evolucionar más rápido",
                        "Porque compiten por recursos con las especies nativas y pueden desequilibrar el ecosistema"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo afecta el derretimiento de los glaciares al nivel del mar?",
                    Answers = new string[] {
                        "Hace que el agua de los océanos se enfríe más",
                        "Provoca un aumento en el nivel del mar, afectando zonas costeras",
                        "Genera más lluvias en zonas desérticas",
                        "Mejora la calidad del agua para el consumo humano"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es el problema de la sobreexplotación pesquera?",
                    Answers = new string[] {
                        "Reduce las poblaciones de peces y afecta la cadena alimenticia marina",
                        "Genera más oportunidades económicas sin impacto ecológico",
                        "Mejora el equilibrio ecológico",
                        "Permite que otras especies crezcan más rápido"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo afecta la contaminación de los suelos a la agricultura?",
                    Answers = new string[] {
                        "Acelera el crecimiento de las plantas",
                        "No tiene impacto en la calidad de los alimentos",
                        "Genera más nutrientes naturales en el suelo",
                        "Reduce la fertilidad del suelo, afectando la producción de cultivos"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo pueden los parques naturales ayudar a conservar la biodiversidad?",
                    Answers = new string[] {
                        "Ayudan a talar árboles de manera más eficiente",
                        "Permiten que los humanos domestiquen especies silvestres",
                        "Protegen especies y hábitats de la actividad humana destructiva",
                        "Facilitan la expansión de ciudades dentro de áreas protegidas"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es el impacto de los derrames de petróleo en los océanos?",
                    Answers = new string[] {
                        "Aumentan la temperatura del agua, beneficiando la biodiversidad",
                        "No tienen efecto en los ecosistemas marinos",
                        "Contaminan el agua y afectan la vida marina durante años",
                        "Se disuelven rápidamente sin dejar contaminación"
                    },
                    CorrectAnswerIndex = 2
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
            Navigation.PushAsync(new ScorePage(correctAnswers, elapsedTime));
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
}