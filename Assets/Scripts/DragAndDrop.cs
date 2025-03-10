using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class DragObject : MonoBehaviour

{
    private Vector3 offset;
    public float y;

    void OnMouseDown()
    {
        // y = gameObject.transform.position.y;

        // translate pos into y-plane
        // could change this to move along the camera ray instead of directly translating up/down
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, y, gameObject.transform.position.z);

        offset = gameObject.transform.position - screenToWorld(Input.mousePosition.x, Input.mousePosition.y, y);
    }
    void OnMouseDrag()
    {
        Vector3 curPosition = screenToWorld(Input.mousePosition.x, Input.mousePosition.y, y) + offset;
        transform.position = curPosition;
    }
    // transform screen point to world point with fixed y
    private Vector3 screenToWorld(float screen_x, float screen_y, float world_y) {
        Plane plane = new Plane(Vector3.up, new Vector3(0f, world_y, 0f));
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(screen_x, screen_y, 0));
        if (plane.Raycast(ray, out float distance)) {
            Vector3 worldPoint = ray.GetPoint(distance);
            return worldPoint;
        }
        Debug.LogError("Did not intersect");
        return Vector3.zero;
    }
}