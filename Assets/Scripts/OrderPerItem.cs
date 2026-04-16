using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStepData
{
    public GameObject objectToSpawn;
    public int whenToSpawn; // O índice do 'amount' em que este objeto spawna
    public Transform spawnPoint;
    public bool destroyPreviousSpawn = true;
    public float delay = 0;
}

public class OrderPerItem : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemsToCompleteOrder = new List<GameObject>();
    [SerializeField] private List<ItemStepData> stepData = new List<ItemStepData>();
    private List<GameObject> originalList = new List<GameObject>();

    [SerializeField] private bool balcao = false;
    [SerializeField] private AudioClip clip;

    private AudioManager audioManager;

    private GameObject lastSpawnedObject;
    private GameObject balcaoObj;
    private OrdersManager ordersManager;
    private bool toBeMade = false;
    private int amount = 0;

    void Start()
    {
        ordersManager = FindFirstObjectByType<OrdersManager>();
        originalList = new List<GameObject>(itemsToCompleteOrder);
        audioManager = FindFirstObjectByType<AudioManager>();
    }

    public void ChangeToBeMadeState(bool b)
    {
        toBeMade = b;
        if (toBeMade) SetObjectsToInteractable();
    }

    public void SetObjectsToInteractable()
    {
        CheckAndSpawnItems();

        if (amount < itemsToCompleteOrder.Count)
        {
            if (amount > 0)
            {
                if (itemsToCompleteOrder[amount - 1].GetComponent<Interactable>().CanBeInteracted)
                    return;
            }

            // Procurar se o item atual tem um delay definido no stepData
            float delay = GetDelayForCurrentAmount();

            if (delay > 0)
            {
                // Se houver delay, iniciamos a Coroutine
                StartCoroutine(SetInteractableAfterDelay(itemsToCompleteOrder[amount], delay));
            }
            else
            {
                // Sem delay, ativa imediatamente
                itemsToCompleteOrder[amount].GetComponent<Interactable>().SetCanBeInteractedToTrue();
            }

            amount++;
            return;
        }

        FinalizeSequence();
    }

    // Função que espera o tempo e depois ativa a interação
    private IEnumerator SetInteractableAfterDelay(GameObject item, float time)
    {
        yield return new WaitForSeconds(time);

        if (item != null) // Segurança caso o item seja destruído enquanto espera
        {
            audioManager.PlaySound(clip);
            item.GetComponent<Interactable>().SetCanBeInteractedToTrue();
        }
    }

    // Função auxiliar para buscar o delay na lista de steps
    private float GetDelayForCurrentAmount()
    {
        foreach (var step in stepData)
        {
            // Se o spawn aconteceu neste 'amount' e tem delay
            if (step.whenToSpawn == amount ) // -1 porque o amount já foi incrementado em CheckAndSpawn
            {
                return step.delay;
            }
        }
        return 0;
    }

    private void CheckAndSpawnItems()
    {
        foreach (var step in stepData)
        {
            if (step.whenToSpawn == amount && step.objectToSpawn != null)
            {
                if (step.destroyPreviousSpawn && lastSpawnedObject != null)
                {
                    Destroy(lastSpawnedObject);
                }

                GameObject spawned = Instantiate(step.objectToSpawn, step.spawnPoint.position, step.spawnPoint.rotation, this.transform);
                lastSpawnedObject = spawned;

                if (spawned.TryGetComponent<Interactable>(out var inter))
                {
                    // Se o item acabou de ser spawnado, ele entra na lista de ordem
                    itemsToCompleteOrder.Insert(amount, spawned);
                }

                if (spawned.TryGetComponent<OrderPerItem>(out var nextOrder))
                {
                    nextOrder.ChangeToBeMadeState(true);
                }
            }
        }
    }

    private void FinalizeSequence()
    {
        amount = 0;
        toBeMade = false;
        itemsToCompleteOrder = new List<GameObject>(originalList);
        lastSpawnedObject = null;

        if (balcao)
        {
            ordersManager.TakeFromOrder();
        }
        else
        {
            balcaoObj = GameObject.FindWithTag("Balcao");
            if (balcaoObj != null && balcaoObj.TryGetComponent<OrderPerItem>(out var next))
            {
                next.ChangeToBeMadeState(true);
            }
        }

        foreach (GameObject item in itemsToCompleteOrder)
        {
            if (item != null)
                item.GetComponent<Interactable>().ResetCompleted();
        }
    }
}