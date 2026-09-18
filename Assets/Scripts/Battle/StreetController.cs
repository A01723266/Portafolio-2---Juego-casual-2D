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

    private void Update()
    {
        MoveStreets();
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
}