using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Duracion de la partida")]
    [SerializeField] private float timedModeDuration = 90f;

    [Header("Configuración de Fácil")]
    [SerializeField] private BattleSettings easySettings = new BattleSettings
    {
        gameMode = GameMode.Easy,
        initialBulletSpeed = 4f,
        maximumBulletSpeed = 7f,
        initialMinSpawnTime = 1.2f,
        initialMaxSpawnTime = 2f,
        finalMinSpawnTime = 0.6f,
        finalMaxSpawnTime = 1f
    };


    [Header("Configuración de Difícil")]
    [SerializeField] private BattleSettings hardSettings = new BattleSettings
    {
        gameMode = GameMode.Hard,
        initialBulletSpeed = 6f,
        maximumBulletSpeed = 10f,
        initialMinSpawnTime = 0.7f,
        initialMaxSpawnTime = 1.2f,
        finalMinSpawnTime = 0.25f,
        finalMaxSpawnTime = 0.6f
    };


    [Header("Configuración de Infinito")]
    [SerializeField] private BattleSettings infiniteSettings = new BattleSettings
    {
        gameMode = GameMode.Infinite,
        initialBulletSpeed = 4.5f,
        maximumBulletSpeed = 12f,
        initialMinSpawnTime = 1f,
        initialMaxSpawnTime = 1.8f,
        finalMinSpawnTime = 0.2f,
        finalMaxSpawnTime = 0.55f
    };

    public void SelectEasyMode()
    {
        StartGame(easySettings, GameMode.Easy);
    }

    public void SelectHardMode()
    {
        StartGame(hardSettings, GameMode.Hard);
    }

    public void SelectInfiniteMode()
    {
        StartGame(infiniteSettings, GameMode.Infinite);
    }

    // Guarda la configuracion de el modo y abre la proxima escena.
    private void StartGame(BattleSettings selectedSettings, GameMode selectedMode)
    {
        // Guarda modo seleccionado
        selectedSettings.gameMode = selectedMode;

        // Guarda el tiempo definido
        if (selectedMode != GameMode.Infinite)
            selectedSettings.timeLimit = timedModeDuration;
        
        // Enviamos los datos a BattleScene
        GameSession.SetSettings(selectedSettings);

        // Cambiamos de Scene a "BattleScene"
        SceneManager.LoadScene("BattleScene");
    } 
}
