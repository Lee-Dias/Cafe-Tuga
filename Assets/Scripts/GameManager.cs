using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private SimpleMovement SimpleMovement;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject topViewCamera;
    [SerializeField] private GameObject customizationCanvas;
    private MapBuildManager mapBuildManager;
    private bool onCustomization = false;

    public bool OnCustomization => onCustomization; 
    public void StartGame()
    {
        if(mapBuildManager.FurnitureToPlace.Count > 0) return;
        SimpleMovement.enabled = true;
        mainCamera.SetActive(true);
        customizationCanvas.SetActive(false);
        topViewCamera.SetActive(false);
        onCustomization = false;
    }

    // Update is called once per frame
    void Start()
    {
        GoToCustomization();
    }

    public void GoToCustomization()
    {
        mapBuildManager = FindFirstObjectByType<MapBuildManager>();
        SimpleMovement.enabled = false;
        mainCamera.SetActive(false);
        customizationCanvas.SetActive(true);
        topViewCamera.SetActive(true);
        onCustomization = true;
    }
}
