using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskPanelView : MonoBehaviour
{
    public GameObject taskPanel;
    public TMP_InputField inputTaskName, inputTaskDate, inputTaskDescription;
    public Button btnSave, btnCancel, btnClose;

    public void Open() => taskPanel.SetActive(true);
    public void Close() => taskPanel.SetActive(false);

    public void ClearFields()
    {
        inputTaskName.text = "";
        inputTaskDate.text = "";
        inputTaskDescription.text = "";
    }

    public string GetTaskName() => inputTaskName.text;
    public string GetTaskDescription() => inputTaskDescription.text;
    public int GetTaskDays() => int.Parse(inputTaskDate.text);
}
