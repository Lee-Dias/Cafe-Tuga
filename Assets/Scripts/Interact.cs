using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    private GameObject objectToPickUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>() == null) return;

        if (other.GetComponent<Interactable>().CanBeInteracted)
        {
            other.GetComponent<Interactable>().ChangeMaterialColor(Color.red);
            objectToPickUp = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interactable>() == null) return;
        Debug.DrawLine(transform.position, other.transform.position, Color.yellow);

        Debug.Log("Colidindo com: " + other.name);

        if (other.GetComponent<Interactable>().CanBeInteracted)
        {
            other.GetComponent<Interactable>().ChangeMaterialColor(Color.red);
             objectToPickUp = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == objectToPickUp)
        {
            if (other.GetComponent<Interactable>().CanBeInteracted)
            {
                other.GetComponent<Interactable>().ChangeMaterialColor(Color.yellow);
                objectToPickUp = null;
            }
        }
    }

    public void OnInteract()
    {

        if (objectToPickUp != null)
        {
            objectToPickUp.GetComponent<Interactable>().SetAsCompleted();
            objectToPickUp = null;
        }
        
    }
}
