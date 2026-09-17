using UnityEngine;

// Guarda los 3 modos de juego seleccionables en una lista.
public enum GameMode
{
    Easy,
    Hard,
    Infinite
}

// Crea una clase para guardar los valores con los que se iniciara BattleScene.
[System.Serializable]
public class BattleSettings
{
    // Modo de juego Seleccionado.
    public GameMode gameMode;

    // Tiempo definido para Facil y Dificil.
    public float timeLimit;

    // Velocidad inicial de las balas.
    public float initialBulletSpeed;

    // Velocidad maxima de las balas.
    public float maximumBulletSpeed;

    // Intervalos min/max inicial para generar balas.
    public float initialMinSpawnTime;
    public float initialMaxSpawnTime;

    // Intervalos min/max final para generar balas.
    public float finalMinSpawnTime;
    public float finalMaxSpawnTime;

    [Header("Solo para modo Infinito")]
    
    // Caunto aumenta la velocidad fuera de el descanso
    public float infiniteSpeedIncreasePerSecond;

    // Caunto baja el intervalo min/max de tiempo de generacion fuera de el descanso
    public float infiniteMinSpawnDecreasePerSecond;
    public float infiniteMaxSpawnDecreasePerSecond;

    // Limite minimo de intervalos de spawn para que no spawne en 0 segundos
    public float minimumMinSpawnTime;
    public float minimumMaxSpawnTime;

    // Duracion de cada descanso
    public float restDuration;

    // Tiempo de presion antes de el primer descaso
    public float initialRestSeparation;

    // Segundos que aumenta la separaciond e cada descanso
    public float restSeparationIncrementation;

    // Spawn Max/Min en fase de descanso
    public float restMinSpawnTime;
    public float restMaxSpawnTime;
}

// Guarda la configuracion de Unity para cambiar de escena.
public static class GameSession
{
    // Configuracion para BattleScene.
    public static BattleSettings CurrentSettings;

    // Tiempo que el jugador sobrevivio
    public static float LastSurvivedTime;

    // Puntaje final obtenido
    public static int LastScore;

    // Guarda la configuracion de el menu.
    public static void SetSettings(BattleSettings settings)
    {
        CurrentSettings = settings;
    }

    // Guarda el resultado final para LoseScene
    public static void SaveResult(float survivedTime, int score)
    {
        LastSurvivedTime = survivedTime;
        LastScore = score;
    }
}