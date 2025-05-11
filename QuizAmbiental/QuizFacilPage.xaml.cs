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
                        "Porque hace que los productos sean más caros",
                        "Porque reduce la cantidad de residuos en el planeta",
                        "Porque genera más contaminación",
                        "Porque los materiales reciclados son menos resistentes"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Qué sucede si desperdiciamos mucha agua?",
                    Answers = new string[] {
                        "La cantidad de lluvia aumenta",
                        "No tiene impacto porque el agua es infinita",
                        "Se gasta un recurso limitado y afecta la vida de muchas especies",
                        "Mejora la calidad de los ríos"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo ayudan los árboles al planeta?",
                    Answers = new string[] {
                        "Bloquean la luz del sol",
                        "Absorben dióxido de carbono y producen oxígeno",
                        "Reducen la temperatura sin afectar el aire",
                        "Solo sirven para dar sombra"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es una forma de evitar la contaminación del aire?",
                    Answers = new string[] {
                        "Usar bicicleta o transporte público en lugar de autos",
                        "Usar ventiladores para mejorar el aire",
                        "Dejar el motor del auto encendido todo el tiempo",
                        "Quemar basura lejos de casa"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Qué ocurre cuando tiramos basura en los ríos?",
                    Answers = new string[] {
                        "Los peces la usan como refugio",
                        "Se contamina el agua y afecta a los animales",
                        "La basura desaparece con el tiempo",
                        "El agua se vuelve más saludable"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo podemos cuidar a los animales en peligro de extinción?",
                    Answers = new string[] {
                        "No comprando productos derivados de ellos",
                        "Cazándolos para protegerlos mejor",
                        "Alimentándolos con comida industrial",
                        "Sacándolos de su hábitat natural"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es una forma efectiva de reducir el uso de plásticos contaminantes?",
                    Answers = new string[] {
                        "Enterrarlos en el suelo",
                        "Guardar los plásticos sin tirarlos",
                        "Quemarlos para que desaparezcan",
                        "Usar bolsas de tela en lugar de bolsas de plástico"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "¿Qué impacto tiene el desperdicio de alimentos en el medio ambiente?",
                    Answers = new string[] {
                        "Hace que los animales tengan más comida",
                        "Mejora la calidad del suelo",
                        "Aumenta la cantidad de desechos orgánicos y la contaminación",
                        "No tiene impacto porque los alimentos se descomponen"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "¿Cuál es una acción sostenible que podemos hacer en casa?",
                    Answers = new string[] {
                        "Comprar más productos plásticos",
                        "Reducir el consumo de energía y agua",
                        "Usar más electricidad en las noches",
                        "Tirar la basura en espacios abiertos"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "¿Cómo podemos ayudar a reducir el calentamiento global?",
                    Answers = new string[] {
                        "Plantando más árboles",
                        "Usando más gasolina en los autos",
                        "Deforestando los bosques",
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
}