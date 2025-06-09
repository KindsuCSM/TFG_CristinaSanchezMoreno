using UnityEngine;

public class DirectionButtonController : MonoBehaviour
{
    public PlayerController playerController;

    // Funcion que manda al script del jugador el movimiento que está realizando
    public void OnDirectionPressed(string direction, bool isPressed)
    {
        playerController.PlayerMove(direction, isPressed);
    }
}
