using UnityEngine;

public class RockController : MonoBehaviour
{
    [Header("Counter Controller")]
    [SerializeField] private CounterController counterController;

    // Se ejecuta la funcion cuando detecta algo collisiondando
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Suma un punto en Counter Controller
        counterController.AddPoint();

        // Elimina el objeto que toco el collider
        Destroy(collision.gameObject);

        // Debug de colision
        Debug.Log("Colision con piedra: " + collision.gameObject.name);
    }
}
