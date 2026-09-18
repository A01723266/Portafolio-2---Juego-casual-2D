using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [Header("Imágenes de vidas")]

    // Variables para las vidas
    [SerializeField] private Image life1;
    [SerializeField] private Image life2;
    [SerializeField] private Image life3;

    [Header("Controladores de partida")]

    // Se usa para detener nuevas balas al perder.
    [SerializeField] private BulletController bulletController;

    // Se usa para guardar tiempo sobrevivido o puntaje final.
    [SerializeField] private CounterController counterController;

    // Controlador de animacion de la calle
    [SerializeField] private StreetController streetController;

    [Header("Objeto con Animation Controller")]

    // Animator del padre para activar la animación de muerte.
    [SerializeField] private Animator pipilaYLosaAnimator;

    [Header("Derrota")]

    // Color que tendrá una vida perdida.
    [SerializeField] private Color lostLifeColor = Color.black;

    // Vidas actuales del jugador.
    private int currentLives = 3;

    // Evita perder más vidas durante los 5 segundos finales.
    private bool isDefeated;

    // Esta función será llamada por PipilaRotation cuando una bala impacte.
    public void LoseLife()
    {
        // Ya no hay vidas.
        if (isDefeated)
        {
            return;
        }

        // Restamos una vida.
        currentLives--;

        // Las vidas se pierden de derecha a izquierda
        if (currentLives == 2)
        {
            life3.color = lostLifeColor;
        }
        else if (currentLives == 1)
        {
            life2.color = lostLifeColor;
        }
        else if (currentLives == 0)
        {
            life1.color = lostLifeColor;

            // Inicia la derrota final.
            DefeatSequence();
        }
    }

    // Guarda el resultado, espera 5 segundos y abre LoseScene.
    private void DefeatSequence()
    {
        // Ya no pueden quitarse más vidas.
        isDefeated = true;

        // Detiene la animacion de calle y Pipila
        streetController.enabled = false;

        // Guardamos ambos datos. LoseScene decidirá cuál mostrar
        // dependiendo de si el modo fue por tiempo o Infinito.
        GameSession.SaveResult(
            counterController.survivedTime,
            counterController.score
        );

        // Detiene únicamente la generación de nuevas balas.
        bulletController.SetSpawning(false);

        // Activa la animación final de muerte.
        pipilaYLosaAnimator.SetTrigger("Death");
    }
}
