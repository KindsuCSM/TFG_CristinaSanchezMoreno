using UnityEngine;

public class OpenShopScript : MonoBehaviour
{
    // Esta clase recoge un GameObject vacío que tiene un Box Collider 2D que ejecutará el que se 
    // abra la tienda en cuanto el objeto del jugador entre o salga de la zona especificada
    public GameObject shopPanel;

    // En cuanto entre el panel de la tienda se activará
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Entramo a la tienda");
            shopPanel.SetActive(true);
        }
    }

    // En cuanto salga de la zona el panel de la tienda se cerrará
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Salimo de la tienda");
            shopPanel.SetActive(false);
        }
    }
}
