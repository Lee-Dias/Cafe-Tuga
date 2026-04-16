using UnityEngine;
using UnityEngine.Splines;

public class CustomersController : MonoBehaviour
{
    [SerializeField]private SplineAnimate goToCounter;
    [SerializeField]private SplineAnimate leaveTheStore;
    [SerializeField] private SplineContainer go;
    [SerializeField] private SplineContainer leave;
    private OrdersManager ordersManager;
    private bool didLeave = false;
    private bool madeOrder=false;

    private void Awake()
    {
        ordersManager = FindFirstObjectByType<OrdersManager>();
        goToCounter.Container = go;
        leaveTheStore.Container = leave;
        goToCounter.Play();

        
    }

    // Update is called once per frame
    public void LeaveTheStore()
    {
        didLeave = true;
        leaveTheStore.Play();

    }

    private void Update()
    {
        if (didLeave && leaveTheStore.IsPlaying == false) {
            Destroy(this.gameObject);
        }
        if (goToCounter.IsPlaying == false && madeOrder == false) { 
            madeOrder = true;
            ordersManager.MakeOrder();
        }
    }
}
