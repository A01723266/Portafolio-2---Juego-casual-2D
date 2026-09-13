using UnityEngine;
using UnityEngine.InputSystem;

public class PipilaYPiedraController : MonoBehaviour
{
    // Velocidad de giro
    [SerializeField] private float rotationSpeed = 1000f;

    // Guarda el angulo al que se quiere girar.
    private Quaternion targetRotation;

    private void Start()
    {
        // Define la orinetacion de el objetivo igual a la de el objeto para que no gire al iniciar.
        targetRotation = transform.rotation;
    }

    private void Update()
    {
        // Empieza a hacer una rotacion con el valor de rotacion que definimos hasta llegar a la rotacion objetivo.
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // Funcion que se llama cuando se precionan valores de Rotate en el Input System.
    public void OnRotate(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();

        // si no se preciona nada no camiba la orientacion.
        if (input == Vector2.zero)
            return;
            
        // Arriba
        if (input.y > 0)
            targetRotation = Quaternion.Euler(0, 0, 0);

        // Abajo
        else if (input.y < 0)
            targetRotation = Quaternion.Euler(0, 0, 180);
        
        // Isquierda
        else if (input.x < 0)
            targetRotation = Quaternion.Euler(0, 0, 90);
        
        // Derecha
        else if (input.x > 0)
            targetRotation = Quaternion.Euler(0, 0, -90);
    }
}
