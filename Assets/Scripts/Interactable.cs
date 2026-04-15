using UnityEngine;

public class Interactable : MonoBehaviour
{

    private bool canBeInteracted = false;


    private Color defaultColor;

    public bool CanBeInteracted => canBeInteracted;

    private void Start()
    {
        defaultColor = this.GetComponent<Renderer>().material.color;
    }
    public void ResetCompleted()
    {
    }
    public void SetAsCompleted()
    {
        canBeInteracted = false;
        this.GetComponentInParent<OrderPerItem>().SetObjectsToInteractable();
        this.GetComponent<Renderer>().material.color = defaultColor;
        
    }
    public void SetCanBeInteractedToTrue()
    {
        canBeInteracted=true;
        ChangeMaterialColor(Color.yellow);
    }
    public void ChangeMaterialColor(Color color)
    {
        this.GetComponent<Renderer>().material.color = color;
    }
}
