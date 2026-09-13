using UnityEngine;

public class PiedraController : MonoBehaviour
{
    // Se ejecuta la funcion cuando detecta algo collisiondando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Elimina el objeto que toco el collider
        Destroy(collision.gameObject);
        // debug de colision
        Debug.Log("Colision con piedra: " + collision.gameObject.name);
    }
}
