using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// ISelectHandler y IDeselectHandler, le indican a EventSystem los eventos de seleccion.
public class MenuButtonSelection : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    // Borde de el boton
    private Outline selectionOutile;

    // Se ejecuta al crear el objeto al que se le asigno el script.
    private void Awake()
    {
        // Aseguramos el outline de el objeto
        selectionOutile = GetComponent<Outline>();

        selectionOutile.enabled = false;
    }

    // Seleccion de boton con teclas, control o mouse. BaseEventData se utiliza para que el EventSystem ejecute esta funcion
    public void OnSelect(BaseEventData eventData)
    {
        // Activamos el outline de el boton seleccionado.
        selectionOutile.enabled = true;
    }

    // Cambio de boton con teclas, control o mouse. BaseEventData se utiliza para que el EventSystem ejecute esta funcion
    public void OnDeselect(BaseEventData eventData)
    {
        // Desactivamos al cambiar de boton.
        selectionOutile.enabled = false;
    }
    
}
