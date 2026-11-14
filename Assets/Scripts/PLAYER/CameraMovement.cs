using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Camera")]

    public Transform cameraPosition;
    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
