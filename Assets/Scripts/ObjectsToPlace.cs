using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ObjectsToPlace", menuName = "Scriptable Objects/ObjectsToPlace")]
public class ObjectsToPlace : ScriptableObject
{
    public enum dificulty {one , two ,three }

    public string Name;
    public Sprite Icon;
    public GameObject Object;
    public int taskDificulty;

    
}
