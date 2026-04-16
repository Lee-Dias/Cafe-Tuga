using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class OrdersManager : MonoBehaviour
{
    [SerializeField] private ObjectsToPlace[] objectsToPlaces;
    [SerializeField] private GameObject fatherOfFurniture;

    [Header("Configurações de Dificuldade (Budget)")]
    [SerializeField] private float minTotalDifficulty = 1;
    [SerializeField] private float maxTotalDifficulty = 2;
    [SerializeField] private float multiplyOfOrdersPerLevel = 1.5f;

    [Header("Limites de Itens por Nível de Objeto")]
    [SerializeField] private int quantidadeInicialDeLimite = 1;
    [SerializeField] private int multiplicadorPorNivel = 3;
    private int[] maxItemsPerDifficultyLevel = { 99, 0, 0 };

    [Header("Progressão de Pedidos")]
    [SerializeField] private int amountOfOrdersOnLevel = 3;
    private int currentLevel = 1;
    private int ordersCompletedOnLevel;
    private List<ObjectsToPlace> finalOrder;
    private OrdersUIManager ordersUIManager;
    private CustomersManager customersManager;

    public List<ObjectsToPlace> FinalOrder => finalOrder;
    private void Start()
    {
        ordersUIManager = FindFirstObjectByType<OrdersUIManager>();
        customersManager = FindFirstObjectByType<CustomersManager>();
    }
    public void MakeOrder()
    {
        float currentOrderBudget = 0;
        finalOrder = new List<ObjectsToPlace>();

        // Dicionário para contar quantos itens de cada dificuldade já colocamos neste pedido
        Dictionary<int, int> countPerDifficulty = new Dictionary<int, int>();

        int safetyBreak = 0;
        while (currentOrderBudget < minTotalDifficulty && safetyBreak < 100)
        {
            safetyBreak++;

            // 1. Pega um objeto aleatório
            ObjectsToPlace randomObj = objectsToPlaces[Random.Range(0, objectsToPlaces.Length)];
            int objLevel = randomObj.taskDificulty; // Assumindo que 1, 2 ou 3

            // 2. VERIFICAÇÃO DE REGRAS:

            // Regra A: O nível do objeto está desbloqueado? (Baseado no tamanho do array)
            if (objLevel > maxItemsPerDifficultyLevel.Length) continue;

            // Regra B: Já atingimos o limite de quantidade para esse nível de objeto?
            countPerDifficulty.TryAdd(objLevel, 0);
            if (countPerDifficulty[objLevel] >= maxItemsPerDifficultyLevel[objLevel - 1]) continue;

            // Regra C: Cabe no orçamento total (Budget)?
            if (currentOrderBudget + objLevel <= maxTotalDifficulty)
            {
                finalOrder.Add(randomObj);
                currentOrderBudget += objLevel;
                countPerDifficulty[objLevel]++;
            }

            // Se atingiu o mínimo, tem chance de encerrar
            if (currentOrderBudget >= minTotalDifficulty && Random.value > 0.5f) break;
        }

        foreach (Transform obj in fatherOfFurniture.transform) {
            if (obj.GetComponent<SpaceSelector>().WhatFurniture == finalOrder[0]){ 
                obj.GetComponent<OrderPerItem>().ChangeToBeMadeState(true);
                break;
            }
        }
        ordersUIManager.UpdateOrder();
    }
    public void TakeFromOrder()
    {
        foreach (Transform obj in fatherOfFurniture.transform)
        {
            if (obj.GetComponent<SpaceSelector>().WhatFurniture == finalOrder[0])
            {
                obj.GetComponent<OrderPerItem>().ChangeToBeMadeState(false);
                break;
            }
        }
        finalOrder.RemoveAt(0);
        if (finalOrder.Count == 0)
        {
            ordersCompletedOnLevel += 1;
            if (ordersCompletedOnLevel >= amountOfOrdersOnLevel)
            {
                UpdateLevel();
                finalOrder = new List<ObjectsToPlace>();
                ordersUIManager.UpdateOrder();
                customersManager.SpawnCustomer();
                return;
            }
            finalOrder = new List<ObjectsToPlace>();
            ordersUIManager.UpdateOrder();
            customersManager.SpawnCustomer();

            return;
        }
        foreach (Transform obj in fatherOfFurniture.transform)
        {

            if (obj.GetComponent<SpaceSelector>().WhatFurniture == finalOrder[0])
            {
                obj.GetComponent<OrderPerItem>().ChangeToBeMadeState(true);
                break;
            }
        }

        ordersUIManager.UpdateOrder();
    }

    void UpdateLevel()
    {
        currentLevel++;
        ordersCompletedOnLevel = 0; // Resetamos para o novo nível

        // Aumenta o orçamento total
        minTotalDifficulty *= multiplyOfOrdersPerLevel;
        maxTotalDifficulty *= multiplyOfOrdersPerLevel;
        amountOfOrdersOnLevel = Mathf.RoundToInt(amountOfOrdersOnLevel * multiplyOfOrdersPerLevel);
        // --- LÓGICA DE DESBLOQUEIO ---
        // Exemplo: No nível 2, libera itens de nível 2
        if (currentLevel == 2) maxItemsPerDifficultyLevel[1] = 2;

        // No nível 3, libera itens de nível 3 (limite de 2) e aumenta os de nível 2
        if (currentLevel == 3)
        {
            maxItemsPerDifficultyLevel[1] *= multiplicadorPorNivel;
            maxItemsPerDifficultyLevel[2] = 2;
        }

        // No nível 4 em diante, você pode remover os limites (colocar 99)
        if (currentLevel >= 4)
        {
            maxItemsPerDifficultyLevel[1] *= multiplicadorPorNivel;
            maxItemsPerDifficultyLevel[2] *= multiplicadorPorNivel;
        }
    }
}