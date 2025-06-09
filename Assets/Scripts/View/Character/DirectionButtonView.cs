using UnityEngine;
using UnityEngine.EventSystems;
public class DirectionButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string direction;
    public DirectionButtonController directionButtonController;

    public void OnPointerDown(PointerEventData eventData)
    {
        directionButtonController.OnDirectionPressed(direction, true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        directionButtonController.OnDirectionPressed(direction, false);
    }
}
