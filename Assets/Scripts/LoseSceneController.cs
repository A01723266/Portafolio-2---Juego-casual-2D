using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseSceneController : MonoBehaviour
{
    [Header("Textos del resultado")]
    [SerializeField] private Text resultTitle;
    [SerializeField] private Text resultValue;

    private void Start()
    {
        // verifica que haya partida guardada
        if (GameSession.CurrentSettings == null)
        {
            Debug.LogError("No hay resultados de partidas");
            return;
        }

        // Modo infinito muestra puntaje final
        if(GameSession.CurrentSettings.gameMode == GameMode.Infinite)
        {
            resultTitle.text = "Puntaje Obtenido";
            resultValue.text = GameSession.LastScore + " puntos";

            return;
        }

        // Modo Facil/Dificil muestra el tiempo sobrevivido
        resultTitle.text = "Tiempo Sobrevivido";
        resultValue.text = FormatSurvivedTime(GameSession.LastSurvivedTime);
    }

    private string FormatSurvivedTime(float survivedTime)
    {
        // Usamos un numero entero de segundos
        int totalSeconds = Mathf.FloorToInt(survivedTime);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        return minutes + " min " + seconds + "seg";
    }

    // Reinicia BattleScene con el mismo modo y configuración.
    public void RestartBattle()
    {
        // Conserva los datos seleccionados de el modo de juego.
        if (GameSession.CurrentSettings == null)
        {
            ReturnToMenu();

            return;
        }

        // Asegura que el juego no se quede pausado.
        Time.timeScale = 1f;

        // Abre BattleScene
        SceneManager.LoadScene("BattleScene");
    }


    // Regresa al menú para elegir un modo nuevo.
    public void ReturnToMenu()
    {
        // Asegura que el juego no se quede pausado.
        Time.timeScale = 1f;

        // Abre MenuScene
        SceneManager.LoadScene("MenuScene");
    }
}
