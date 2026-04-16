using UnityEngine;
using UnityEngine.Splines;

public class CustomersController : MonoBehaviour
{
    [SerializeField]private SplineAnimate goToCounter;
    [SerializeField]private SplineAnimate leaveTheStore;
    [SerializeField] private SplineContainer go;
    [SerializeField] private SplineContainer leave;
    [SerializeField] private AudioClip audio;
    private AudioManager audioManager;
    private OrdersManager ordersManager;
    private bool didLeave = false;
    private bool madeOrder=false;
    private Animator animator;

    private void Awake()
    {
        ordersManager = FindFirstObjectByType<OrdersManager>();
        animator = GetComponent<Animator>();
        goToCounter.Container = go;
        leaveTheStore.Container = leave;
        goToCounter.Play();
        audioManager = FindFirstObjectByType<AudioManager>();


    }

    // Update is called once per frame
    public void LeaveTheStore()
    {
        didLeave = true;
        leaveTheStore.Play();

    }

    private void Update()
    {
        if (leaveTheStore.IsPlaying == true || goToCounter.IsPlaying == true)
        {
            animator.SetBool("isWalking", true);
        }
        else {
            animator.SetBool("isWalking", false);
        }

        if (didLeave && leaveTheStore.IsPlaying == false) {
            Destroy(this.gameObject);
        }
        if (goToCounter.IsPlaying == false && madeOrder == false) {
            audioManager.PlaySound(audio);
            madeOrder = true;
            ordersManager.MakeOrder();
        }
    }
}
