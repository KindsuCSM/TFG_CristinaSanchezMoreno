using UnityEngine;

public class RunningButtonController : MonoBehaviour
{
    public PlayerController playerController;
    public RunningButtonView runningButtonView;

    private bool isRunning = false;

    // Cambiamos el estdo del jugador en caso de que corra ademas de cambiar el btn
    public void ToggleRunningState()
    {
        isRunning = !isRunning;

        playerController.SetRunningState(isRunning);
        runningButtonView.UpdateButtonAnimation(isRunning);
    }
}
