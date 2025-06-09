using UnityEngine;
using UnityEngine.EventSystems;

public class RunningButtonView : MonoBehaviour, IPointerClickHandler
{
    public Animator animationButton;
    public RunningButtonController runningButtonController;

    public void OnPointerClick(PointerEventData eventData)
    {
        runningButtonController.ToggleRunningState();
    }

    public void UpdateButtonAnimation(bool isPressed)
    {
        animationButton.SetBool("isPressed", isPressed);
    }
}
