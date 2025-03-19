using System.Collections.Generic;
using UnityEngine;

public class WokController : MonoBehaviour
{
    public GameObject[] recipeCards; 
    private List<GameObject> foodInWok = new List<GameObject>(); 

    private void Start()
    {
        CloseRecipeCard();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food") && !foodInWok.Contains(other.gameObject))
        {
            foodInWok.Add(other.gameObject);
            Debug.Log("Food added to wok: " + other.name);
            CheckRecipe();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Food") && foodInWok.Contains(other.gameObject))
        {
            foodInWok.Remove(other.gameObject);
            Debug.Log("Food removed from wok: " + other.name);
        }
    }

    private void CheckRecipe()
    {
        if (foodInWok.Count == 2)
        {
            string[] foodNames = new string[foodInWok.Count];
            for (int i = 0; i < foodInWok.Count; i++)
            {
                foodNames[i] = foodInWok[i].GetComponent<FoodItem>().foodName;
            }

            int recipeIndex = GetRecipeIndex(foodNames);
            if (recipeIndex != -1)
            {
                Debug.Log("Showing recipe card: " + recipeIndex);
                ShowRecipeCard(recipeIndex);
            }

            foreach (var food in foodInWok)
            {
                food.transform.position = food.GetComponent<FoodItem>().initialPosition;
            }
            foodInWok.Clear();
        }
    }

    private int GetRecipeIndex(string[] foodNames)
    {
        if (foodNames.Length == 2)
        {
            if ((foodNames[0] == "carrot" && foodNames[1] == "carrot") ||
                (foodNames[0] == "carrot" && foodNames[1] == "carrot"))
            {
                return 0;
            }
            else if ((foodNames[0] == "carrot" && foodNames[1] == "pepper") ||
                     (foodNames[0] == "pepper" && foodNames[1] == "carrot"))
            {
                return 1;
            }
            else if ((foodNames[0] == "broccoli" && foodNames[1] == "pepper") ||
                     (foodNames[0] == "pepper" && foodNames[1] == "broccoli"))
            {
                return 2;
            }
            else if ((foodNames[0] == "steak" && foodNames[1] == "pepper") ||
                     (foodNames[0] == "pepper" && foodNames[1] == "steak"))
            {
                return 3;
            }
            else if ((foodNames[0] == "broccoli" && foodNames[1] == "steak") ||
                     (foodNames[0] == "steak" && foodNames[1] == "broccoli"))
            {
                return 4;
            }
        }

        return -1;
    }

    private void ShowRecipeCard(int index)
    {
        foreach (var card in recipeCards)
        {
            card.SetActive(false);
        }
        if (index >= 0 && index < recipeCards.Length)
        {
            recipeCards[index].SetActive(true);
        }
    }

    public void CloseRecipeCard()
    {
        foreach (var card in recipeCards)
        {
            card.SetActive(false);
        }
    }
}