using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]private bool shouldDestroyOncomplete = false;
    [SerializeField] private AudioClip audioClip;

    private AudioManager audioManager;
    private bool canBeInteracted = false;


    private Color defaultColor;

    public bool CanBeInteracted => canBeInteracted;

    private void Start()
    {
        audioManager = FindFirstObjectByType<AudioManager>();
        if (this.GetComponent<Renderer>() != null)
        {
            defaultColor = this.GetComponent<Renderer>().material.color;
        }
        else
        {
            defaultColor = GetComponentInChildren<Renderer>().material.color;
        }


        
    }
    public void ResetCompleted()
    {
    }
    public void SetAsCompleted()
    {
        if (shouldDestroyOncomplete)
        {
            this.GetComponentInParent<OrderPerItem>().SetObjectsToInteractable();
            audioManager.PlaySound(audioClip);
            Destroy(this.gameObject);
        }
        else
        {
            audioManager.PlaySound(audioClip);
            canBeInteracted = false;
            this.GetComponentInParent<OrderPerItem>().SetObjectsToInteractable();
            if(this.GetComponent<Renderer>() == null)
            {
                foreach (Transform transform in this.transform)
                {
                    if (transform.GetComponent<Renderer>() != null) transform.GetComponent<Renderer>().material.color = defaultColor;
                }
            }
            else
            {
                this.GetComponent<Renderer>().material.color = defaultColor;
            }
            
        }


        
    }
    public void SetCanBeInteractedToTrue()
    {
        canBeInteracted=true;
        ChangeMaterialColor(Color.cyan);
    }
    public void ChangeMaterialColor(Color color)
    {
        if (this.GetComponent<Renderer>() != null)
        {
            this.GetComponent<Renderer>().material.color = color;
        }
        else {
            foreach (Transform transform in this.transform) {
                if(transform.GetComponent<Renderer>() != null) transform.GetComponent<Renderer>().material.color = color;
            }
        }
            
    }
    void OnMouseEnter()
    {
        if (!canBeInteracted) return;
        ChangeMaterialColor(Color.red);
    }
    void OnMouseExit()
    {
        if (!canBeInteracted) return;
        ChangeMaterialColor(Color.cyan);
    }
    void OnMouseDown()
    {
        if (!canBeInteracted) return;
        SetAsCompleted();
    }
}
