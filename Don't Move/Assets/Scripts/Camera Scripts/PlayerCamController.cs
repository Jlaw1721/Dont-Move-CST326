using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    
    
    public Transform currentOrientation;
    
    public Vector2 mouseSensitivity;
    private Vector2 _mouseInput;
    private Vector2 _currentRotation;
    
    private void Start()
    {
        // Locking the cursor in the center of the screen and making it invisible to the player
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

   
    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        // Gathering player inputs
        _mouseInput.x = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity.x;
        _mouseInput.y = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity.y;

        // Clamping rotation so the player can only look up or down 90 degrees
        _currentRotation.x = Mathf.Clamp(_currentRotation.x, -90f, 90f);
        
        _currentRotation.x -= _mouseInput.y;
        _currentRotation.y += _mouseInput.x;
        
        // Rotating Camera across x and y axis
        transform.rotation = Quaternion.Euler(_currentRotation.x,_currentRotation.y, 0f);
        // Rotating Camera across y axis
        currentOrientation.rotation = Quaternion.Euler(0f, _currentRotation.y, 0f);
    }
}
