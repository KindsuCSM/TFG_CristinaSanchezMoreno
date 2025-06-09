using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "PlantData", menuName = "Scripts/Items/PlantsData")]
public class PlantsData : ScriptableObject
{
    [SerializeField] private string plantId;
    [SerializeField] private Sprite icon;
    [SerializeField] private string plantName;
    [SerializeField] private string plantDescription;
    [SerializeField] private int cost;
    [SerializeField] private bool isStackable;
    [SerializeField] private Sprite[] growSprites;

    public string PlantId { get { return plantId; } }
    public Sprite Icon { get { return icon; } }
    public string PlantName { get { return plantName; } }
    public string PlantDescription { get { return plantDescription; } }
    public int Cost { get { return cost; } }
    public bool IsStackable { get { return isStackable; } }
    public Sprite[] GrowSprites { get { return growSprites; } }


#if UNITY_EDITOR
    private void OnEnable()
    {
        if (string.IsNullOrEmpty(plantId))
        {
            plantId = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}



