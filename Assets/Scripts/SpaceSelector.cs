using UnityEngine;

public class SpaceSelector : MonoBehaviour
{
    private Color originalColor;
    private Renderer objRenderer;
    private MapBuildManager mapBuildManager;
    private ObjectsToPlace whatFurniture = null;


    [Header("References")]
    [SerializeField] private bool isBlankSpace = false;

    [Header("Configurações de Cor")]
    [SerializeField]private Color highlightColor = Color.green; // Cor ao passar o rato



    public void ChangeWhatFurniture(ObjectsToPlace otp)
    {
        whatFurniture = otp;
    }

    void Start()
    {
        mapBuildManager = FindFirstObjectByType<MapBuildManager>();
        objRenderer = GetComponent<Renderer>();
        originalColor = objRenderer.material.color;
    }
    void OnMouseEnter()
    {
        objRenderer.material.color = highlightColor;
    }

    void OnMouseExit()
    {
        objRenderer.material.color = originalColor;
    }

    void OnMouseDown()
    {
        ExecutarAcao();
    }

    void ExecutarAcao()
    {
        // Verifica se existe algo selecionado para evitar erros
        if (mapBuildManager.SelectedGameObject != null && isBlankSpace)
        {
            // Criamos o objeto na posição (transform.position) e rotação (transform.rotation) deste cubo
            GameObject newPlaced = Instantiate(mapBuildManager.SelectedGameObject.Object, transform.position, transform.rotation);

            newPlaced.GetComponent<SpaceSelector>().ChangeWhatFurniture(mapBuildManager.SelectedGameObject);

            mapBuildManager.RemoveFromList(mapBuildManager.SelectedGameObject);
            mapBuildManager.SelectGameObject(null);
            // Destruímos o cubo vazio
            Destroy(this.gameObject);
        }
        else
        {
            if (!isBlankSpace) 
            { 
                // Criamos o objeto na posição (transform.position) e rotação (transform.rotation) deste cubo
                Instantiate(mapBuildManager.BlankSpaceObject, transform.position, transform.rotation);
                mapBuildManager.AddToList(whatFurniture);
                // Destruímos o cubo vazio
                Destroy(this.gameObject);            
            }
        }

    }
}