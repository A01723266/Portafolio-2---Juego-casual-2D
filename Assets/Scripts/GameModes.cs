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
}

// Guarda la configuracion de Unity para cambiar de escena.
public static class GameSession
{
    // Configuracion para BattleScene.
    public static BattleSettings CurrentSettings;

    // Guarda la configuracion de el menu.
    public static void SetSettings(BattleSettings settings)
    {
        CurrentSettings = settings;
    }
}