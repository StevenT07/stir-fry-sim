using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class DragObject : MonoBehaviour

{
    private Vector3 offset;
    private float z;

    void OnMouseDown()
    {
        z = gameObject.transform.position.z;

        offset = gameObject.transform.position - screenToWorld(Input.mousePosition.x, Input.mousePosition.y, z);
    }
    void OnMouseDrag()
    {
        Vector3 curPosition = screenToWorld(Input.mousePosition.x, Input.mousePosition.y, z) + offset;
        transform.position = curPosition;
    }
    // transform screen point to world point with fixed z
    private Vector3 screenToWorld(float screen_x, float screen_y, float world_z) {
        Plane plane = new Plane(Vector3.forward, new Vector3(0f, 0f, world_z));
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screen_x, screen_y, 0));
        if (plane.Raycast(ray, out float distance)) {
            Vector3 worldPoint = ray.GetPoint(distance);
            return worldPoint;
        }
        return Vector3.zero;
    }
}