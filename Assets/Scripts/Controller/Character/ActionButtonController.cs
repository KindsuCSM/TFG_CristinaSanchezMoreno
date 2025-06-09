using UnityEngine;
using UnityEngine.EventSystems;

public class ActionButtonController : MonoBehaviour
{
    public PlayerController playerController;
    public TileManager tileManager;

    // Funcion que realiza una accion sobre las tiles, como plantar, regar o harar la tierra
    public void OnActionButtonPressed(string kindOfAction)
    {
        Vector3Int targetCell = playerController.GetTargetCell(); // Obtiene la tile sobre la que se va a realizar la acción
        tileManager.PerformTileAction(kindOfAction, targetCell); // Llamamos al manager creado para que realice la acción sobre la tile
    }
}