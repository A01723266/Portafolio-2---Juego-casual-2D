using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Prefab de la bala que se va a crear.
    [SerializeField] private GameObject bulletPrefab;

    // Los 4 puntos donde pueden aparecer las balas.
    [SerializeField] private Transform spawnPointUp;
    [SerializeField] private Transform spawnPointDown;
    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;

    // Velocidad con la que se moverá la bala.
    [SerializeField] private float bulletSpeed = 5f;



    // Tiempo minimo y maximo entre cada bala
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] private float maxSpawnTime = 3f;

    // Guarda el ultimo punto de spawn usado, se va a usar para evitar que salgan 2 balas en la misma ubicacion.
    private int lastSpawn = -1;

    // Tiempo entre cada bala
    private float currentspawnTime;

    // Temporizador para saber cuando crear cada bala.
    private float timer;

    private void Start()
    {
        // Al Iniciar el juego, elegimos el primer tiempo aleatorio.
        SetRandomSpawnTime();
    }

    private void Update()
    {
        // Aumenta el contador usando tiempo real
        timer += Time.deltaTime;
        
        // crea balas con el intervalo de spawn
        if (timer >= currentspawnTime)
        {
            // LLamamos a la funcion para crear bala
            SpawnBullet();

            // Reiniciar contador
            timer = 0f;

            //Elegimos un nuevo tiempo aleatorio para la sigueinte bala.
            SetRandomSpawnTime();
        }
    }

    private void SetRandomSpawnTime()
    {
        // Elegir nuemro aleatoroio en los limites definidos.
        currentspawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }

    private void SpawnBullet()
    {
        // Elegimos un nuemro aleatorio entre 0 y 3
        int randomSpawn = Random.Range(0,4);

        // Si sale el mismo spawn que la bala anterior, volvemos a elegir hasta que sea diferente.
        while (randomSpawn == lastSpawn)
        {
            randomSpawn = Random.Range(0, 4);
        }

        // Guardamos este spawn como el ultimo usado.
        lastSpawn = randomSpawn;
        
        // Guardamos el punto donde va a aparecer la bala.
        Transform selectedSpawnPoint = spawnPointUp;

        // Se define el spawn point aleatoriamente.
        if (randomSpawn == 0)
        {
            selectedSpawnPoint = spawnPointUp;
        }
        else if (randomSpawn == 1)
        {
            selectedSpawnPoint = spawnPointDown;
        }
        else if (randomSpawn == 2)
        {
            selectedSpawnPoint = spawnPointLeft;
        }
        else if (randomSpawn == 3)
        {
            selectedSpawnPoint = spawnPointRight;
        }

        // Crea la bala en la posición y rotación del SpawnPoint seleccionado.
        GameObject bullet = Instantiate(
            bulletPrefab,
            selectedSpawnPoint.position,
            selectedSpawnPoint.rotation
        );

        // Buscamos el Rigidbody2D de la bala.
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Calculamos la direccion desde el SpawnPoint hacia el centro del mapa.
        Vector2 direction = (Vector2.zero - (Vector2)selectedSpawnPoint.position).normalized;

        // Le damos velocidad a la bala hacia el centro.
        rb.linearVelocity = direction * bulletSpeed;

    }

    
}
