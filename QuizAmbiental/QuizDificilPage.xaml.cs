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

        // Lista de 10 preguntas dif�ciles
        questions = new List<QuizQuestion>
            {
                new QuizQuestion {
                    QuestionText = "�C�mo afecta la deforestaci�n al ciclo del agua?",
                    Answers = new string[] {
                        "M�s lluvias en la regi�n",
                        "Mejora agua potable",
                        "Menos retenci�n de agua, afecta r�os y lagos",
                        "Sin impacto en ciclo del agua"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "�Por qu� el calentamiento global afecta m�s a los polos?",
                    Answers = new string[] {
                        "Menos agua congelada en invierno",
                        "Clima m�s seco",
                        "Hielos derriten, sube nivel del mar",
                        "Animales necesitan m�s calor"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "�Impacto de residuos industriales en ecosistemas acu�ticos?",
                    Answers = new string[] {
                        "Agua filtra toxinas, sin efecto",
                        "Contaminan agua, afectan flora y fauna",
                        "Mejoran crecimiento de corales",
                        "Purifican agua en r�os"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "�Acidificaci�n de oc�anos y equilibrio marino?",
                    Answers = new string[] {
                        "Debilita conchas y esqueletos",
                        "Peces m�s resistentes",
                        "Menos ox�geno en mares",
                        "Sin impacto en biodiversidad"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "�Por qu� especies invasoras da�an ecosistemas?",
                    Answers = new string[] {
                        "Reemplazan especies d�biles",
                        "Mejoran biodiversidad local",
                        "Aceleran evoluci�n de especies",
                        "Compiten por recursos, desequilibran ecosistema"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "�Derretimiento de glaciares y nivel del mar?",
                    Answers = new string[] {
                        "Agua oce�nica m�s fr�a",
                        "Aumenta nivel del mar, afecta costas",
                        "M�s lluvias en desiertos",
                        "Mejor calidad del agua"
                    },
                    CorrectAnswerIndex = 1
                },
                new QuizQuestion {
                    QuestionText = "�Problema de la sobreexplotaci�n pesquera?",
                    Answers = new string[] {
                        "Menos peces, afecta cadena alimenticia",
                        "M�s oportunidades econ�micas sin impacto",
                        "Mejora equilibrio ecol�gico",
                        "Otras especies crecen m�s r�pido"
                    },
                    CorrectAnswerIndex = 0
                },
                new QuizQuestion {
                    QuestionText = "�Contaminaci�n de suelos y agricultura?",
                    Answers = new string[] {
                        "Acelera crecimiento de plantas",
                        "Sin impacto en calidad de alimentos",
                        "M�s nutrientes naturales en suelo",
                        "Menos fertilidad, afecta cultivos"
                    },
                    CorrectAnswerIndex = 3
                },
                new QuizQuestion {
                    QuestionText = "�Parques naturales y biodiversidad?",
                    Answers = new string[] {
                        "Talan �rboles m�s eficiente",
                        "Humanidad dom�stica especies",
                        "Protegen h�bitats de da�o humano",
                        "Ciudades crecen en �reas protegidas"
                    },
                    CorrectAnswerIndex = 2
                },
                new QuizQuestion {
                    QuestionText = "�Impacto de derrames de petr�leo en oc�anos?",
                    Answers = new string[] {
                        "M�s temperatura, beneficia biodiversidad",
                        "Sin efecto en ecosistemas marinos",
                        "Contaminan agua, afectan vida marina a�os",
                        "Se disuelven r�pido, sin contaminaci�n"
                    },
                    CorrectAnswerIndex = 2
                }
            };


        // Inicializar el cron�metro
        timer = Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += OnTimerTick;
        timer.Start();

        DisplayCurrentQuestion();
    }

    private void OnTimerTick(object? sender, EventArgs e)
    {
        elapsedTime++;
        lblTimer.Text = $"Tiempo: {elapsedTime} s";
    }

    private void DisplayCurrentQuestion()
    {
        if (currentQuestionIndex >= questions.Count)
        {
            timer.Stop();
            Navigation.PushAsync(new ScorePage(correctAnswers, elapsedTime, "Dif�cil"));
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