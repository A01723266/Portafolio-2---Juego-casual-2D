using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Instancia unica del audio manager
    public static AudioManager instance;

    [Header("Audios")]

    // Musica que suena durante el juego
    [SerializeField] private AudioClip gameMusic;

    // Sonido al seleccionar un boton
    [SerializeField] private AudioClip selectSound;

    // Sonido al bloquear una bala con losa
    [SerializeField] private AudioClip rockImpactSound;

    // Sonido al recibir una bala pipila
    [SerializeField] private AudioClip pipilaImpactSound;

    // Componente que reproduce todos los audios
    private AudioSource audioSource;

    private void Awake()
    {
        // Evita que existan dos Audio Manager
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Guarda este Audio Manager
        instance = this;

        // Mantiene el objeto al cambiar de escena
        DontDestroyOnLoad(gameObject);

        // Obtiene el Audio Source del objeto
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Define la musica principal
        audioSource.clip = gameMusic;

        // Repite la musica al terminar
        audioSource.loop = true;

        // Inicia la musica
        audioSource.Play();
    }

    // Reproduce el sonido de seleccion
    public void PlaySelectSound()
    {
        audioSource.PlayOneShot(selectSound);
    }

    // Reproduce el sonido de bala bloqueada
    public void PlayRockImpactSound()
    {
        audioSource.PlayOneShot(rockImpactSound);
    }

    // Reproduce el sonido de bala no bloqueada
    public void PlayPipilaImpactSound()
    {
        audioSource.PlayOneShot(pipilaImpactSound);
    }
}