using System;
using UnityEngine;

public class PlayerCamController : MonoBehaviour
{
    [SerializeField] private Transform currentOrientation;
    [HideInInspector] public Vector2 mouseSensitivity;
    private Vector2 _mouseInput;
    private Vector2 _currentRotation;
    private float _prevSpeed;
    private float _currentMouseSensitivity;
    private static PlayerCamController _instance;
    public static PlayerCamController Instance => _instance;
    private const float Tolerance = 0.01f;  // Tolerance variable to avoid floating point comparisons between float values (unity gets upset if this isn't here)

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    private void Start()
    {
        // Locking the cursor in the center of the screen and making it invisible to the player
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        // Initialize previous rotation to current rotation at start
        var transform1 = transform;
        var rotation = transform1.rotation;
        _currentRotation = new Vector2(rotation.x, rotation.y);

        
        //if (GameSettings.Instance.mouseSens == 0)
        //{
        //    GameSettings.Instance.mouseSensitivitySlider.value = 3;
        //}

        mouseSensitivity.x = GameSettings.Instance.mouseSens;
        mouseSensitivity.y = GameSettings.Instance.mouseSens;
        _currentMouseSensitivity = GameSettings.Instance.mouseSens;
    }
    
    private void Update()
    {
        MoveCamera();

        // Checking to see if the player has updated the mouse sensitivity in the settings
        if (Math.Abs(GameSettings.Instance.mouseSens - _currentMouseSensitivity) > Tolerance)
        {
            mouseSensitivity.x = GameSettings.Instance.mouseSens;
            mouseSensitivity.y = GameSettings.Instance.mouseSens;
            _currentMouseSensitivity = GameSettings.Instance.mouseSens;
        }
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
        // Rotating orientation object on player across y axis
        currentOrientation.rotation = Quaternion.Euler(0f, _currentRotation.y, 0f);
    }

    public float GetCameraMovement()
    {
        // Getting the rotation amount of the current frame
        var rotation = transform.rotation;
        Vector2 currRotation = new Vector2(rotation.x, rotation.y);

        return currRotation.magnitude;
    }
}