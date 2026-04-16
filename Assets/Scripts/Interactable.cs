using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]private bool shouldDestroyOncomplete = false; 
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
        if (shouldDestroyOncomplete){
            Destroy(this.gameObject);
        }
        
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
    void OnMouseEnter()
    {
        if (!canBeInteracted) return;
        ChangeMaterialColor(Color.red);
    }
    void OnMouseExit()
    {
        if (!canBeInteracted) return;
        ChangeMaterialColor(Color.yellow);
    }
    void OnMouseDown()
    {
        if (!canBeInteracted) return;
        SetAsCompleted();
    }
}
