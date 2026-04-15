using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private SimpleMovement SimpleMovement;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject topViewCamera;
    [SerializeField] private GameObject customizationCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private SphereCollider spCollider;
    private OrdersManager ordersManager;
    private MapBuildManager mapBuildManager;
    private bool onCustomization = false;

    public bool OnCustomization => onCustomization; 
    public void StartGame()
    {
        if(mapBuildManager.FurnitureToPlace.Count > 0) return;
        SimpleMovement.enabled = true;
        mainCamera.SetActive(true);
        gameplayCanvas.SetActive(true);
        spCollider.enabled = true;
        customizationCanvas.SetActive(false);
        topViewCamera.SetActive(false);
        onCustomization = false;
        ordersManager.MakeOrder();


    }

    // Update is called once per frame
    void Start()
    {
        ordersManager = FindAnyObjectByType<OrdersManager>();
        GoToCustomization();
    }

    public void GoToCustomization()
    {
        mapBuildManager = FindFirstObjectByType<MapBuildManager>();
        SimpleMovement.enabled = false;
        mainCamera.SetActive(false);
        gameplayCanvas.SetActive(false);
        customizationCanvas.SetActive(true);
        topViewCamera.SetActive(true);
        spCollider.enabled = false;
        onCustomization = true;
    }
}
