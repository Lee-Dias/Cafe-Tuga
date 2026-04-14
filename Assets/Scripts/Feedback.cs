using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Importante adicionar este namespace

// Adicione a interface IPointerClickHandler
public class Feedback : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject[] objectsToActivateOnClick;
    [SerializeField] private GameObject parentOfObjectsToChangeColor;
    [SerializeField] private ColourPicker _colourPicker;
    [SerializeField] private Image _previewImage;

    private bool _turnedOn;

    private void Start() => _colourPicker.ColourChanged += colourHasChanged;

    private void colourHasChanged(Color newColour)
    {
        if (!_turnedOn)
        {
            _previewImage.enabled = true;
            _turnedOn = true;
        }
        _previewImage.color = newColour;
        foreach (Transform item in parentOfObjectsToChangeColor.transform) { 
            item.GetComponent<Renderer>().material.color = newColour;
        }
    }
    public bool GetState()
    {
        return objectsToActivateOnClick[0].activeSelf;
    }
    // Em vez de OnMouseDown, use OnPointerClick
    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var item in objectsToActivateOnClick) {
            item.SetActive(!item.activeSelf);
        }
    }
}