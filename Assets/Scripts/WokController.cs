using System.Collections.Generic;
using UnityEngine;

public class WokController : MonoBehaviour
{
    public GameObject[] recipeCards; // Array of recipe card UI panels (assign in Inspector)
    private List<GameObject> foodInWok = new List<GameObject>(); // Tracks food items in the wok

    private void Start()
    {
        CloseRecipeCard();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a food item
        if (other.CompareTag("Food") && !foodInWok.Contains(other.gameObject))
        {
            // Add the food item to the list
            foodInWok.Add(other.gameObject);
            Debug.Log("Food added to wok: " + other.name);

            // Check if the required number of food items is in the wok
            CheckRecipe();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to a food item
        if (other.CompareTag("Food") && foodInWok.Contains(other.gameObject))
        {
            // Remove the food item from the list
            foodInWok.Remove(other.gameObject);
            Debug.Log("Food removed from wok: " + other.name);
        }
    }

    private void CheckRecipe()
    {
        // Check if the required number of food items is in the wok
        if (foodInWok.Count == 2) // Assuming 2 food items are required
        {
            // Get the names of the food items in the wok
            string[] foodNames = new string[foodInWok.Count];
            for (int i = 0; i < foodInWok.Count; i++)
            {
                foodNames[i] = foodInWok[i].GetComponent<FoodItem>().foodName;
            }

            // Determine which recipe card to show
            int recipeIndex = GetRecipeIndex(foodNames);
            if (recipeIndex != -1)
            {
                Debug.Log("Showing recipe card: " + recipeIndex);
                ShowRecipeCard(recipeIndex);
            }

            // Reset food items to their initial positions
            foreach (var food in foodInWok)
            {
                food.transform.position = food.GetComponent<FoodItem>().initialPosition;
            }

            // Clear the list of food items in the wok
            foodInWok.Clear();
        }
    }

    private int GetRecipeIndex(string[] foodNames)
    {
        // Define recipe combinations
        if (foodNames.Length == 2)
        {
            if ((foodNames[0] == "carrot" && foodNames[1] == "carrot") ||
                (foodNames[0] == "carrot" && foodNames[1] == "carrot"))
            {
                return 0; // Recipe 1: Fried Rice
            }
            else if ((foodNames[0] == "carrot" && foodNames[1] == "pepper") ||
                     (foodNames[0] == "pepper" && foodNames[1] == "carrot"))
            {
                return 1; // Recipe 2: Veggie Omelette
            }
            // Add more combinations as needed
        }
        // Add more conditions for other combinations

        return -1; // No matching recipe
    }

    private void ShowRecipeCard(int index)
    {
        // Hide all recipe cards first
        foreach (var card in recipeCards)
        {
            card.SetActive(false);
        }

        // Show the selected recipe card
        if (index >= 0 && index < recipeCards.Length)
        {
            recipeCards[index].SetActive(true);
        }
    }

    // Call this method from the "X" button's OnClick event
    public void CloseRecipeCard()
    {
        foreach (var card in recipeCards)
        {
            card.SetActive(false);
        }
    }
}