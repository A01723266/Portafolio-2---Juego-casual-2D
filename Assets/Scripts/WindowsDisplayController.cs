using UnityEngine;
using UnityEngine.InputSystem;

public class WindowsDisplayController : MonoBehaviour
{
    // Guarda la única instancia que debe existir del controlador.
    private static WindowsDisplayController instance;

    [Header("Resolución de la ventana")]
    [SerializeField] private int windowWidth = 1920;
    [SerializeField] private int windowHeight = 1080;

    private void Awake()
    {
        // Si ya existe otro DisplayManager, elimina esta copia.
        // Esto evita duplicados si vuelves a MenuScene.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Guarda esta instancia como la única válida.
        instance = this;

        // Hace que este objeto persista al cambiar de escena.
        // Así F11 funciona en Menu, Battle, Win y Lose.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Al iniciar el juego, abre una ventana fija de 1920 x 1080.
        // Esta resolución siempre tiene aspecto 16:9.
        SetWindowed();
    }

    private void Update()
    {
        // Detecta F11 con el New Input System.
        // Si se presiona, alterna entre ventana y pantalla completa.
        if (Keyboard.current != null &&
            Keyboard.current.f11Key.wasPressedThisFrame)
        {
            ToggleFullscreen();
        }
    }

    public void ToggleFullscreen()
    {
        // Si actualmente está en ventana, entra a fullscreen.
        if (Screen.fullScreenMode == FullScreenMode.Windowed)
        {
            SetFullscreen();
        }
        // Si ya está en fullscreen, regresa a ventana.
        else
        {
            SetWindowed();
        }
    }

    private void SetWindowed()
    {
        // Cambia a ventana usando la resolución configurada arriba.
        // Con 1920 x 1080 la UI conserva aspecto 16:9.
        Screen.SetResolution(
            windowWidth,
            windowHeight,
            FullScreenMode.Windowed
        );
    }

    private void SetFullscreen()
    {
        // Resolución de respaldo si no se encuentra otra 16:9.
        Resolution bestResolution = new Resolution
        {
            width = 1920,
            height = 1080
        };

        // Recorre las resoluciones disponibles del monitor.
        foreach (Resolution resolution in Screen.resolutions)
        {
            // Calcula la proporción de la resolución actual.
            float aspectRatio = (float)resolution.width / resolution.height;

            // Comprueba si es aproximadamente 16:9.
            bool isSixteenByNine =
                Mathf.Abs(aspectRatio - (16f / 9f)) < 0.01f;

            // Guarda la resolución 16:9 más grande encontrada.
            if (isSixteenByNine &&
                resolution.width > bestResolution.width)
            {
                bestResolution = resolution;
            }
        }

        // Entra a fullscreen con la mayor resolución 16:9 disponible.
        // ExclusiveFullScreen cambia la resolución de salida de Windows.
        Screen.SetResolution(
            bestResolution.width,
            bestResolution.height,
            FullScreenMode.ExclusiveFullScreen
        );
    }
}