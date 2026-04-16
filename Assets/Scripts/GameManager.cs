using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject topViewCamera;
    [SerializeField] private GameObject customizationCanvas;
    [SerializeField] private GameObject gameplayCanvas;
    [SerializeField] private GameObject furnitureFather;
    [SerializeField] private GameObject teto;
    [SerializeField] private GameObject luzes;
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

        StartCoroutine(ActivateEnvironmentWithDelay(2.0f));

        foreach (Transform tr in furnitureFather.transform)
        {
            if(tr.GetComponent<SpaceSelector>() != null)
            {
                if (tr.GetComponent<BoxCollider>() != null) tr.GetComponent<BoxCollider>().enabled = false;
                if (tr.GetComponent<Renderer>() != null) tr.GetComponent<Renderer>().enabled = false;
            }
        }


    }

    private IEnumerator ActivateEnvironmentWithDelay(float delay)
    {
        // Espera o tempo solicitado
        yield return new WaitForSeconds(delay);

        // Ativa os objetos após o tempo passar
        if (teto != null) teto.SetActive(true);
        if (luzes != null) luzes.SetActive(true);
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
