using UnityEngine;
using UnityEngine.SceneManagement;

public class WinSceneController : MonoBehaviour
{
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
