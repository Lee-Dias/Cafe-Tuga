using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    [SerializeField] private GameObject[] Customers;
    private GameObject currentCustomer;

    // Update is called once per frame
    public void SpawnCustomer()
    {
        if (currentCustomer != null) {
            currentCustomer.GetComponent<CustomersController>().LeaveTheStore();
        }
        int a = Random.Range(0, Customers.Length-1);

        currentCustomer = Instantiate(Customers[a]);
    }
}
