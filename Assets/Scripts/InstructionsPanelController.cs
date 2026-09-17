using UnityEngine;
using UnityEngine.EventSystems;

public class InstructionsPanelController : MonoBehaviour
{
    [Header("Panel del libro")]

    // Panel que muestra las instrucciones
    [SerializeField] private GameObject instructionsPanel;

    [Header("Botones")]

    // Boton que abre el libro
    [SerializeField] private GameObject infoButton;

    // Boton que cierra el libro
    [SerializeField] private GameObject closeButton;

    // Abre el libro de instrucciones
    public void OpenInstructions()
    {
        // Activa el panel del libro
        instructionsPanel.SetActive(true);

        // Selecciona el boton X
        EventSystem.current.SetSelectedGameObject(closeButton);
    }

    // Cierra el libro de instrucciones
    public void CloseInstructions()
    {
        // Desactiva el panel del libro
        instructionsPanel.SetActive(false);

        // Regresa la seleccion al boton ?
        EventSystem.current.SetSelectedGameObject(infoButton);
    }
}