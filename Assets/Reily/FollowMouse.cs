using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f; 
        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        worldPos.z = -2f; 
        transform.position = worldPos;
    }
}