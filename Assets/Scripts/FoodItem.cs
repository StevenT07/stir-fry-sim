using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public string foodName;
    public Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }
}
