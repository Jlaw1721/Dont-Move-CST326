using UnityEngine;

public class MovePlayerCam : MonoBehaviour
{
    // Connect to "CameraPos" on the player object to update the camera's position
    [SerializeField] private Transform cameraPosition;
    
    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
