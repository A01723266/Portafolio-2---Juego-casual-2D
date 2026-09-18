using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleModeController : MonoBehaviour
{
    [Header("Bullet Controller")]
    [SerializeField] private BulletController bulletController;

    [Header("Counter Controller")]
    [SerializeField] private CounterController counterController;


    // Configuración seleccionada.
    private BattleSettings currentSettings;

    // Define si la batalla termino.
    private bool isBattleFinished;

    // Contador de dificultad acumulado en Infinito
    private float infiniteDifficultyTime;

    // Contador de separacion entre cada descanso
    private float restSeparationTimer;

    // Contador de duracion de descanso
    private float restTimer;

    // Tiempo de separacion entre cada descanso
    private float currentRestSeparation;

    // Define si esta o no en fase de descanso
    private bool isResting;


    private void Awake()
    {
        // Comprueba que haya configuracion definida.
        if (GameSession.CurrentSettings == null)
        {
            Debug.LogWarning("No se encontró configuración de partida. ");
            return;
        }

        // Guarda los datos del modo seleccionado.
        currentSettings = GameSession.CurrentSettings;

        // Aplica la dificultad inicial.
        ApplyDifficulty(0f);

        // Debug para detectar errores
        Debug.Log("Modo seleccionado: " + currentSettings.gameMode);

        if (currentSettings.gameMode == GameMode.Infinite)
        {
            currentRestSeparation = currentSettings.initialRestSeparation;
        }
    }


    private void Update()
    {
        // Si no hay configuración no hace nada.
        if (currentSettings == null)
        {
            return;
        }

        // El modo infinito se define aparte por que tine configuracion especial.
        if (currentSettings.gameMode == GameMode.Infinite)
        {
            UpdateInfiniteMode();
            return;
        }

        // Cuando el tiempo termino, ya no genera balas.
        if (counterController.isTimeFinished)
        {
            FinishTimedMode();
            return;
        }

        // Usa el progreso para aumentar la difucultad
        ApplyDifficulty(counterController.dificultyProgress);
    }

    // Controla cuando inicia y termina el descanso y lo que pasa en ambos casos
    private void UpdateInfiniteMode()
    {
        if (isResting)
        {
            // cuanta el tiempo que dura en el descanso
            restTimer += Time.deltaTime;

            // Detenemos la velocidad y aplicamos spawn de descanso
            ApplyRestBulletSettings();

            // Cuando termina el descanso volve a la prseion normal
            if(restTimer >= currentSettings.restDuration)
            {
                // Define el descanso como terminado y aplica el temporizador como 0 para poder volver a usarlo
                isResting = false;
                restTimer = 0f;
                
                // Reinicia el temporizador de tiempo de espera
                restSeparationTimer = 0f;

                // Incementa el tiempo entre cada separacion
                currentRestSeparation += currentSettings.restSeparationIncrementation;
                
                // Debug para detectar errores
                Debug.Log("Descanso terminado. Proxima separacion: " + currentRestSeparation + "segundos");
            }

            return;
        }

        // Aumenta la dificultad fuera de el descanso
        infiniteDifficultyTime += Time.deltaTime;

        // Vuelve a aplicar dificultad normal
        ApplyNormalInfiniteDifficulty();

        // Cuenta el tiempo que que falta antes de llegar a el proximo descanso
        restSeparationTimer += Time.deltaTime;

        if (restSeparationTimer >= currentRestSeparation)
        {
            // Define el descanso como Iniciado y aplica el temporizador como 0 para poder volver a usarlo
            isResting = true;
            restTimer = 0f;
            
            // Debug para detectar errores
            Debug.Log("Inicio de descanso.");
        }

    }

    // Calcula la velocidad actual de el modo infinito
    private float GetInfiniteBulletSpeed()
    {
        return currentSettings.initialBulletSpeed +
            (infiniteDifficultyTime *
            currentSettings.infiniteSpeedIncreasePerSecond);
    }
    
    // Aplica los valores normales
    private void ApplyNormalInfiniteDifficulty()
    {
        // Aumenta la vemocidad constantemente
        float bulletSpeed = GetInfiniteBulletSpeed();

        // Baja el tiempo de spawn constantemente
        float minSpawnTime = Mathf.Max(
            currentSettings.minimumMinSpawnTime, 
            currentSettings.initialMinSpawnTime - (infiniteDifficultyTime * currentSettings.infiniteMinSpawnDecreasePerSecond)
        );
        float maxSpawnTime = Mathf.Max(
            currentSettings.minimumMaxSpawnTime, 
            currentSettings.initialMaxSpawnTime - (infiniteDifficultyTime * currentSettings.infiniteMaxSpawnDecreasePerSecond)
        );

        // Aplica los cambios a el controlador de bala
        bulletController.Configure(
            bulletSpeed,
            minSpawnTime,
            maxSpawnTime
        );
    }

    // Aplica los valores de el descanso
    private void ApplyRestBulletSettings()
    {
        // Mantene la velociad que se obtuvo antes de el inicio
        float frozenBulletSpeed = GetInfiniteBulletSpeed();

        // Aplica los cambios al controllador de bala
        bulletController.Configure(
            frozenBulletSpeed,
            currentSettings.restMinSpawnTime,
            currentSettings.restMaxSpawnTime
        );
    }


    // Ajusta velocidad e intervalos min/max de spawn según el progreso.
    private void ApplyDifficulty(float progress)
    {
        // Suaviza el incremento de dificultad
        float smoothProgress = Mathf.SmoothStep(0f, 1f, progress);

        // Aumenta la velocidad de las balas.
        float bulletSpeed = Mathf.Lerp(
            currentSettings.initialBulletSpeed,
            currentSettings.maximumBulletSpeed,
            smoothProgress
        );

        // Reduce el tiempo entre balas para que aparezcan más seguido.
        float minSpawnTime = Mathf.Lerp(
            currentSettings.initialMinSpawnTime,
            currentSettings.finalMinSpawnTime,
            smoothProgress
        );

        float maxSpawnTime = Mathf.Lerp(
            currentSettings.initialMaxSpawnTime,
            currentSettings.finalMaxSpawnTime,
            smoothProgress
        );

        // Aplica los valores al generador de balas
        bulletController.Configure(
            bulletSpeed,
            minSpawnTime,
            maxSpawnTime
        );
    }


    // Termina batalla por tiempo.
    private void FinishTimedMode()
    {
        // Detine si la variable ya habia sido definida para que no se repita.
        if (isBattleFinished)
        {
            return;
        }

        // La batalla termino definida por una variable
        isBattleFinished = true;

        // Detiene la produccion de balas
        bulletController.SetSpawning(false);

        // Llama a el cambio de escena
        StartCoroutine(LoadWinScene());

        // Debug para detectar errores
        Debug.Log("Modo por tiempo completado.");
    }

    private IEnumerator LoadWinScene()
    {
        // Espera 2 segundos
        yield return new WaitForSeconds(3f);

        // Cambia de escena
        SceneManager.LoadScene("WinScene");
    }
}