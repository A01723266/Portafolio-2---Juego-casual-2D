using UnityEngine;

public class PipilaController : MonoBehaviour
{
    [Header("Life Controller")]
    [SerializeField] private LifeController lifeController;

    // Se ejecuta la funcion cuando detecta algo collisiondando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Aplicar animacion referenciando a el mismo objeto.
        GetComponent<Animator>().SetTrigger("Damage");

        // Llama a la funcion para perder una vida.
        lifeController.LoseLife();

        // Reproduce sonido de bala no bloqueada
        AudioManager.instance.PlayPipilaImpactSound();
        
        // Elimina el objeto que toco el collider
        Destroy(collision.gameObject);
        
        // debug de colision
        Debug.Log("Colision con Pipila: " + collision.gameObject.name);
    }
}