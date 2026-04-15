using System.Collections.Generic;
using UnityEngine;

public class MapBuildManager : MonoBehaviour
{
    [SerializeField] private GameObject blankSpaceObject;
    [SerializeField] private List<ObjectsToPlace> furnitureToPlace;
    [SerializeField] private GameObject furnitureParent;

    private ObjectsToPlace selectedGameObject;

    private FurnitureUiManager furnitureUiManager;
    public GameObject FurnitureParent => furnitureParent;
    public List<ObjectsToPlace> FurnitureToPlace => furnitureToPlace;
    public GameObject BlankSpaceObject => blankSpaceObject;
    public ObjectsToPlace SelectedGameObject => selectedGameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        furnitureUiManager = FindFirstObjectByType<FurnitureUiManager>();
    }
    public void SelectGameObject(ObjectsToPlace objectsToPlace)
    {
        selectedGameObject = objectsToPlace;

        
    }

    public void RemoveFromList(ObjectsToPlace obj)
    {
        furnitureToPlace.Remove(obj);
        furnitureUiManager.UpdateButtons();
    }
    public void AddToList(ObjectsToPlace obj)
    {
        furnitureToPlace.Add(obj);
        furnitureUiManager.UpdateButtons();
    }

}
