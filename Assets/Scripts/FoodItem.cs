using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public string foodName; // Set this in the Inspector for each food item
    public Vector3 initialPosition; // Stores the initial position of the food item

    private void Start()
    {
        // Save the initial position of the food item
        initialPosition = transform.position;
    }
}
