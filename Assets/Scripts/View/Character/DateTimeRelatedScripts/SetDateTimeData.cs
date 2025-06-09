using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Globalization;

public class SetDateTimeData : MonoBehaviour
{
    public TextMeshProUGUI txtDate;
    public TextMeshProUGUI txtTime;
    public TextMeshProUGUI txtMoney;
    private PlayerController playerController;

    private DateTime currentDay;
    void Start()
    {
        playerController = PlayerController.Instance.GetComponent<PlayerController>();
    }
    void Update()

    {
        setTextTime();
        setTextDate();
        setMoneyData();
    }

    private void setTextTime()
    {
        //Recogemos la fecha y hora actual
        DateTime dateTime = DateTime.Now;

        // Personalizo el formato de la fecha
        string formattedPersonalTime = dateTime.ToString("hh:mm tt");

        // Muestro la fecha personalizada en su respectivo Text-MeshPro
        txtTime.SetText(formattedPersonalTime);
    }

    private void setTextDate()
    {
        // Recogemos la fecha y hora actual
        DateTime dateTime = DateTime.Now;
        // Personalizo el idioma de la fecha a ingles
        CultureInfo englishDate = new CultureInfo("en-US");
        // string dateFormat = date.ToString('D', englishDate);

        // Personalizo el formato de la fecha
        string formattedPersonalDate = dateTime.ToString("ddd, dd MMM", englishDate);

        // Muestro la fecha personalizada en su respectivo Text-MeshPro
        txtDate.SetText(formattedPersonalDate);
    }

    // Mostramos en su respectivo txt de la UI el dinero del jugador 
    private void setMoneyData()
    {
        txtMoney.SetText(playerController.GetMoney().ToString());
    }
}
