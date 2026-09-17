using UnityEngine;

public class StreetController : MonoBehaviour
{
    [Header("Calles")]

    // Primer tramo de calle
    [SerializeField] private Transform street1;

    // Segundo tramo de calle
    [SerializeField] private Transform street2;

    // Velocidad a la que baja la calle
    [SerializeField] private float streetSpeed = 2f;

    // Distancia entre las dos calles
    [SerializeField] private float streetLength = 36f;

    [Header("Animacion del Pipila")]

    // Objeto con Pipila y la piedra
    [SerializeField] private Transform pipilaParent;

    // Cambio de escala
    [SerializeField] private float scaleAmount = 0.02f;

    // Velocidad de la animacion
    [SerializeField] private float scaleSpeed = 6f;

    // Escala original del Pipila
    private Vector3 originalPipilaScale;

    private void Start()
    {
        // Guarda la escala original
        originalPipilaScale = pipilaParent.localScale;
    }

    private void Update()
    {
        MoveStreets();
        AnimatePipila();
    }

    // Mueve y recicla las calles
    private void MoveStreets()
    {
        MoveStreet(street1);
        MoveStreet(street2);
    }

    // Mueve un tramo de calle
    private void MoveStreet(Transform street)
    {
        // Mueve la calle hacia abajo
        street.position += Vector3.down * streetSpeed * Time.deltaTime;

        // Regresa la calle arriba cuando su posicion es -36
        if (street.position.y <= -streetLength)
        {
            street.position += Vector3.up * streetLength * 2f;
        }
    }

    // Anima el movimiento del Pipila
    private void AnimatePipila()
    {
        // Calcula el cambio de escala
        float scaleChange = Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;

        // Aplica la escala al Pipila
        pipilaParent.localScale = originalPipilaScale +
            new Vector3(scaleChange, scaleChange, 0f);
    }
}