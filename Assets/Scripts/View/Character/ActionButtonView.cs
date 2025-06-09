using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButtonView : MonoBehaviour, IPointerDownHandler
{
    // Se le asigna en Unity el tipo de acción
    public string kindOfAction;

    // Referencia al controlador encargado de manejar el evento
    public ActionButtonController actionButtonController;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (Time.timeScale != 0)
        {
            actionButtonController.OnActionButtonPressed(kindOfAction);
        }
    }
}
