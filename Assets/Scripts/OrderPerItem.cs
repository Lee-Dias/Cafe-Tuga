using UnityEngine;

public class OrderPerItem : MonoBehaviour
{
    [SerializeField]private GameObject[] itemsToCompleteOrder;

    [SerializeField]private bool balcao = false;

    private GameObject balcaoObj;
    private OrdersManager ordersManager;
    private bool toBeMade = false;

    private bool completed = false;
    private int amount=0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ordersManager = FindFirstObjectByType<OrdersManager>();

    }

    public void ChangeToBeMadeState(bool b)
    {
        toBeMade = b;
        if (toBeMade){
            SetObjectsToInteractable();
        }
    }
    public void SetObjectsToInteractable()
    {

        if (amount < itemsToCompleteOrder.Length) {
            if(amount -1 >= 0)
            {
                if (itemsToCompleteOrder[amount - 1].GetComponent<Interactable>().CanBeInteracted) return;
            }
            itemsToCompleteOrder[amount].GetComponent<Interactable>().SetCanBeInteractedToTrue();
            amount += 1;
            return;
        }

            
        amount = 0;
        toBeMade=false;
        if (balcao)
        {
            ordersManager.TakeFromOrder();
        }
        else {
            balcaoObj = GameObject.FindWithTag("Balcao");
            balcaoObj.GetComponent<OrderPerItem>().ChangeToBeMadeState(true);
        }
        foreach (GameObject item in itemsToCompleteOrder)
        {
            item.GetComponent<Interactable>().ResetCompleted();
        }
    }
}
