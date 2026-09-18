using UnityEngine;
using UnityEngine.SceneManagement;

public class PipilaYLosaAnimationEvent : MonoBehaviour
{
    // Cambia a la escena de derrota al terminar la animación.
    public void LoadLoseScene()
    {
        SceneManager.LoadScene("LoseScene");
    }
}