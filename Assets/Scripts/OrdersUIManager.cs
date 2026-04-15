using UnityEngine;
using UnityEngine.UI;

public class OrdersUIManager : MonoBehaviour
{
    [SerializeField] private GameObject whereToPutOrder;
    [SerializeField] private Image imagePrefab;
    private OrdersManager ordersManager;
    void Start()
    {
        ordersManager = GetComponent<OrdersManager>();
    }

    // Update is called once per frame
    public void UpdateOrder()
    {
        foreach(Transform go in whereToPutOrder.transform)
        {
            Destroy(go.gameObject);
        }
        foreach(ObjectsToPlace objectsToPlace in ordersManager.FinalOrder)
        {
            Image go = Instantiate(imagePrefab, whereToPutOrder.transform);
            go.sprite = objectsToPlace.Icon;
        }
    }
}
