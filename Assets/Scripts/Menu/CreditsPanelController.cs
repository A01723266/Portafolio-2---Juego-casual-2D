using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsPanelController : MonoBehaviour
{
    [Header("Panel de Creditos")]

    // Panel que muestra los Creditos
    [SerializeField] private GameObject creditsPanel;

    [Header("Botones")]

    // Boton que abre Creditos
    [SerializeField] private GameObject creditsButton;

    // Boton que cierra Creditos
    [SerializeField] private GameObject creditsCloseButton;

    // Abre el libro de instrucciones
    public void OpenCredits()
    {
        // Activa el panel del libro
        creditsPanel.SetActive(true);

        // Selecciona el boton X
        EventSystem.current.SetSelectedGameObject(creditsCloseButton);
    }

    // Cierra el libro de instrucciones
    public void CloseCredits()
    {
        // Desactiva el panel del libro
        creditsPanel.SetActive(false);

        // Regresa la seleccion al boton ?
        EventSystem.current.SetSelectedGameObject(creditsButton);
    }
}