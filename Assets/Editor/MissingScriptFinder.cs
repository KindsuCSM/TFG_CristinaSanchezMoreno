using UnityEngine;
using UnityEditor;

public class MissingScriptsFinder
{
    [MenuItem("Tools/Buscar Scripts Faltantes en la Escena")]
    public static void FindMissingScripts()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        int count = 0;

        foreach (GameObject go in allObjects)
        {
            Component[] components = go.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] == null)
                {
                    Debug.LogWarning($"GameObject '{go.name}' tiene un script faltante en la posición {i}.", go);
                    count++;
                }
            }
        }

        Debug.Log($"Total de scripts faltantes encontrados: {count}");
    }
}