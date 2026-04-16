using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject topViewCamera;
    [SerializeField] private GameObject customizationCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    private CustomersManager customersManager;
    private MapBuildManager mapBuildManager;
    private bool onCustomization = false;

    public bool OnCustomization => onCustomization; 
    public void StartGame()
    {
        if(mapBuildManager.FurnitureToPlace.Count > 0) return;
        gameplayCanvas.SetActive(true);
        customizationCanvas.SetActive(false);
        topViewCamera.SetActive(false);
        onCustomization = false;
        customersManager.SpawnCustomer();


    }

    // Update is called once per frame
    void Start()
    {
        customersManager = FindAnyObjectByType<CustomersManager>();
        GoToCustomization();
    }

    public void GoToCustomization()
    {
        mapBuildManager = FindFirstObjectByType<MapBuildManager>();
        gameplayCanvas.SetActive(false);
        customizationCanvas.SetActive(true);
        topViewCamera.SetActive(true);
        onCustomization = true;
    }
}
