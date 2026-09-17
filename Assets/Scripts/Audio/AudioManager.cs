using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Instancia unica del audio manager
    public static AudioManager instance;

    [Header("Musica")]

    // Lista de canciones "Playlist"
    [SerializeField] private AudioClip[] gameMusic;

    [Header("Efectos de sonido")]

    // Sonido al seleccionar un boton
    [SerializeField] private AudioClip selectSound;

    // Sonido al bloquear una bala con losa
    [SerializeField] private AudioClip rockImpactSound;

    // Sonido al recibir una bala pipila
    [SerializeField] private AudioClip pipilaImpactSound;

    [Header("Audio Sources")]

    // Componente que reproduce musica
    [SerializeField] private AudioSource musicSource;

    // Componente que reproduce musica
    [SerializeField] private AudioSource soundEffectsSource;

    [Header("Volumen")]
    // Volumen para la musica
    [SerializeField] private float musicVolume = 0.7f;
    // Volumen para los efectos de sonido.
    [SerializeField] private float soundEffectsVolume = 1f;

    // Guarda la posicion de la cancion.
    private int currentSongIndex;

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
    }

    private void Start()
    {
        // Si no hay canciones, no hace nada.
        if (gameMusic == null || gameMusic.Length == 0)
        {
            Debug.LogWarning("No hay canciones asignadas en Game Music.");
            return;
        }

        // Elige una cancion aleatioria.
        currentSongIndex = Random.Range(0, gameMusic.Length);

        // Configura los volumenes iniciales.
        musicSource.volume = musicVolume;
        soundEffectsSource.volume = soundEffectsVolume;

        // La muscia se va a repetir con el codigo.
        musicSource.loop = false;

        // Reproduce la cancion elegida.
        PlayCurrentSong();
    }

    private void Update()
    {
        // CUando la cancion termina, isPlaying pasa a false.
        if (musicSource.isPlaying == false && gameMusic.Length > 0)
        {
            // Pasa a la sigueinte cancion
            // Usa % para siempre usar el residuo asi cuando llegamos a el maximo el resuduo va a ser 0 y se va a reiniciar
            currentSongIndex = (currentSongIndex + 1) % gameMusic.Length;

            // reproducir cancion
            PlayCurrentSong();
        }
    }

    private void PlayCurrentSong()
    {
        // pone o camibia la cancion en el audio source
        musicSource.clip = gameMusic[currentSongIndex];

        // inicia cancion
        musicSource.Play();
    }

    // Asignar volumen a el audio soruce de muscia
    private void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        musicSource.volume = musicVolume;
    }

    // Asignar volumen a el audio soruce de sound effect
    private void SetSoundEffectVolume(float newVolume)
    {
        soundEffectsVolume = newVolume;
        soundEffectsSource.volume = soundEffectsVolume;
    }

    // Reproduce el sonido de seleccion
    public void PlaySelectSound()
    {
        soundEffectsSource.PlayOneShot(selectSound);
    }

    // Reproduce el sonido de bala bloqueada
    public void PlayRockImpactSound()
    {
        soundEffectsSource.PlayOneShot(rockImpactSound);
    }

    // Reproduce el sonido de bala no bloqueada
    public void PlayPipilaImpactSound()
    {
        soundEffectsSource.PlayOneShot(pipilaImpactSound);
    }
}