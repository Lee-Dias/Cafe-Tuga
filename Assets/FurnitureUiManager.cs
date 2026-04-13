using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FurnitureUiManager : MonoBehaviour
{
    [SerializeField] private GameObject panelWithButtons;
    [SerializeField] private Toggle buttonPrefab;
    [SerializeField] private ToggleGroup toggleGroup;


    private MapBuildManager MapBuildManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MapBuildManager = FindFirstObjectByType<MapBuildManager>();
        UpdateButtons();
    }
    public void UpdateButtons()
    {
        ObjectsToPlace objetoAnterior = MapBuildManager.SelectedGameObject;

        // Limpamos o painel
        foreach (Transform child in panelWithButtons.transform)
        {
            Destroy(child.gameObject);
        }

        // Recriamos a lista
        foreach (ObjectsToPlace go in MapBuildManager.FurnitureToPlace)
        {
            Toggle newToggle = Instantiate(buttonPrefab, panelWithButtons.transform);
            newToggle.group = toggleGroup;
            newToggle.image.sprite = go.Icon;

            // 2. VERIFICAÇÃO: Se este objeto é o que estava selecionado antes, ativamos o Toggle
            if (objetoAnterior != null && go == objetoAnterior)
            {
                // Importante: setar o isOn ANTES de adicionar o listener para evitar loops infinitos
                // ou garantir que a lógica de seleção seja disparada corretamente.
                newToggle.isOn = true;
            }
            else
            {
                newToggle.isOn = false;
            }
            newToggle.onValueChanged.AddListener((bool isOn) => {
            if (isOn)
            {
                MapBuildManager.SelectGameObject(go);
            }
            else
            {
                if (!toggleGroup.AnyTogglesOn())
                {
                    MapBuildManager.SelectGameObject(null);

                    if (EventSystem.current != null && EventSystem.current.currentSelectedGameObject == newToggle.gameObject)
                    {
                        EventSystem.current.SetSelectedGameObject(null);
                    }
                }
            }
            });       
            newToggle.image.sprite = go.Icon;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
