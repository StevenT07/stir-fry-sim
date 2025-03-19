using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WokDrag : MonoBehaviour
{
    private Vector3 offset;
    private float z;
    public float speed = 5f; // cap on speed
    private Rigidbody rb;
    private Vector3 targetPosition;
    private bool isDragging = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void OnMouseDown()
    {
        rb.useGravity = false; 
        z = transform.position.z;
        offset = transform.position - ScreenToWorld(Input.mousePosition.x, Input.mousePosition.y, z);
        isDragging = true;
    }

    void OnMouseUp()
    {
        rb.useGravity = true; 
        rb.linearVelocity = Vector3.zero; 
        isDragging = false;
    }

    void FixedUpdate()
    {
        if (isDragging) {
            targetPosition = ScreenToWorld(Input.mousePosition.x, Input.mousePosition.y, z) + offset;
            Vector3 direction = (targetPosition - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
            // slow down as wok approaches cursor
            if (distanceToTarget > 0.01f) {
                rb.linearVelocity = direction * Mathf.Min(speed, distanceToTarget / Time.fixedDeltaTime);
            } else {
                rb.linearVelocity = Vector3.zero;
            }
        }
    }

    private Vector3 ScreenToWorld(float screenX, float screenY, float worldZ)
    {
        Plane plane = new Plane(Vector3.forward, new Vector3(0f, 0f, worldZ));
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screenX, screenY, 0));
        if (plane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}