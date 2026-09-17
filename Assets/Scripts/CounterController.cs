using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class CounterController : MonoBehaviour
{
    [Header("Textos del contador")]

    // Titulo
    [SerializeField] private Text counterTitle;
    // Valor
    [SerializeField] private Text counterValue;

    // Datos del modo de juego
    private BattleSettings currentSettings;

    // Tiempo transcurrido
    private float timeElapsed;

    //  Cosulta el tiempo sobrevivido de timeElapsed
    public float survivedTime => timeElapsed;

    // Tiempo restante
    public float timeRemaning;

    // Progreso de Dificultad
    public float dificultyProgress;

    // puntaje (para modo infinito)
    public int score {get; private set;}

    // Define si la partida ya termino
    public bool isTimeFinished {get; private set;}

    private void Awake()
    {
        // Debug para detectar errores
        if (GameSession.CurrentSettings == null)
        {
            Debug.LogError("No se encontró configuración de partida. ");
            return;
        }

        // Lee la configuracion de el menu
        currentSettings = GameSession.CurrentSettings;

        // Modo Infinito
        if (currentSettings.gameMode == GameMode.Infinite)
        {
            counterTitle.text = "Puntaje";
            score = 0;
            UpdateScoreText();
            return;
        }

        // Modo Facil/Dificil 
        counterTitle.text = "Timepo restante";
        timeRemaning = currentSettings.timeLimit;
        UpdateTimeText();
    }

    private void Update()
    {
        // Modo Infinito
        if (currentSettings == null || currentSettings.gameMode == GameMode.Infinite)
            return;
        
        // Tiempo ya termino
        if (isTimeFinished == true)
            return;

        // Suma el timepo que pasa
        timeElapsed += Time.deltaTime;

        // Callcula el tiempo restante
        timeRemaning = Mathf.Max(0f, currentSettings.timeLimit - timeElapsed);

        // Progreso de la partida en porcentaje
        dificultyProgress = Mathf.Clamp01(timeElapsed / currentSettings.timeLimit);

        // Actualiza el texto
        UpdateTimeText();

        // Tiempo llega a 0
        if (timeRemaning <= 0f)
        {
            isTimeFinished = true;

            // Debug para detectar errores
            Debug.Log("El tiempo termino.");
        }
    }

    // Funcion para agregar puntos
    public void AddPoint()
    {
        // Solo en modo Infinito
        if (currentSettings == null || currentSettings.gameMode != GameMode.Infinite)
            return;
        
        // Suma un punto por cada bala bloqueada
        score++;

        // Actualiz el texto
        UpdateScoreText();
    }

    // Muestra tiempo restante
    private void UpdateTimeText()
    {
        // redondea para arriba
        int totalSeconds = Mathf.CeilToInt(timeRemaning);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        counterValue.text = minutes + " min  " + seconds + " seg";
    }

    // Muestra puntaje
    private void UpdateScoreText()
    {
        counterValue.text = score + " puntos";
    }
}
